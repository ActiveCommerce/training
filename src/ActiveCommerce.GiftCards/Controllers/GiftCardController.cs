using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActiveCommerce.Carts;
using ActiveCommerce.Web.Filters;
using ActiveCommerce.Web.Controllers;
using Sitecore.Diagnostics;
using Sitecore.Ecommerce;
using ActiveCommerce.GiftCards.Payments;

namespace ActiveCommerce.GiftCards.Controllers
{
    [NoCache]
    [NoSitecoreAnalytics]
    [EnforceCartNotEmpty]
    [RequireHttpsIfEnabled]
    public class GiftCardController : AppControllerBase
    {
        protected virtual ShoppingCart ShoppingCart
        {
            get
            {
                return Context.Entity.GetInstance<Sitecore.Ecommerce.DomainModel.Carts.ShoppingCart>() as ShoppingCart;
            }
        }

        [HttpPost]
        public virtual ActionResult Apply(string code, GiftCardInfo card)
        {
            if (string.IsNullOrEmpty(code))
            {
                return JsonError("code is null or empty");
            }
            if (card == null)
            {
                return JsonError("card details are missing");
            }
            try
            {
                var cart = ShoppingCart;
                var existing = cart.InitialPayments.Any(x => x.Provider is GiftCardPaymentProvider && (x.Details as GiftCardInfo).CardNumber == card.CardNumber);
                if (existing)
                {
                    return JsonError("Checkout-Gift-Card-Already-Added");
                }
                var balanceDue = (cart.Totals as Prices.OrderTotals).BalanceDue;
                if (balanceDue <= 0)
                {
                    return JsonError("Checkout-Gift-Card-Zero-Balance-Due");
                }
                var paymentFactory = Context.Entity.Resolve<ActiveCommerce.Payments.PaymentFactory>();
                var payment = paymentFactory.Create(code);
                var manager = Context.Entity.Resolve<IGiftCardManager>();
                if (!manager.Validate(card))
                {
                    return JsonError("Gift-Card-Not-Valid");
                }
                card.Balance = manager.GetBalance(card);
                if (card.Balance <= 0)
                {
                    return JsonError("Gift-Card-Insufficient-Balance");
                }
                payment.Details = card;
                cart.InitialPayments.Add(payment);
                AdjustPaymentAmounts();
            }
            catch (Exception e)
            {
                return JsonError(e.Message);
            }
            return Json(true);
        }

        [HttpPost]
        public virtual ActionResult Remove(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return JsonError("id is null or empty");
            }
            try
            {
                var cart = ShoppingCart;
                var payment = cart.InitialPayments.SingleOrDefault(x => x.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase));
                if (payment != null)
                {
                    cart.InitialPayments.Remove(payment);
                    AdjustPaymentAmounts();
                }
            }
            catch (Exception e)
            {
                return JsonError(e.Message);
            }
            return Json(true);
        }

        protected virtual void AdjustPaymentAmounts()
        {
            var cart = ShoppingCart;
            
            // reset all to 0 first (i.e. reset balance due)
            foreach (var payment in cart.InitialPayments.Where(x => x.Provider is GiftCardPaymentProvider))
            {
                payment.Amount = 0;
            }

            // now, go through each and grab up to balance due amount
            var balanceDue = (cart.Totals as Prices.OrderTotals).BalanceDue;
            foreach (var payment in cart.InitialPayments.Where(x => x.Provider is GiftCardPaymentProvider))
            {
                var balance = (payment.Details as GiftCardInfo).Balance;
                payment.Amount = balance > balanceDue ? balanceDue : balance;
                balanceDue = balanceDue - payment.Amount;
            }
        }
    }
}