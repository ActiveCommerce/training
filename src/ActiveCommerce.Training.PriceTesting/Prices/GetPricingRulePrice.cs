using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.Training.PriceTesting.Rules;
using Microsoft.Practices.Unity;
using ActiveCommerce.Extensions;
using Sitecore.Diagnostics;
using Sitecore.Ecommerce.DomainModel.Carts;

namespace ActiveCommerce.Training.PriceTesting.Prices
{
    public class GetPricingRulePrice : ActiveCommerce.Prices.Pipelines.GetProductTotals.GetNormalPrice
    {
        public override void Process(ActiveCommerce.Prices.Pipelines.GetProductTotals.GetProductTotalsArgs args)
        {
            var shopContext = Sitecore.Ecommerce.Context.Entity.Resolve<Sitecore.Ecommerce.ShopContext>();
            var settings = shopContext.GetGeneralSettings() as ActiveCommerce.Training.PriceTesting.Configuration.GeneralSettings;
            if (settings == null)
            {
                Sitecore.Diagnostics.Log.Warn("GetPricingRulePrice: General Settings did not resolve to correct type", this);
                return;
            }

            var ruleContext = new PricingRuleContext
                              {
                                  Cart = Sitecore.Ecommerce.Context.Entity.GetInstance<ShoppingCart>() as ActiveCommerce.Carts.ShoppingCart,
                              };
            settings.PricingRules.Run(ruleContext);
            if (string.IsNullOrEmpty(ruleContext.PriceLevel))
            {
                return;
            }

            PriceName = ruleContext.PriceLevel;
            var price = GetPrice(args.Product.PriceXml);
            if (!price.HasValue)
            {
                return;
            }

            args.Totals.PriceExVat = price.Value;
            args.Totals.DiscountExVat = 0;
            args.HasPrice = true;
        }
    }
}