using ActiveCommerce.Training.CartPersistence.Common;
using ActiveCommerce.Training.CartPersistence.Pipelines.PersistCart;
using Sitecore.Diagnostics;
using Sitecore.Ecommerce.DomainModel.Carts;
using Sitecore.Ecommerce.DomainModel.Users;
using Sitecore.Pipelines;
using System;
using Microsoft.Practices.Unity;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.Analytics
{
    public class PersistShoppingCart
    {
        public void Process(PipelineArgs args)
        {
            try
            {
                if (!PersistingIsActive())
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
            catch (Exception exception)
            {
                Log.Error("Error occured in the Cart Persistence persist pipeline.", exception);
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