using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Sitecore.Caching;
using Sitecore.Data;
using Sitecore.Data.DataProviders;
using Sitecore.Data.Items;
using Sitecore.StringExtensions;

namespace ActiveCommerce.Training.ProductDataProvider
{
    public class MongoDataProvider : Sitecore.Data.DataProviders.DataProvider
    {
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
                //TODO: flag product as hidden?
                return null;
            }

            //first time seeing this product -- link it in the MongoDB
            if (!book.Contains("sitecoreId"))
            {
                collection.Update(query, Update.Set("sitecoreId", itemDefinition.ID.ToShortID().ToString()));
            }

            //map fields
            //TODO: more dynamic field mapping
            var fields = new FieldList();
            fields.Add(FieldIDs.Author, book["author"].AsString);
            fields.Add(FieldIDs.Genre, book["genre"].AsString);
            var dateValue = DateTime.ParseExact(book["publishDate"].AsString, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            fields.Add(FieldIDs.PublishDate, Sitecore.DateUtil.ToIsoDate(dateValue));
            return fields;
        }
    }
}