using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.Products.Stock;
using Sitecore.Ecommerce.DomainModel.Products;

namespace ActiveCommerce.Training.ProductStockExtension
{
    public class ProductStockManager : ActiveCommerce.Products.Stock.ProductStockManager
    {
        public ProductStockManager(Sitecore.Ecommerce.ShopContext shopContext, IProductStockRepository<Products.Stock.ProductStock> productStockRepository, IProductRepository productRepository) : base(shopContext, productStockRepository, productRepository)
        {
        }

        public override Sitecore.Ecommerce.DomainModel.Products.ProductStock GetStock(ProductStockInfo stockInfo)
        {
            var stock = base.GetStock(stockInfo);
            var extended = stock as ProductStock;
            if (extended == null)
            {
                return stock;
            }

            /**
             * Look at a site attribute to see if we should use an alternate stock value. Real-world mapping to
             * regional warehouses would likely require more work here. Depending on your inventory management approach,
             * you might also need to override Update methods to ensure the right stock value is decremented as well on update.
             */
            if (ShopContext.InnerSite.Properties["stockRegion"] == "alternate")
            {
                extended.Stock = extended.RegionSpecificStock;
            }

            /**
             * Check to see if stock levels are below a product-specific threshhold for considering the product out of stock.
             * Allows for a stock "buffer" but may result in lost sales.
             */ 
            if (extended.OutOfStockThreshold > 0 && stock.Stock <= extended.OutOfStockThreshold)
            {
                extended.Stock = 0;
            }

            return extended;
        }
    }
}