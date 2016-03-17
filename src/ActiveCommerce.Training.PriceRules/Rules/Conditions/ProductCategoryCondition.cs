using ActiveCommerce.Extensions;
using Glass.Mapper.Sc;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Rules.Conditions;
using ActiveCommerce.Products.Browsing;

namespace ActiveCommerce.Training.PriceRules.Rules.Conditions
{
    public class ProductCategoryCondition<T> : WhenCondition<T> where T : PricingRuleContext
    {
        /// <summary>
        /// Gets or sets the catalog category item id.
        /// </summary>
        /// <value>The catalog category item id.</value>
        public virtual ID ItemId { get; set; }

        protected override bool Execute(T ruleContext)
        {
            Assert.ArgumentNotNull(ruleContext, "ruleContext");
            if (ruleContext.GetProductTotalsArgs == null || ruleContext.GetProductTotalsArgs.Product == null)
            {
                return false;
            }
            var item = Sitecore.Context.Database.GetItem(ItemId);
            if (item == null)
            {
                return false;
            }

            var sitecoreService = new SitecoreService(item.Database);
            var category = sitecoreService.CreateType<ProductCategory>(item, false, true);
            return ruleContext.GetProductTotalsArgs.Product.IsInCategory(category);
        }
    }
}