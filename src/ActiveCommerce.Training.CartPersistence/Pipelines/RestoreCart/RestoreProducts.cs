using ActiveCommerce.Training.CartPersistence.Pipelines.RestoreCartProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.RestoreCart
{
    public class RestoreProducts : IRestoreCartProcessor
    {
        public void Process(RestoreCartArgs args)
        {
            if (args.CartItems == null || !args.CartItems.Any())
            {
                return;
            }

            if (args.ShoppingCart.ShoppingCartLines.Any() && !args.ShoppingCart.ShoppingCartLines.Select(s => s.Product.Code).Except(args.CartItems.Keys).Any())
            {
                return;
            }

            if (args.ShoppingCart.ShoppingCartLines.Any())
            {
                args.Result.CartMerged = true;
            }

            var restoreProductArgs = new RestoreCartProductArgs
            {
                CartManager = args.CartManager,
                ShoppingCart = args.ShoppingCart,
                StockManager = args.StockManager,
                ProductRepository = args.ProductRepository,
                Result = args.Result
            };
            foreach (var product in args.CartItems)
            {
                restoreProductArgs.ProductCode = product.Key;
                restoreProductArgs.Quantity = product.Value;
                RestoreCartProductPipeline.Run(restoreProductArgs);
            }
        }
    }
}