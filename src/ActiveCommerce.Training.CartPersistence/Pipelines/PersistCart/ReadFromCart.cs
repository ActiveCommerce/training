using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.PersistCart
{
    public class ReadFromCart : IPersistCartProcessor
    {
        public void Process(PersistCartArgs args)
        {
            var cart = args.ShoppingCart;
            args.CartItems = new Dictionary<string, uint>();
            foreach (var line in cart.ShoppingCartLines)
            {
                args.CartItems.Add(line.Product.Code, line.Quantity);
            }
            args.CouponCode = string.Join("|", cart.CouponCodes);
        }
    }
}