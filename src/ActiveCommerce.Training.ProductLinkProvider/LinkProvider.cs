using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.Extensions;
using Sitecore.Data.Items;
using Sitecore.Links;
using ActiveCommerce.Products;
using Microsoft.Practices.Unity;

namespace ActiveCommerce.Training.ProductLinkProvider
{
    public class LinkProvider : Sitecore.Links.LinkProvider
    {
        public override string GetItemUrl(Item item, UrlOptions options)
        {
            if (item[TemplateFields.ProductCode] != null && !string.IsNullOrWhiteSpace(item[TemplateFields.ProductCode]))
            {
                // This is a product
                var repository = Sitecore.Ecommerce.Context.Entity.Resolve<IAdvancedProductRepository>();
                var product = repository.GetProduct<ProductBaseData>(item);
                if (product != null)
                {
                    return product.GetUrl(); 
                }
            }
            // Otherwise, just let base do its thing
            return base.GetItemUrl(item, options);
        }
    }
}