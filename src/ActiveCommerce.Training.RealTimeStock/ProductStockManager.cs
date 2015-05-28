using System;
using ActiveCommerce.Products.Stock;
using Sitecore.Ecommerce.DomainModel.Products;
using ProductStock = ActiveCommerce.Products.Stock.ProductStock;

namespace ActiveCommerce.Training.RealTimeStock
{
    public class ProductStockManager : ActiveCommerce.Products.Stock.ProductStockManager
    {
        public ProductStockManager(Sitecore.Ecommerce.ShopContext shopContext, IProductStockRepository<ProductStock> productStockRepository, IProductRepository productRepository) : base(shopContext, productStockRepository, productRepository)
        {
        }

        public override Sitecore.Ecommerce.DomainModel.Products.ProductStock GetStock(Sitecore.Ecommerce.DomainModel.Products.ProductStockInfo stockInfo)
        {
            var client = new Services.ProductServiceClient();
            var stock = client.GetStock(stockInfo.ProductCode);
            return new Sitecore.Ecommerce.DomainModel.Products.ProductStock
            {
                Code = stockInfo.ProductCode,
                Stock = stock
            };
        }

        public override void Update(Sitecore.Ecommerce.DomainModel.Products.ProductStockInfo stockInfo, System.Linq.Expressions.Expression<Func<long, long>> expression)
        {
            return;
        }

        public override void Update(Sitecore.Ecommerce.DomainModel.Products.ProductStockInfo stockInfo, long newAmount)
        {
            return;
        }
    }
}