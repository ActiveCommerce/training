using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.RestoreCartProduct
{
    public class CheckProductStock : IRestoreCartProductProcessor
    {
        public void Process(RestoreCartProductArgs args)
        {
            var stock = args.StockManager.GetStock(new Sitecore.Ecommerce.DomainModel.Products.ProductStockInfo
            {
                ProductCode = args.ProductCode
            });
            if (stock.Stock == 0)
            {
                args.Result.Success = false;
                args.Result.FailedProducts.Add(args.ProductCode);
                args.AbortPipeline();
            }
            else if (stock.Stock < args.Quantity)
            {
                //reduce stock added to cart, note product as failed, but don't abort
                args.Quantity = (uint)stock.Stock;
                args.Result.Success = false;
                args.Result.FailedProducts.Add(args.ProductCode);
            }
        }
    }
}