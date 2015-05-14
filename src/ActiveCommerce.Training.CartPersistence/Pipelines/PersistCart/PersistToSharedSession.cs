using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.PersistCart
{
    public class PersistToSharedSession : IPersistCartProcessor
    {
        public void Process(PersistCartArgs args)
        {
            var tracker = Sitecore.Analytics.Tracker.Current;
            var cart = args.ShoppingCart;
            tracker.Contact.Attachments["ac-cart"] = cart;
        }
    }
}