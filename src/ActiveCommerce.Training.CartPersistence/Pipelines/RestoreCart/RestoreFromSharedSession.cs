using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.Carts;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.RestoreCart
{
    public class RestoreFromSharedSession : IRestoreCartProcessor
    {
        public void Process(RestoreCartArgs args)
        {
            var tracker = Sitecore.Analytics.Tracker.Current;
            if (tracker.Contact.Attachments.ContainsKey("ac-cart"))
            {
                var cart = tracker.Contact.Attachments["ac-cart"] as ShoppingCart;
                Sitecore.Ecommerce.Context.Entity.SetInstance(cart);
            }
        }
    }
}