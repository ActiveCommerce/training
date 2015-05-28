using System;
using System.Linq;
using ActiveCommerce.Orders;
using ActiveCommerce.Orders.Management;
using ActiveCommerce.Rules;
using Microsoft.Practices.Unity;
using Sitecore.Diagnostics;
using Sitecore.Rules.Conditions;

namespace ActiveCommerce.Training.Discounts.LimitedUse
{
    public class LimitUsePerCustomer<T> : WhenCondition<T> where T : PromoRuleContext
    {
        public uint UseLimit { get; set; }

        protected override bool Execute(T ruleContext)
        {
            Assert.ArgumentNotNull(ruleContext, "ruleContext");
            if (ruleContext.PromoRule == null || string.IsNullOrEmpty(ruleContext.PromoRule.Code))
            {
                return false;
            }
            if (ruleContext.Cart == null || ruleContext.Cart.CustomerInfo == null)
            {
                return false;
            }
            var email = ruleContext.Cart.CustomerInfo.Email;
            if (string.IsNullOrEmpty(email))
            {
                return true;
            }
            var orderManager = Sitecore.Ecommerce.Context.Entity.Resolve<IOrderManager<Order>>();
            var count = orderManager.GetOrders()
                .Count(x => x.BuyerCustomerParty.Party.Contact.ElectronicMail == email &&
                    x.AllowanceCharge.Any(y => !y.ChargeIndicator && y.ID == ruleContext.PromoRule.Code));
            if (count >= UseLimit)
            {
                DiscountMessages.Add("Discount-Redemption-Customer-Limit-Reached", ruleContext.PromoRule.LineDescription, UseLimit);
            }
            return count < UseLimit;
        }
    }
}