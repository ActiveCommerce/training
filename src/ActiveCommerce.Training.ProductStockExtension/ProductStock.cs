using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.ProductStockExtension
{
    public class ProductStock : ActiveCommerce.Products.Stock.ProductStock
    {
        public virtual long RegionSpecificStock { get; set; }
        public virtual long OutOfStockThreshold { get; set; }
    }
}