using Sitecore.Data;
using Sitecore.Rules.Actions;
using Sitecore.StringExtensions;

namespace ActiveCommerce.Training.PriceRules.Rules
{
    public class SetPriceLevelAction<T> : RuleAction<T> where T : PricingRuleContext
    {
        public ID ItemId { get; set; }

        public override void Apply(T ruleContext)
        {
            var args = ruleContext.GetProductTotalsArgs;
            if (ItemId == ID.Null)
            {
                Sitecore.Diagnostics.Log.Error("SetPriceLevelAction was used with a null ID", this);
                return;
            }

            var item = Sitecore.Context.Database.Items[ItemId];
            if (item == null)
            {
                Sitecore.Diagnostics.Log.Error("SetPriceLevelAction could not find item {0}".FormatWith(ItemId), this);
                return;
            }

            var priceLevel = item.Name;
            if (!args.Totals.ContainsKey(priceLevel))
            {
                Sitecore.Diagnostics.Log.Error("SetPriceLevelAction could not find price level {0}".FormatWith(priceLevel), this);
                return;
            }
            var price = args.Totals[priceLevel];

            args.Totals.PriceExVat = price;
            args.Totals.DiscountExVat = 0;
            args.HasPrice = true;
        }
    }
}