using ActiveCommerce.Extensions;
using ActiveCommerce.Training.PriceRules.Configuration;
using ActiveCommerce.Training.PriceRules.Rules;
using Microsoft.Practices.Unity;
using Sitecore.Ecommerce.DomainModel.Carts;

namespace ActiveCommerce.Training.PriceRules.Prices
{
    public class RunPricingRules
    {
        public void Process(ActiveCommerce.Prices.Pipelines.GetProductTotals.GetProductTotalsArgs args)
        {
            var shopContext = Sitecore.Ecommerce.Context.Entity.Resolve<Sitecore.Ecommerce.ShopContext>();
            var settings = shopContext.GetGeneralSettings() as GeneralSettings;
            if (settings == null)
            {
                Sitecore.Diagnostics.Log.Warn("GetPricingRulePrice: General Settings did not resolve to correct type", this);
                return;
            }

            var ruleContext = new PricingRuleContext
                              {
                                  Item = args.Product.InnerItem,
                                  Cart = Sitecore.Ecommerce.Context.Entity.GetInstance<ShoppingCart>() as ActiveCommerce.Carts.ShoppingCart,
                                  GetProductTotalsArgs = args
                              };
            settings.PricingRules.Run(ruleContext);
        }
    }
}