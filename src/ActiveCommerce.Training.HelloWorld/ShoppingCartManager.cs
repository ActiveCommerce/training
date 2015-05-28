using Sitecore.Ecommerce.DomainModel.Carts;
using Sitecore.Ecommerce.DomainModel.Products;
using Sitecore.StringExtensions;
using IProductStockManager = ActiveCommerce.Products.Stock.IProductStockManager;

namespace ActiveCommerce.Training.HelloWorld
{
    public class ShoppingCartManager : ActiveCommerce.Carts.ShoppingCartManager
    {
        public ShoppingCartManager(IProductRepository productRepository, IProductStockManager stockManager, ShoppingCartLine cartLinePrototype) : base(productRepository, stockManager, cartLinePrototype)
        {
        }

        public override void AddProduct(string productCode, uint quantity)
        {
            Sitecore.Diagnostics.Log.Warn("{0} added to cart!".FormatWith(productCode), this);
            base.AddProduct(productCode, quantity);
        }
    }
}