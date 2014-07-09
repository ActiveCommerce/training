using ActiveCommerce.Training.CartPersistence.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.RestoreCart
{
    public class ReadFromCookie : IRestoreCartProcessor
    {
        public void Process(RestoreCartArgs args)
        {
            var cookie = new ShoppingCartCookie();
            if (args.CartItems == null)
            {
                args.CartItems = cookie.CartItems;
            }
            else
            {
                foreach (var item in cookie.CartItems)
                {
                    if (!args.CartItems.ContainsKey(item.Key))
                    {
                        args.CartItems.Add(item.Key, item.Value);
                    }
                    else if (args.CartItems[item.Key] < item.Value)
                    {
                        args.CartItems[item.Key] = item.Value;
                    }
                }
            }
            if (string.IsNullOrEmpty(args.CouponCode))
            {
                args.CouponCode = cookie.CouponCode;
            }
        }
    }
}