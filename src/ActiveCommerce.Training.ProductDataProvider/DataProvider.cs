using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Sitecore.Caching;
using Sitecore.Data;
using Sitecore.StringExtensions;

namespace ActiveCommerce.Training.ProductDataProvider
{
    public class DataProvider : Sitecore.Data.DataProviders.DataProvider
    {
        protected HashSet<ID> Templates = new HashSet<ID>();

        public string DataPath { get; set; }

        protected XElement Data
        {
            get
            {
                return XElement.Load(Sitecore.IO.FileUtil.MapPath(DataPath));
            }
        }

        public DataProvider()
        {
            this.CacheOptions.DisableAll = true;
        }

        public void AddTemplate(string template)
        {
            if (!ID.IsID(template))
            {
                return;
            }
            Templates.Add(ID.Parse(template));
        }

        public override ItemDefinition GetItemDefinition(ID itemId, Sitecore.Data.DataProviders.CallContext context)
        {
            var itemDefinition = context.CurrentResult as ItemDefinition;
            if (itemDefinition == null || !Templates.Contains(itemDefinition.TemplateID))
            {
                return null;
            }

            //ideally we would find a way of clearing the items from Sitecore caches when data changes instead
            (itemDefinition as ICacheable).Cacheable = false;
            context.DataManager.Database.Caches.DataCache.RemoveItemInformation(itemDefinition.ID);
            context.DataManager.Database.Caches.ItemCache.RemoveItem(itemDefinition.ID);
            return null;
        }

        public override FieldList GetItemFields(ItemDefinition itemDefinition, VersionUri versionUri, Sitecore.Data.DataProviders.CallContext context)
        {
            if (!Templates.Contains(itemDefinition.TemplateID))
            {
                return null;
            }

            var currentFields = context.CurrentResult as FieldList;
            var productCode = currentFields != null ? currentFields[ActiveCommerce.TemplateFields.ProductCode] : null;
            if (productCode.IsNullOrEmpty())
            {
                return null;
            }

            var book = Data.Elements("book").SingleOrDefault(x => x.Attribute("id").Value == productCode);
            if (book == null)
            {
                return null;
            }

            var fields = new FieldList();
            fields.Add(FieldIDs.Author, book.Element("author").Value);
            fields.Add(FieldIDs.Genre, book.Element("genre").Value);
            var dateValue = DateTime.ParseExact(book.Element("publish_date").Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            fields.Add(FieldIDs.PublishDate, Sitecore.DateUtil.ToIsoDate(dateValue));
            return fields;
        }
    }
}