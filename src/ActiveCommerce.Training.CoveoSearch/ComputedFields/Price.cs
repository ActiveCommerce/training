using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using ActiveCommerce.Extensions;
using ActiveCommerce.Products;
using ActiveCommerce.ShopContext;
using Microsoft.Practices.Unity;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Diagnostics;
using Sitecore.Shell.Framework.Commands.Masters;
using Sitecore.Sites;

namespace ActiveCommerce.Training.CoveoSearch.ComputedFields
{
    public class Price : AbstractProductComputedField
    {
        public override string ReturnType
        {
            get
            {
                return "Number";
            }
            set { }
        }

        public Price(XmlNode config) : base(config)
        {

        }

        protected override object ComputeFieldValue(ProductBaseData product, IIndexable indexable)
        {
            var pricing = product.GetDisplayTotals();
            if (pricing == null)
            {
                return null;
            }
            return pricing.MinPrice;
        }
    }
}