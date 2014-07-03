using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.Training.PriceTesting.Rules;
using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Rules;

namespace ActiveCommerce.Training.PriceTesting.Configuration
{
    public class GeneralSettings : ActiveCommerce.Configurations.GeneralSettings
    {
        [SitecoreField(FieldName="Pricing Rules")]
        public RuleList<PricingRuleContext> PricingRules { get; set; }
    }
}