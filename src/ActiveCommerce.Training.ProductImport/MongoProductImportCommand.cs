using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using ActiveCommerce.ShopContext;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Sitecore.Data.Fields;
using Sitecore.Ecommerce;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Ecommerce.DomainModel.Data;
using Sitecore.Ecommerce.DomainModel.Products;
using Sitecore.SecurityModel;
using Sitecore.StringExtensions;
using Sitecore.Tasks;

namespace ActiveCommerce.Training.ProductImport
{
    public class MongoProductImportCommand
    {
        public void ImportProducts(Item[] itemArray, CommandItem commandItem, ScheduleItem scheduleItem)
        {
            Sitecore.Diagnostics.Log.Info("Starting import...", this);

            //be sure to populate these values on your schedule item
            var schedule = new ActiveCommerce.SitecoreX.ScheduledTasks.ExtendedScheduleItem(scheduleItem);
            var connString = schedule.Arguments["connectionString"];
            var collection = schedule.Arguments["collection"];
            var path = schedule.Arguments["path"];
            var templateId = schedule.Arguments["templateId"];

            using (new SecurityDisabler())
            {
                using (new ActiveCommerce.ShopContext.ShopContextSwitcher(schedule.SiteContext, schedule.Database))
                {
                    Sitecore.Diagnostics.Log.Info("Executing import...", this);
                    var client = new MongoClient(connString);
                    var server = client.GetServer();
                    var database = server.GetDatabase(MongoUrl.Create(connString).DatabaseName);
                    var mongoCollection = database.GetCollection(collection);
                    DoImport(mongoCollection, path, templateId);
                }
            }
        }

        protected void DoImport(MongoCollection collection, string path, string templateId)
        {
            //write out argument values to log
            Sitecore.Diagnostics.Log.Warn(string.Format("Importing {0} to {1} with template {2}", collection.Name, path, templateId), this);

            var repository = Sitecore.Ecommerce.Context.Entity.Resolve<IProductRepository>();
            var bookParent = Sitecore.Context.Database.Items[path];

            var books = collection.FindAllAs<BsonDocument>();
            foreach (var book in books)
            {
                var id = book["id"].AsString;
                Sitecore.Diagnostics.Log.Info("Importing {0}".FormatWith(id), this);
                Item productItem = null;
                var product = repository.Get<ProductBaseData>(id);
                if (product == null)
                {
                    Sitecore.Diagnostics.Log.Info("Not found, will create.", this);
                    var itemName = ItemUtil.ProposeValidItemName(book["title"].AsString);
                    productItem = bookParent.Add(itemName, new TemplateID(ID.Parse(templateId)));

                    //update the mongo document with the id of the Sitecore item
                    var query = new QueryDocument("id", id);
                    collection.Update(query, Update.Set("sitecoreId", productItem.ID.ToShortID().ToString()));
                }
                else
                {
                    Sitecore.Diagnostics.Log.Info("Found existing product!", this);
                    productItem = Sitecore.Context.Database.Items[((IEntity)product).Alias];
                }
                using (new EditContext(productItem))
                {
                    productItem["Product Code"] = id;
                }
            }
        }
    }
}