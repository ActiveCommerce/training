using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.CartPersistence.Cookies
{
    public class ShoppingCartCookie
    {
        public const string ShoppingCartCookieName = "AC_SHOPPINGCART_COOKIE";
        public const string CouponCodeCookieName = "AC_COUPONCODE_COOKIE";

        private HttpContext _context;

        protected string CookieDomain
        {
            get
            {
                return Sitecore.Configuration.Settings.GetSetting("ActiveCommerce.Cart.CookieDomain");
            }
        }

        protected TimeSpan CookieExpiration
        {
            get
            {
                return Sitecore.Configuration.Settings.GetTimeSpanSetting("ActiveCommerce.Cart.CookieExpiration", "3650.00:00:00");
            }
        }
        

        public IDictionary<string, uint> CartItems { get; set; }
        public string CouponCode { get; set; }

        public ShoppingCartCookie() : this(HttpContext.Current)
        {

        }

        public ShoppingCartCookie(HttpContext context)
        {
            CartItems = new Dictionary<string, uint>();
            if (context == null)
            {
                return;
            }
            _context = context;
            LoadCart();
            LoadCoupon();
        }

        private void LoadCart()
        {
            var cartCookie = GetCookie(ShoppingCartCookieName);
            if (cartCookie == null || string.IsNullOrEmpty(cartCookie.Value))
            {
                return;
            }

            var products = cartCookie.Value.Split(',');
            foreach (var product in products)
            {
                var codeQuantity = product.Split('|');
                if (codeQuantity.Length != 2) continue;
                uint quantity = 0;
                uint.TryParse(codeQuantity[1], out quantity);
                if (quantity < 1) continue;
                CartItems.Add(codeQuantity[0], quantity);
            }
        }

        private void LoadCoupon()
        {
            var couponCookie = GetCookie(CouponCodeCookieName);
            if (couponCookie == null || string.IsNullOrWhiteSpace(couponCookie.Value))
            {
                return;
            }

            CouponCode = couponCookie.Value;
        }

        private HttpCookie GetCookie(string name)
        {
            if (_context == null)
            {
                return null;
            }
            return this.GetCookie(_context.Response.Cookies, name) ?? this.GetCookie(_context.Request.Cookies, name);
        }

        private HttpCookie GetCookie(HttpCookieCollection cookies, string name)
        {
            if (!cookies.AllKeys.Any((string key) => key == name))
            {
                return null;
            }
            return cookies.Get(name);
        }

        public void Save()
        {
            if (_context == null)
            {
                return;
            }
            SaveCart();
            SaveCoupon();
        }

        private void SaveCart()
        {
            string cookieValue = string.Empty;
            if (CartItems != null && CartItems.Count > 0)
            {
                cookieValue = string.Join(",", CartItems.Select(x => string.Format("{0}|{1}", x.Key, x.Value)));
            }
            var cartCookie = GetResponseCookie(ShoppingCartCookieName);
            cartCookie.Value = cookieValue;
        }

        private void SaveCoupon()
        {
            var couponCookie = GetResponseCookie(CouponCodeCookieName);
            couponCookie.Value = CouponCode ?? string.Empty;
        }

        private HttpCookie GetResponseCookie(string name)
        {
            var httpCookie = _context.Response.Cookies[name];
            if (httpCookie == null)
            {
                httpCookie = new HttpCookie(name);
                _context.Response.Cookies.Add(httpCookie);
            }
            httpCookie.Path = "/";
            httpCookie.Expires = DateTime.UtcNow.Add(CookieExpiration);
            if (!string.IsNullOrEmpty(CookieDomain))
            {
                httpCookie.Domain = CookieDomain;
            }
            return httpCookie;
        }
    }
}