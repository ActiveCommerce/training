using ActiveCommerce.Currencies;
using ActiveCommerce.Rules.Actions;
using Microsoft.Practices.Unity;

namespace ActiveCommerce.Training.PriceRules.Rules.Actions
{
    public class ApplyDiscountAction<T> : DiscountAction<T> where T : PricingRuleContext
    {
        protected virtual ICurrencyRounder CurrencyRounder
        {
            get
            {
                return Sitecore.Ecommerce.Context.Entity.Resolve<ICurrencyRounder>();
            }
        }

        public override void Apply(T ruleContext)
        {
            var args = ruleContext.GetProductTotalsArgs;
            if (!args.HasPrice || args.Totals.PriceExVat <= 0)
            {
                return;
            }

            var discount = GetDiscount(string.Empty, string.Empty);
            discount.Recalculate(args.Totals.PriceExVat, CurrencyRounder, args.Currency);
            if (discount.TotalDiscount <= 0) return;

            args.Totals.PriceExVat = args.Totals.PriceExVat - discount.TotalDiscount;
            args.Totals.DiscountExVat = args.Totals.DiscountExVat + discount.TotalDiscount;
        }
    }
}