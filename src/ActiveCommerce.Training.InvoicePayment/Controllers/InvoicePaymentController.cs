using System;
using System.Web.Mvc;
using ActiveCommerce.Carts;
using ActiveCommerce.Payments;
using ActiveCommerce.Training.InvoicePayment.Payments;
using ActiveCommerce.Web.Filters;
using Microsoft.Practices.Unity;
using Sitecore.Diagnostics;

namespace ActiveCommerce.Training.InvoicePayment.Controllers
{
    [NoCache]
    [NoSitecoreAnalytics]
    [EnforceCartNotEmpty]
    [RequireHttpsIfEnabled]
    public class InvoicePaymentController : ActiveCommerce.Web.Controllers.AppControllerBase
    {
        protected virtual ShoppingCart ShoppingCart
        {
            get
            {
                return Sitecore.Ecommerce.Context.Entity.GetInstance<Sitecore.Ecommerce.DomainModel.Carts.ShoppingCart>() as ShoppingCart;
            }
        }

        protected virtual PaymentFactory PaymentFactory
        {
            get
            {
                return Sitecore.Ecommerce.Context.Entity.Resolve<PaymentFactory>();
            }
        }

        public virtual ActionResult UpdatePurchaseOrderNumber(string code, string purchaseOrderNumber)
        {
            if (string.IsNullOrEmpty(purchaseOrderNumber))
            {
                return JsonError("purchaseOrderNumber is null or empty");
            }
            try
            {
                var cart = ShoppingCart;
                // make sure the primary payment is InvoicePayment
                if (cart.PrimaryPayment == null || cart.PrimaryPayment.System.Code != code)
                {
                    var payment = PaymentFactory.Create(code);
                    cart.PrimaryPayment = payment;
                }
                Assert.IsTrue(cart.PrimaryPayment.Provider is InvoicePaymentOption, "Payment provider must be of type InvoicePaymentOption");
                var paymentDetails = Assert.ResultNotNull(cart.PrimaryPayment.Details as InvoicePaymentDetails, "PaymentDetails must be of type InvoicePaymentDetails");
                paymentDetails.PurchaseOrderNumber = purchaseOrderNumber;
            }
            catch (Exception e)
            {
                return JsonError(e.Message);
            }
            return Json(true);
        }
    }
}