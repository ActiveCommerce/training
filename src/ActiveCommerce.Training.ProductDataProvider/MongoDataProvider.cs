using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using Glass.Mapper.Sc.CodeFirst;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Sitecore.Caching;
using Sitecore.Data;
using Sitecore.Data.DataProviders;
using Sitecore.Data.Items;
using Sitecore.Diagnostics.PerformanceCounters;
using Sitecore.Ecommerce.PriceMatrix;
using Sitecore.StringExtensions;

namespace ActiveCommerce.Training.ProductDataProvider
{
    public class MongoDataProvider : Sitecore.Data.DataProviders.DataProvider
    {
        private static class FieldIds
        {
            public static ID Ean = ID.Parse(ActiveCommerce.Products.Constants.FieldIds.Product.EAN);
            public static ID Sku = ID.Parse(ActiveCommerce.Products.Constants.FieldIds.Product.SKU);
            public static ID Title = ID.Parse(ActiveCommerce.Products.Constants.FieldIds.Product.Title);
            public static ID ShortDescription = ID.Parse(ActiveCommerce.Products.Constants.FieldIds.Product.ShortDescription);
            public static ID Description = ID.Parse(ActiveCommerce.Products.Constants.FieldIds.Product.Description);
            public static ID Weight = ID.Parse(ActiveCommerce.Products.Constants.FieldIds.Product.Weight);
            public static ID Price = ID.Parse(ActiveCommerce.Products.Constants.FieldIds.Product.Price);
            public static ID Hidden = ID.Parse(ActiveCommerce.Products.Constants.FieldIds.Product.Hidden);
        }

        private static volatile MongoOplogCacheClearer _cacheClearer;
        private static readonly object _lockObj = new object();

        private bool _initialized;
        private string _connectionString;
        private string _database;
        
        protected HashSet<ID> Templates = new HashSet<ID>();

        public string ConnectionString
        {
            get { return _connectionString; }
            set
            {
                _connectionString = value;
                _database = MongoUrl.Create(value).DatabaseName;
            }
        }

        public string MongoDatabase
        {
            get { return _database; }
        }

        public string Collection { get; set; }

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
            if (_cacheClearer == null)
            {
                lock (_lockObj)
                {
                    if (_cacheClearer == null)
                    {
                        _cacheClearer = new MongoOplogCacheClearer(this.ConnectionString, this.MongoDatabase, this.Collection);
                        new Thread(_cacheClearer.Start).Start();
                    }
                }
            }
            if (!_initialized)
            {
                lock (_lockObj)
                {
                    if (!_initialized)
                    {
                        _cacheClearer.AddDatabase(this.Database);
                        _initialized = true;
                    }
                }
            }
            return null;
        }

        public override FieldList GetItemFields(ItemDefinition itemDefinition, VersionUri versionUri, Sitecore.Data.DataProviders.CallContext context)
        {
            var fields = new FieldList();

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

            var client = new MongoClient(ConnectionString);
            var server = client.GetServer();
            var database = server.GetDatabase(MongoDatabase);
            var collection = database.GetCollection(Collection);

            //look for this product in the mongo collection
            var query = new QueryDocument("id", productCode);
            var book = collection.FindOne(query);
            if (book == null)
            {
                fields.Add(FieldIds.Hidden, "1");
                //TODO: return empty values for other fields
                return fields;
            }

            //map fields
            //TODO: more dynamic field mapping, via config?

            //base product fields
            fields.Add(FieldIds.Ean, productCode);
            fields.Add(FieldIds.Sku, productCode);
            fields.Add(FieldIds.Title, book["title"].AsString);
            fields.Add(FieldIds.ShortDescription, book["description"].AsString);
            fields.Add(FieldIds.Description, book["description"].AsString);
            fields.Add(FieldIds.Weight, book["weight"].AsString);

            //pricing is a little more work
            var priceCategoryItem = new CategoryItem("Normalprice", book["price"].AsString);
            var priceCategory = new Category("Shop");
            priceCategory.AddItem(priceCategoryItem);
            var priceMatrix = new PriceMatrix();
            priceMatrix.AddCategory(priceCategory);
            var serializer = new XmlSerializer(priceMatrix.GetType());
            var priceOutput = new StringBuilder();
            var xmlSettings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true
            };
            var xmlWriter = XmlWriter.Create(priceOutput, xmlSettings);
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            serializer.Serialize(xmlWriter, priceMatrix, namespaces);
            fields.Add(FieldIds.Price, priceOutput.ToString());

            //book fields
            fields.Add(FieldIDs.Author, book["author"].AsString);
            fields.Add(FieldIDs.Genre, book["genre"].AsString);
            var dateValue = DateTime.ParseExact(book["publishDate"].AsString, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            fields.Add(FieldIDs.PublishDate, Sitecore.DateUtil.ToIsoDate(dateValue));
            return fields;
        }
    }
}