using ActiveCommerce.Training.CartPersistence.Cookies;
using ActiveCommerce.Training.CartPersistence.Pipelines.PersistCart;
using Sitecore.Ecommerce.DomainModel.Carts;
using Sitecore.Ecommerce.DomainModel.Users;
using Sitecore.Pipelines;
using Sitecore.Pipelines.HttpRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.Analytics
{
    public class PersistShoppingCart
    {
        public void Process(PipelineArgs args)
        {
            if (!Sitecore.Analytics.Tracker.IsActive)
            {
                return;
            }

            var persistCartArgs = new PersistCartArgs
            {
                ShoppingCart = Sitecore.Ecommerce.Context.Entity.GetInstance<ShoppingCart>() as ActiveCommerce.Carts.ShoppingCart,
                CustomerManager = Sitecore.Ecommerce.Context.Entity.Resolve<ICustomerManager<CustomerInfo>>()
            };
            PersistCartPipeline.Run(persistCartArgs);
        }
    }
}