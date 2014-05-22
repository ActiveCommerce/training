using System;
using System.Xml.Linq;
using ActiveCommerce.ShopContext;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Ecommerce;
using Sitecore.Ecommerce.DomainModel.Products;
using Sitecore.SecurityModel;
using Sitecore.Tasks;

namespace ActiveCommerce.Training.ProductStockUpdate
{
    public class ProductStockUpdate
    {
        public void UpdateStock(Item[] itemArray, CommandItem commandItem, ScheduleItem scheduleItem)
        {
            var repository = Sitecore.Ecommerce.Context.Entity.Resolve<IProductRepository>();
            var schedule = new ActiveCommerce.SitecoreX.ScheduledTasks.ExtendedScheduleItem(scheduleItem);
            var file = schedule.Arguments["file"];


            using (new SecurityDisabler())
            {
                using (new ShopContextSwitcher(schedule.SiteContext, schedule.Database))
                {
                    Sitecore.Diagnostics.Log.Info("Executing import...", this);

                    var stockManager = Sitecore.Ecommerce.Context.Entity.Resolve<IProductStockManager>();
                    var xml = XElement.Load(Sitecore.IO.FileUtil.MapPath(file));
                    var books = xml.Descendants("book");
                    foreach (var book in books)
                    {
                        long stock = Int64.Parse(book.Element("stock").Value);
                        stockManager.Update(new ProductStockInfo
                                            {
                                                ProductCode = book.Attribute("id").Value
                                            }, stock);
                    }
                }
            }
        }
    }
}