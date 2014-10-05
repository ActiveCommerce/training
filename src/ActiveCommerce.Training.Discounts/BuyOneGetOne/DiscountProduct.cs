using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.Rules;

namespace ActiveCommerce.Training.Discounts.BuyOneGetOne
{
    public class DiscountProduct<T> : ActiveCommerce.Rules.Actions.PromoActions.DiscountProductAction<T> where T : PromoRuleContext
    {
        public string BogoName { get; set; }

        public uint Multiplier { get; set; }

        public string MaxCountString { get; set; }

        public uint MaxCount
        {
            get
            {
                uint count = 0;
                uint.TryParse(MaxCountString, out count);
                return count;
            }
        }

        public DiscountProduct()
        {
            BogoName = Constants.DefaultBogoName;
            Multiplier = 1;
        }

        public override void Apply(T ruleContext)
        {
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
            var toDiscount = count * Multiplier;
            toDiscount = MaxCount > 0 && MaxCount < toDiscount ? MaxCount : toDiscount;
            CountString = toDiscount.ToString();
            base.Apply(ruleContext);
        }
    }
}