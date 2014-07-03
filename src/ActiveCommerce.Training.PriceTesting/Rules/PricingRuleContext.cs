using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Rules;

namespace ActiveCommerce.Training.PriceTesting.Rules
{
    public class PricingRuleContext : ActiveCommerce.Rules.CartRuleContext
    {
        public string PriceLevel { get; set; }
    }
}