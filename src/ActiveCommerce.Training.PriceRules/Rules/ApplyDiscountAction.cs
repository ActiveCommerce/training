using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Rules.Actions;

namespace ActiveCommerce.Training.PriceRules.Rules
{
    public class ApplyDiscountAction<T> : RuleAction<T> where T : PricingRuleContext
    {
        public virtual decimal DiscountPercentage { get; set; }

        public override void Apply(T ruleContext)
        {
            var args = ruleContext.GetProductTotalsArgs;
            if (!args.HasPrice || DiscountPercentage <= 0 || args.Totals.PriceExVat <= 0)
            {
                return;
            }

            var discount = args.Totals.PriceExVat*(DiscountPercentage/100);
            args.Totals.PriceExVat = args.Totals.PriceExVat - discount;
            args.Totals.DiscountExVat = args.Totals.DiscountExVat + discount;
        }
    }
}