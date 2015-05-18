using ActiveCommerce.Training.PriceRules.Rules;
using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Rules;

namespace ActiveCommerce.Training.PriceRules.Configuration
{
    [SitecoreType]
    public class GeneralSettings : ActiveCommerce.Configurations.GeneralSettings
    {
        [SitecoreField(FieldName="Pricing Rules")]
        public RuleList<PricingRuleContext> PricingRules { get; set; }
    }
}