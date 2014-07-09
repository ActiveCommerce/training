using Sitecore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.RestoreCart
{
    public class CheckArgs : IRestoreCartProcessor
    {
        public void Process(RestoreCartArgs args)
        {
            Assert.ArgumentNotNull(args.ShoppingCart, "ShoppingCart");
            Assert.ArgumentNotNull(args.CartManager, "CartManager");
            Assert.ArgumentNotNull(args.ProductRepository, "ProductRepository");
            Assert.ArgumentNotNull(args.StockManager, "StockManager");
            Assert.ArgumentNotNull(args.CustomerManager, "CustomerManager");
            Assert.ArgumentNotNull(args.Result, "Result");
        }
    }
}