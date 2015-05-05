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
using Sitecore.Sites;

namespace ActiveCommerce.Training.CoveoSearch.ComputedFields
{
    public abstract class AbstractProductComputedField : IComputedIndexField
    {
        public virtual string FieldName { get; set; }

        public virtual string ReturnType { get; set; }

        protected SiteContext SiteContext { get; set; }

        public AbstractProductComputedField(XmlNode config)
        {
            Assert.ArgumentNotNull(config, "config");
            var siteConfig = config.Attributes["site"];
            if (siteConfig != null && !string.IsNullOrEmpty(siteConfig.Value))
            {
                SiteContext = SiteContext.GetSite(siteConfig.Value);
            }
            if (SiteContext == null)
            {
                Sitecore.Diagnostics.Log.Error("Product computed field configured without Site Context", this);
            }
        }

        public object ComputeFieldValue(Sitecore.ContentSearch.IIndexable indexable)
        {
            if (SiteContext == null)
            {
                return null;
            }

            var indexItem = indexable as SitecoreIndexableItem;
            if (indexItem == null)
            {
                return null;
            }

            var item = (Sitecore.Data.Items.Item)indexItem.Item;
            if (item == null)
            {
                return null;
            }

            if (!item.Template.DescendsFrom(TemplateIDs.ProductBase))
            {
                return null;
            }

            using (new ShopContextSwitcher(SiteContext, item.Database))
            {
                var repository = Sitecore.Ecommerce.Context.Entity.Resolve<IAdvancedProductRepository>();
                var product = repository.GetProduct<ProductBaseData>(item);
                if (product == null)
                {
                    return null;
                }
                return ComputeFieldValue(product, indexable);
            }
        }

        protected abstract object ComputeFieldValue(ProductBaseData product, Sitecore.ContentSearch.IIndexable indexable);

    }
}