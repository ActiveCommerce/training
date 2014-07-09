using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.RestoreCartProduct
{
    public class CheckProductInCart : IRestoreCartProductProcessor
    {
        public void Process(RestoreCartProductArgs args)
        {
            var cart = args.ShoppingCart;
            var cartProduct = cart.ShoppingCartLines.Where(x => x.Product.Code == args.ProductCode).SingleOrDefault();
            if (cartProduct != null && cartProduct.Quantity >= args.Quantity)
            {
                args.AbortPipeline();
                return;
            }
            args.Result.AttemptedRestore = true;
        }
    }
}