using Sitecore.StringExtensions;

namespace ActiveCommerce.Training.HelloWorld
{
    public class ShoppingCartManager : ActiveCommerce.Carts.ShoppingCartManager
    {
        public override void AddProduct(string productCode, uint quantity)
        {
            Sitecore.Diagnostics.Log.Warn("{0} added to cart!".FormatWith(productCode), this);
            base.AddProduct(productCode, quantity);
        }
    }
}