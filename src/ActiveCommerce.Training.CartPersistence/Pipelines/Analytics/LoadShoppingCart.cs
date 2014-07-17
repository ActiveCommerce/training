﻿using System;
using ActiveCommerce.Training.CartPersistence.Common;
using Sitecore.Diagnostics;
using Sitecore.Ecommerce.DomainModel.Users;
using Sitecore.Pipelines;
using Sitecore.Ecommerce.DomainModel.Products;
using ActiveCommerce.Training.CartPersistence.Pipelines.RestoreCart;
using Sitecore.Ecommerce.DomainModel.Carts;
using Microsoft.Practices.Unity;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.Analytics
{
    public class LoadShoppingCart
    {
        public void Process(PipelineArgs args)
        {
            try
            {
                if (!PersistingIsActive())
                {
                    return;
                }

                var restoreProductArgs = new RestoreCartArgs
                {
                    CartManager = Sitecore.Ecommerce.Context.Entity.Resolve<IShoppingCartManager>(),
                    ShoppingCart =
                        Sitecore.Ecommerce.Context.Entity.GetInstance<ShoppingCart>() as
                            ActiveCommerce.Carts.ShoppingCart,
                    StockManager = Sitecore.Ecommerce.Context.Entity.Resolve<IProductStockManager>(),
                    ProductRepository = Sitecore.Ecommerce.Context.Entity.Resolve<IProductRepository>(),
                    CustomerManager = Sitecore.Ecommerce.Context.Entity.Resolve<ICustomerManager<CustomerInfo>>(),
                    Result = new RestoreCartResult()
                };
                RestoreCartPipeline.Run(restoreProductArgs);
            }
            catch (Exception exception)
            {
                Log.Error("Error occured in the Cart Persistence Loader.", exception);
            }
        }

        protected virtual bool PersistingIsActive()
        {
            if (!CartPersistenceContext.IsActive || !Sitecore.Analytics.Tracker.IsActive)
            {
                return false;
            }

            return true;
        }
    }
}