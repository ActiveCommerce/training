using ActiveCommerce.Prices.Pipelines.GetProductTotals;

namespace ActiveCommerce.Training.PriceRules.Rules
{
    public class PricingRuleContext : ActiveCommerce.Rules.CartRuleContext
    {
        public GetProductTotalsArgs GetProductTotalsArgs { get; set; }
    }
}