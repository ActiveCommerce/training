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
                if (!PersistenceActive())
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
            catch (Exception e)
            {
                Log.Error("Error persisting shopping cart", e, this);
            }
        }

        protected virtual bool PersistenceActive()
        {
            return CartPersistenceContext.IsActive;
        }
    }
}