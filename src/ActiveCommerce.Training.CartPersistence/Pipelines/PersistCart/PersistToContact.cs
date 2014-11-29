using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.Analytics.Model;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.PersistCart
{
    public class PersistToContact : IPersistCartProcessor
    {
        public void Process(PersistCartArgs args)
        {
            var tracker = Sitecore.Analytics.Tracker.Current;
            if (tracker == null || !tracker.IsActive || tracker.Contact == null)
            {
                return;
            }

            var persistentCart = tracker.Contact.GetFacet<IShoppingCart>("Cart");
            foreach (var line in persistentCart.ShoppingCartLines.Keys)
            {
                persistentCart.ShoppingCartLines.Remove(line);
            }
            foreach (var item in args.CartItems)
            {
                var code = item.Key;
                var persistentLine = persistentCart.ShoppingCartLines.Create(code);
                persistentLine.ProductCode = code;
                persistentLine.Quantity = item.Value;
            }
            persistentCart.CouponCode = args.CouponCode;
        }
    }
}