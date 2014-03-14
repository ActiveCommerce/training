using System;

namespace ActiveCommerce.Training.RealTimeStock
{
    public class ProductStockManager : Sitecore.Ecommerce.DomainModel.Products.IProductStockManager
    {
        public Sitecore.Ecommerce.DomainModel.Products.ProductStock GetStock(Sitecore.Ecommerce.DomainModel.Products.ProductStockInfo stockInfo)
        {
            var client = new Services.ProductServiceClient();
            var stock = client.GetStock(stockInfo.ProductCode);
            return new Sitecore.Ecommerce.DomainModel.Products.ProductStock
            {
                Code = stockInfo.ProductCode,
                Stock = stock
            };
        }

        public void Update(Sitecore.Ecommerce.DomainModel.Products.ProductStockInfo stockInfo, System.Linq.Expressions.Expression<Func<long, long>> expression)
        {
            return;
        }

        public void Update(Sitecore.Ecommerce.DomainModel.Products.ProductStockInfo stockInfo, long newAmount)
        {
            return;
        }
    }
}