using System;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using ActiveCommerce.ShopContext;
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
    public class ProductImportCommand
    {
        public void ImportProducts(Item[] itemArray, CommandItem commandItem, ScheduleItem scheduleItem)
        {
            Sitecore.Diagnostics.Log.Info("Starting import...", this);

            //be sure to populate these values on your schedule item
            var schedule = new ActiveCommerce.SitecoreX.ScheduledTasks.ExtendedScheduleItem(scheduleItem);
            var file = schedule.Arguments["file"];
            var path = schedule.Arguments["path"];
            var templateId = schedule.Arguments["templateId"];

            using (new SecurityDisabler())
            {
                using (new DatabaseSwitcher(schedule.Database))
                {
                    using (new ShopContextSwitcher(schedule.SiteContext))
                    {
                        Sitecore.Diagnostics.Log.Info("Executing import...", this);
                        DoImport(file, path, templateId);
                    }
                }
            }
        }

        protected void DoImport(string file, string path, string templateId)
        {
            //write out argument values to log
            Sitecore.Diagnostics.Log.Warn(string.Format("Importing {0} to {1} with template {2}", file, path, templateId), this);

            var repository = Sitecore.Ecommerce.Context.Entity.Resolve<IProductRepository>();
            var bookParent = Sitecore.Context.Database.Items[path];

            var xml = XElement.Load(Sitecore.IO.FileUtil.MapPath(file));
            var books = xml.Descendants("book");
            foreach (var book in books)
            {
                var id = book.Attribute("id").Value;
                Sitecore.Diagnostics.Log.Info("Importing {0}".FormatWith(id), this);
                Item productItem = null;
                var product = repository.Get<ProductBaseData>(id);
                if (product == null)
                {
                    Sitecore.Diagnostics.Log.Info("Not found, will create.", this);
                    var itemName = ItemUtil.ProposeValidItemName(book.Element("title").Value);
                    productItem = bookParent.Add(itemName, new TemplateID(ID.Parse(templateId)));
                }
                else
                {
                    Sitecore.Diagnostics.Log.Info("Found existing product!", this);
                    productItem = Sitecore.Context.Database.Items[((IEntity)product).Alias];
                }
                using (new EditContext(productItem))
                {
                    productItem["Product Code"] = id;
                    productItem["EAN"] = id;
                    productItem["SKU"] = id;
                    productItem["Title"] = book.Element("title").Value;
                    productItem["Genre"] = book.Element("genre").Value;
                    productItem["Short Description"] = book.Element("description").Value;
                    productItem["Description"] = book.Element("description").Value;
                    productItem["Author"] = book.Element("author").Value;
                    productItem["Weight"] = book.Element("weight").Value;

                    //update publish date
                    var dateField = (DateField)productItem.Fields["Publish Date"];
                    var dateValue = DateTime.ParseExact(book.Element("publish_date").Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    dateField.Value = Sitecore.DateUtil.ToIsoDate(dateValue);

                    //update pricing
                    var priceXml = XElement.Parse(productItem["Price"].ToString());
                    priceXml.Descendants("item")
                            .Where(x => x.Attribute("id").Value == "Normalprice")
                            .Descendants("price").First().Value = book.Element("price").Value;
                    productItem["Price"] = priceXml.ToString();
                }
            }
        }
    }
}