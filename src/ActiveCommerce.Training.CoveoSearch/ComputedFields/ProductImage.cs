using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using ActiveCommerce.Extensions;
using ActiveCommerce.Products;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Diagnostics;
using Sitecore.Sites;

namespace ActiveCommerce.Training.CoveoSearch.ComputedFields
{
    public class ProductImage : AbstractProductComputedField
    {
        public ProductImage(XmlNode config) : base(config)
        {

        }

        protected override object ComputeFieldValue(ProductBaseData product, IIndexable indexable)
        {
            var fullProduct = product as Product;
            if (fullProduct == null)
            {
                return null;
            }
            var image = fullProduct.Image;
            if (image == null)
            {
                return null;
            }
            return image.GetUri(166, 166);
        }
    }
}