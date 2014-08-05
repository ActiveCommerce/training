using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.PersistCart
{
    public class PersistToCustomer : IPersistCartProcessor
    {
        public const string CouponCodeKey = "AC Cart Coupon";
        public const string CartItemsKey = "AC Cart Items";
        public const string EmptyCart = "<empty />";

        public void Process(PersistCartArgs args)
        {
            var user = args.CustomerManager.CurrentUser;
            if (user == null || string.IsNullOrEmpty(user.NickName) || Sitecore.Context.Domain.IsAnonymousUser(user.NickName))
            {
                return;
            }

            //CustomerInfo does not seem to be able to reset to an empty/null value, so we need to use an empty indicator

            var coupon = EmptyCart;
            if (!string.IsNullOrEmpty(args.CouponCode))
            {
                coupon = args.CouponCode;
            }
            
            var cartItems = EmptyCart;
            if (args.CartItems != null && args.CartItems.Any())
            {
                cartItems = string.Join(",", args.CartItems.Select(x => string.Format("{0}|{1}", x.Key, x.Value)));
            }

            user.CustomProperties[CouponCodeKey] = coupon;
            user.CustomProperties[CartItemsKey] = cartItems;
            args.CustomerManager.UpdateCustomerProfile(user);
        }
    }
}