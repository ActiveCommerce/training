using System.Web;
using ActiveCommerce.Extensions;
using ActiveCommerce.Products;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Ecommerce;
using Sitecore.Ecommerce.Catalogs;
using Sitecore.Ecommerce.Search;
using Sitecore.Ecommerce.Utils;
using Sitecore.IO;
using Sitecore.Links;
using Sitecore.Text;

namespace ActiveCommerce.Training.ProductUrl
{
    public class NameAndVariantProductUrlProcessor : ProductUrlProcessor
    {
        public Sitecore.Ecommerce.ShopContext ShopContext { get; set; }

        public NameAndVariantProductUrlProcessor(ISearchProvider searchProvider) : base(searchProvider)
        {
        }

        public override UrlString GetProductUrl(Item catalogItem, Item productItem)
        {
            Assert.ArgumentNotNull(catalogItem, "catalogItem");
            Assert.ArgumentNotNull(productItem, "productItem");

            UrlOptions options = UrlOptions.DefaultOptions;
            options.AddAspxExtension = false;
            UrlString urlString = new UrlString(LinkManager.GetItemUrl(catalogItem, options));

            var repository = Sitecore.Ecommerce.Context.Entity.Resolve<IAdvancedProductRepository>();
            var product = repository.GetProduct<Product>(productItem);
            if (product is ProductVariant)
            {
                productItem = (product as ProductVariant).Parent.InnerItem;
                foreach (var opt in (product as ProductVariant).Options)
                {
                    urlString.Parameters.Add(opt.Key, opt.Value.Value);
                }
            }

            string name = Sitecore.MainUtil.EncodeName(productItem.Name);
            string file = HttpUtility.UrlPathEncode(name);
            if (urlString.Parameters.Count > 0)
            {
                file += "#/"; // Tack on hash if we have option parameters
            }
            string str = FileUtil.MakePath(urlString.Path, file, '/');
            urlString.Path = str;
            if (UrlOptions.DefaultOptions.AddAspxExtension)
            {
                urlString.Path = urlString.Path + ".aspx";
            }
            return urlString;
        }

        public override Item ResolveProductItem(params string[] arguments)
        {
            Assert.ArgumentNotNull(arguments, "arguments");
            Assert.IsNotNull(ShopContext, "ShopContext");
            var settings = ShopContext.GetBusinessSettings();

            string name = arguments[0];
            Assert.IsNotNullOrEmpty(name, "name");
            name = Sitecore.MainUtil.DecodeName(name);
            Item item = Sitecore.Context.Database.GetItem(settings.ProductsLink);
            Assert.IsNotNull(item, "Products root item cannot be null.");
            Query query = new Query
            {
                SearchRoot = item.ID.ToString()
            };
            query.AppendField("_name", name, MatchVariant.Exactly);

            Item result;
            foreach (Item current in this.SearchProvider.Search(query, Sitecore.Context.Database))
            {
                if (ProductRepositoryUtil.IsBasedOnTemplate(current.Template, new ID(this.ProductTemplateId)))
                {
                    result = current;
                    return result;
                }
            }
            return null;
        }
    }
}