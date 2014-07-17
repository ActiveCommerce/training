using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.Training.CartPersistence.Carts;
using ActiveCommerce.Training.CartPersistence.Common;
using ActiveCommerce.Training.CartPersistence.Pipelines.RestoreCart;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.PersistCart
{
    public class CheckIfCartUpdateEventIsSet : IPersistCartProcessor
    {
        public void Process(PersistCartArgs args)
        {
            if (!CartPersistenceContext.CartUpdatedEventInitialized)
            {
                args.ShoppingCart.CartChanged += CartsUpdatedHandler.CartUpdated;

                CartPersistenceContext.CartUpdatedEventInitialized = true;
            }
        }
    }
}