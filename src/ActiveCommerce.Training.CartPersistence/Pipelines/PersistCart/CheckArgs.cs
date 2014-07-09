using Sitecore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.PersistCart
{
    public class CheckArgs : IPersistCartProcessor
    {
        public void Process(PersistCartArgs args)
        {
            Assert.ArgumentNotNull(args.ShoppingCart, "ShoppingCart");
            Assert.ArgumentNotNull(args.CustomerManager, "CustomerManager");
        }
    }
}