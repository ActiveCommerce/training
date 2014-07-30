using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;

namespace ActiveCommerce.Training.Web.sitecore.admin
{
    public partial class performance : System.Web.UI.Page
    {
        private int _itemsToCreate = 500;
        private int _numEdits = 1;
        private bool _delete = true;
        private bool _bucketTest = true;
        private bool _nonBucketTest = true;
        private bool _query = true;
        private bool _saveTwice = false;
        private readonly ID _bucketId = new ID("{1C8CCF13-1C38-4FAB-9C9D-898D6CBEC35C}");
        private readonly ID _nonBucketId = new ID("{468F546B-1ADA-4DCB-96B2-B538138D3C74}");
        private readonly ID _singleItemId = new ID("{4332CABD-9B01-4846-B5C1-F8A3ACFF1FDD}");
        private readonly TemplateID _template = new TemplateID(new ID("{1D8E9C9B-F494-48E1-B400-EED3CD25833C}"));
        private const string fieldName = "TestField";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["num"] != null)
            {
                _itemsToCreate = int.Parse(Request.QueryString["num"]);
            }
            if (Request.QueryString["edits"] != null)
            {
                _numEdits = int.Parse(Request.QueryString["edits"]);
            }
            if (Request.QueryString["delete"] == "0")
            {
                _delete = false;
            }
            if (Request.QueryString["query"] == "0")
            {
                _query = false;
            }
            if (Request.QueryString["bucket"] == "0")
            {
                _bucketTest = false;
            }
            if (Request.QueryString["nonbucket"] == "0")
            {
                _nonBucketTest = false;
            }
            if (Request.QueryString["save"] == "2")
            {
                _saveTwice = true;
            }
            if (Request.QueryString["auto"] == "1")
            {
                RunTest();
            }

        }

        protected void Run_Click(object sender, EventArgs e)
        {
            this.uxDone.Visible = false;
            this.uxResults.Visible = false;
            uxNumItems.Text = string.Format("{0} items", _itemsToCreate);
            RunTest();
        }

        protected void RunTest()
        {
            using (new SecurityDisabler())
            {
                var master = Sitecore.Data.Database.GetDatabase("master");
                var bucket = master.Items[_bucketId];
                var nonBucket = master.Items[_nonBucketId];
                var singleItem = master.Items[_singleItemId];
                EditSingleItem(singleItem);
                if (_bucketTest)
                {
                    var bucketItems = CreateItems(bucket, uxBucketCreate);
                    EditItems(bucketItems, bucket.Name, uxBucketEdit);
                    if (_query)
                        QueryItems(bucket, uxBucketQuery);
                    if (_delete)
                        DeleteItems(bucketItems, bucket.Name, uxBucketDelete);
                }
                if (_nonBucketTest)
                {
                    var nonBucketItems = CreateItems(nonBucket, uxNonBucketCreate);
                    EditItems(nonBucketItems, nonBucket.Name, uxNonBucketEdit);
                    if (_query)
                        QueryItems(nonBucket, uxNonBucketQuery);
                    if (_delete)
                        DeleteItems(nonBucketItems, nonBucket.Name, uxNonBucketDlete);
                }
            }
            this.uxDone.Visible = true;
            this.uxResults.Visible = true;
        }

        protected void EditSingleItem(Item item)
        {
            for (int i = 0; i < _numEdits; i++)
            {
                using (new EditContext(item))
                {
                    if (_saveTwice)
                        item.Editing.BeginEdit();
                    item[fieldName] = "hello world " + System.Guid.NewGuid();
                    if (_saveTwice)
                        item.Editing.EndEdit(true, false);
                }
            }
        }

        protected IList<Item> CreateItems(Item parent, Label results)
        {
            var items = new List<Item>();
            var template = new TemplateID(_template);
            var start = DateTime.Now;

            for (var i = 0; i < _itemsToCreate; i++)
            {
                var item = parent.Add("test", _template);
                items.Add(item);
            }

            var end = DateTime.Now;
            var elapsed = (end - start).TotalMilliseconds;
            var average = elapsed/_itemsToCreate;
            Sitecore.Diagnostics.Log.Info(string.Format("PERFORMANCE TEST | {0}: Created {1} items in {2}ms (avg {3}ms)", parent.Name, _itemsToCreate, elapsed, average), this);
            results.Text = string.Format("{0}ms (avg {1}ms)", elapsed, average);
            return items;
        }

        protected void EditItems(IList<Item> items, string parentName, Label results)
        {
            var count = items.Count();
            var start = DateTime.Now;

            foreach (var item in items)
            {
                for (int i = 0; i < _numEdits; i++)
                {
                    using (new EditContext(item))
                    {
                        if (_saveTwice)
                            item.Editing.BeginEdit();
                        item[fieldName] = "hello world " + System.Guid.NewGuid();
                        if (_saveTwice)
                            item.Editing.EndEdit(true, false);
                    }
                }
            }

            var end = DateTime.Now;
            var elapsed = (end - start).TotalMilliseconds;
            var average = elapsed / count;
            Sitecore.Diagnostics.Log.Info(string.Format("PERFORMANCE TEST | {0}: Edited {1} items in {2}ms (avg {3}ms)", parentName, count, elapsed, average), this);
            results.Text = string.Format("{0}ms (avg {1}ms)", elapsed, average);
        }

        protected void QueryItems(Item parent, Label results)
        {
            var start = DateTime.Now;
            parent.Axes.SelectItems(string.Format("//*[@{0}='{1}']", fieldName, "hello world"));
            var end = DateTime.Now;
            var elapsed = (end - start).TotalMilliseconds;
            Sitecore.Diagnostics.Log.Info(string.Format("PERFORMANCE TEST | {0}: Queried {1} items in {2}ms", parent.Name, _itemsToCreate, elapsed), this);
            results.Text = string.Format("{0}ms", elapsed);
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