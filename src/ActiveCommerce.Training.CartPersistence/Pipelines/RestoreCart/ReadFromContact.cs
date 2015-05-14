using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.Training.CartPersistence.Analytics.Model;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.RestoreCart
{
    public class ReadFromContact : IRestoreCartProcessor
    {
        public void Process(RestoreCartArgs args)
        {
            var tracker = Sitecore.Analytics.Tracker.Current;
            if (tracker == null || !tracker.IsActive || tracker.Contact == null)
            {
                return;
            }

            var persistentCart = tracker.Contact.GetFacet<IShoppingCart>("Cart");
            var lines = persistentCart.ShoppingCartLines;
            if (lines == null)
            {
                return;
            }

            if (lines.Keys.Any())
            {
                args.CartItems = new Dictionary<string, uint>();
            }
            foreach (var key in lines.Keys)
            {
                args.CartItems.Add(lines[key].ProductCode, lines[key].Quantity);
            }
            args.CouponCode = persistentCart.CouponCode;
        }
    }
}