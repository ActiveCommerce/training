using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace ActiveCommerce.Training.ProductStockExtension.Mappings
{
    public class ProductStockMap : SubclassMap<ProductStock>
    {
        public ProductStockMap()
        {
            Table("ProductStockExtension");
            Map(x => x.RegionSpecificStock);
            Map(x => x.OutOfStockThreshold);
        }
    }
}