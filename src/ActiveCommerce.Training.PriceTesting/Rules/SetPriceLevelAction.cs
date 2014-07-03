using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data;
using Sitecore.Rules.Actions;
using Sitecore.StringExtensions;

namespace ActiveCommerce.Training.PriceTesting.Rules
{
    public class SetPriceLevelAction<T> : RuleAction<T> where T : PricingRuleContext
    {
        public ID ItemId { get; set; }

        public override void Apply(T ruleContext)
        {
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

            ruleContext.PriceLevel = item.Name;
        }
    }
}