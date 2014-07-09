using ActiveCommerce.Training.CartPersistence.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.PersistCart
{
    public class PersistToCookie : IPersistCartProcessor
    {
        public void Process(PersistCartArgs args)
        {
            var cookie = new ShoppingCartCookie();
            cookie.CartItems.Clear();
            foreach (var item in args.CartItems)
            {
                cookie.CartItems.Add(item.Key, item.Value);
            }
            cookie.CouponCode = args.CouponCode;
            cookie.Save();
        }
    }
}