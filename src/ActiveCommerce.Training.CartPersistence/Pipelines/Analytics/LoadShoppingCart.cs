using ActiveCommerce.Training.CartPersistence.Cookies;
using Sitecore.Ecommerce.DomainModel.Carts;
using Sitecore.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using ActiveCommerce.Training.CartPersistence.Pipelines.RestoreCartProduct;
using Sitecore.Ecommerce.DomainModel.Products;
using ActiveCommerce.Training.CartPersistence.Pipelines.RestoreCart;
using Sitecore.Ecommerce.Users;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.Analytics
{
    public class LoadShoppingCart
    {
        public void Process(PipelineArgs args)
        {
            if (!Sitecore.Analytics.Tracker.IsActive)
            {
                return;
            }

            var restoreProductArgs = new RestoreCartArgs
            {
                CartManager = Sitecore.Ecommerce.Context.Entity.Resolve<IShoppingCartManager>(),
                ShoppingCart = Sitecore.Ecommerce.Context.Entity.GetInstance<ShoppingCart>() as ActiveCommerce.Carts.ShoppingCart,
                StockManager = Sitecore.Ecommerce.Context.Entity.Resolve<IProductStockManager>(),
                ProductRepository = Sitecore.Ecommerce.Context.Entity.Resolve<IProductRepository>(),
                CustomerManager = Sitecore.Ecommerce.Context.Entity.Resolve<CustomerManager<Sitecore.Ecommerce.DomainModel.Users.CustomerInfo>>(),
                Result = new RestoreCartResult()
            };
            RestoreCartPipeline.Run(restoreProductArgs);
        }
    }
}