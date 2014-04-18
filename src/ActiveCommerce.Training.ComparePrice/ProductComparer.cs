using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.Extensions;
using ActiveCommerce.Products;
using ActiveCommerce.Products.Comparison;
using ProductBaseData = Sitecore.Ecommerce.DomainModel.Products.ProductBaseData;

namespace ActiveCommerce.Training.ComparePrice
{
    public class ProductComparer<T> : ActiveCommerce.Products.Comparison.ProductComparer<T> where T : Product
    {
        protected override Products.Comparison.CompareValue[] GetValues(Sitecore.Data.Items.Item item, Products.Comparison.ICompareField field)
        {
            var product = base.ProductRepository.Get<ProductBaseData>(item[ActiveCommerce.TemplateFields.ProductCode]);
            if (field.FieldName.Equals("price", StringComparison.InvariantCultureIgnoreCase))
            {
                var totals = product.GetProductTotals();
                return new[] { new CompareValue { Value = totals.PriceDisplay } };
            }
            return base.GetValues(item, field);
        }
    }
}