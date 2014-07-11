using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.Training.CartPersistence.Carts;
using ActiveCommerce.Training.CartPersistence.Common;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.RestoreCart
{
    public class SetCartUpdatedEvent : IRestoreCartProcessor
    {
        public void Process(RestoreCartArgs args)
        {
            args.ShoppingCart.CartChanged  += CartsUpdatedHandler.CartUpdated;

            CartPersistenceContext.CartUpdatedEventInitialized = true;
        }
    }
}