using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Ecommerce.DomainModel.Products;
using Sitecore.SecurityModel;
using Microsoft.Practices.Unity;

namespace ActiveCommerce.Training.Web.sitecore.admin
{
    public partial class performance : System.Web.UI.Page
    {
        private const int itemsToCreate = 500;
        private readonly ID _bucketId = new ID("{1C8CCF13-1C38-4FAB-9C9D-898D6CBEC35C}");
        private readonly ID _nonBucketId = new ID("{468F546B-1ADA-4DCB-96B2-B538138D3C74}");
        private readonly TemplateID _template = new TemplateID(new ID("{1D8E9C9B-F494-48E1-B400-EED3CD25833C}"));
        private const string fieldName = "TestField";

        protected void Page_Load(object sender, EventArgs e)
        {
            var myobj = Sitecore.Ecommerce.Context.Entity.Resolve<IProductRepository>();
        }

        protected void Run_Click(object sender, EventArgs e)
        {
            this.uxDone.Visible = false;
            this.uxResults.Visible = false;
            uxNumItems.Text = string.Format("{0} items", itemsToCreate);
            using (new SecurityDisabler())
            {
                var master = Sitecore.Data.Database.GetDatabase("master");
                var bucket = master.Items[_bucketId];
                var nonBucket = master.Items[_nonBucketId];
                var bucketItems = CreateItems(bucket, uxBucketCreate);
                EditItems(bucketItems, bucket.Name, uxBucketEdit);
                DeleteItems(bucketItems, bucket.Name, uxBucketDelete);
                var nonBucketItems = CreateItems(nonBucket, uxNonBucketCreate);
                EditItems(nonBucketItems, nonBucket.Name, uxNonBucketEdit);
                DeleteItems(nonBucketItems, nonBucket.Name, uxNonBucketDlete);
            }
            this.uxDone.Visible = true;
            this.uxResults.Visible = true;
        }

        protected IList<Item> CreateItems(Item parent, Label results)
        {
            var items = new List<Item>();
            var template = new TemplateID(_template);
            var start = DateTime.Now;

            for (var i = 0; i < itemsToCreate; i++)
            {
                var item = parent.Add("test", _template);
                items.Add(item);
            }

            var end = DateTime.Now;
            var elapsed = (end - start).TotalMilliseconds;
            var average = elapsed/itemsToCreate;
            Sitecore.Diagnostics.Log.Info(string.Format("PERFORMANCE TEST | {0}: Created {1} items in {2}ms (avg {3}ms)", parent.Name, itemsToCreate, elapsed, average), this);
            results.Text = string.Format("{0}ms (avg {1}ms)", elapsed, average);
            return items;
        }

        protected void EditItems(IList<Item> items, string parentName, Label results)
        {
            var count = items.Count();
            var start = DateTime.Now;

            foreach (var item in items)
            {
                using (new EditContext(item))
                {
                    item[fieldName] = "hello world";
                }
            }

            var end = DateTime.Now;
            var elapsed = (end - start).TotalMilliseconds;
            var average = elapsed / count;
            Sitecore.Diagnostics.Log.Info(string.Format("PERFORMANCE TEST | {0}: Edited {1} items in {2}ms (avg {3}ms)", parentName, count, elapsed, average), this);
            results.Text = string.Format("{0}ms (avg {1}ms)", elapsed, average);
        }

        protected void DeleteItems(IList<Item> items, string parentName, Label results)
        {
            var count = items.Count();
            var start = DateTime.Now;

            foreach (var item in items)
            {
                item.Delete();
            }

            var end = DateTime.Now;
            var elapsed = (end - start).TotalMilliseconds;
            var average = elapsed/count;
            Sitecore.Diagnostics.Log.Info(string.Format("PERFORMANCE TEST | {0}: Deleted {1} items in {2}ms (avg {3}ms)", parentName, count, elapsed, average), this);
            results.Text = string.Format("{0}ms (avg {1}ms)", elapsed, average);
        }

    }
}