using ActiveCommerce.Training.CartPersistence.Pipelines.PersistCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.RestoreCart
{
    /// <summary>
    /// TODO: Persisted values don't seem to be saving at the moment?
    /// </summary>
    public class ReadFromCustomer : IRestoreCartProcessor
    {
        public void Process(RestoreCartArgs args)
        {
            //don't restore customer's cart if they already have one. otherwise, cart could be changed during checkout
            //TODO: better way to handle this?
            if (args.ShoppingCart.ShoppingCartLines.Any() || args.ShoppingCart.CouponCodes.Any())
            {
                return;
            }

            var user = args.CustomerManager.CurrentUser;
            if (user == null || string.IsNullOrEmpty(user.NickName) || Sitecore.Context.Domain.IsAnonymousUser(user.NickName))
            {
                return;
            }

            if (!string.IsNullOrEmpty(user.CustomProperties[PersistToCustomer.CouponCodeKey]))
            {
                args.CouponCode = user.CustomProperties[PersistToCustomer.CouponCodeKey];
            }

            if (!string.IsNullOrEmpty(user.CustomProperties[PersistToCustomer.CartItemsKey]))
            {
                if (args.CartItems == null)
                {
                    args.CartItems = new Dictionary<string, uint>();
                }

                var cartItems = user.CustomProperties[PersistToCustomer.CartItemsKey];
                var products = cartItems.Split(',');
                foreach (var product in products)
                {
                    var codeQuantity = product.Split('|');
                    if (codeQuantity.Length != 2) continue;
                    uint quantity = 0;
                    uint.TryParse(codeQuantity[1], out quantity);
                    if (quantity < 1) continue;
                    var productCode = codeQuantity[0];
                    if (!args.CartItems.ContainsKey(productCode))
                    {
                        args.CartItems.Add(productCode, quantity);
                    }
                    else if (args.CartItems[productCode] < quantity)
                    {
                        args.CartItems[productCode] = quantity;
                    }
                }
            }

        }
    }
}