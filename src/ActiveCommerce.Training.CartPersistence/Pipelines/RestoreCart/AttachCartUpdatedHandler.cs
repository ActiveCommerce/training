using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.Training.CartPersistence.Carts;
using ActiveCommerce.Training.CartPersistence.Common;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.RestoreCart
{
    public class AttachCartUpdatedHandler : IRestoreCartProcessor
    {
        public void Process(RestoreCartArgs args)
        {
            //if (!CartPersistenceContext.CartUpdatedEventInitialized)
            //{
                args.ShoppingCart.CartChanged += CartUpdatedHandler.CartUpdated;
                CartPersistenceContext.CartUpdatedEventInitialized = true;
            //}
        }
    }
}