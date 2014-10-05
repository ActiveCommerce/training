using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.Rules;
using Sitecore.Diagnostics;
using Sitecore.Rules.Actions;

namespace ActiveCommerce.Training.Discounts.BuyOneGetOne
{
    public class DivideCount<T> : RuleAction<T> where T : PromoRuleContext
    {
        public string BogoName { get; set; }

        public uint DivideBy { get; set; }

        public DivideCount()
        {
            DivideBy = 1;
        }

        public override void Apply(T ruleContext)
        {
            Assert.ArgumentNotNull(ruleContext, "ruleContext");
            var countParam = string.Format(Constants.BogoCountParam, BogoName);
            if (!ruleContext.Parameters.ContainsKey(countParam))
            {
                return;
            }
            var count = ruleContext.Parameters[countParam] as uint?;
            if (count == null || count == 0)
            {
                return;
            }
            count = count/DivideBy;
            ruleContext.Parameters[countParam] = count;
        }
    }
}