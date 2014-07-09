using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.RestoreCartProduct
{
    public class AddProductToCart : IRestoreCartProductProcessor
    {
        public void Process(RestoreCartProductArgs args)
        {
            var cartManager = args.CartManager;
            var cart = args.ShoppingCart;
            var cartProduct = cart.ShoppingCartLines.Where(x => x.Product.Code == args.ProductCode).SingleOrDefault();
            if (cartProduct != null && cartProduct.Quantity < args.Quantity)
            {
                args.Quantity = args.Quantity - cartProduct.Quantity;
            }
            cartManager.AddProduct(args.ProductCode, args.Quantity);
        }
    }
}