using Sitecore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.RestoreCartProduct
{
    public class CheckArgs : IRestoreCartProductProcessor
    {
        public void Process(RestoreCartProductArgs args)
        {
            Assert.ArgumentNotNull(args.ShoppingCart, "ShoppingCart");
            Assert.ArgumentNotNull(args.CartManager, "CartManager");
            Assert.ArgumentNotNull(args.ProductRepository, "ProductRepository");
            Assert.ArgumentNotNull(args.StockManager, "StockManager");
            Assert.ArgumentNotNull(args.Result, "Result");
            Assert.ArgumentNotNullOrEmpty(args.ProductCode, "ProductCode");
            Assert.ArgumentCondition(args.Quantity > 0, "Quantity", "not > 0");
        }
    }
}