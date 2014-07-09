using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.RestoreCart
{
    public class RestoreCouponCode : IRestoreCartProcessor
    {
        public void Process(RestoreCartArgs args)
        {
            if (!string.IsNullOrEmpty(args.CouponCode) && !args.ShoppingCart.CouponCodes.Any())
            {
                args.ShoppingCart.AddCouponCode(args.CouponCode);
            }
        }
    }
}