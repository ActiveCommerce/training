using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.Training.Payment.CheckOut;
using Sitecore.Ecommerce.DomainModel.CheckOuts;
using Sitecore.Ecommerce.DomainModel.Payments;
using Microsoft.Practices.Unity;

namespace ActiveCommerce.Training.Payment
{
    public class InvoicePaymentOption : Sitecore.Ecommerce.DomainModel.Payments.PaymentProvider
    {
        public override void Invoke(Sitecore.Ecommerce.DomainModel.Payments.PaymentSystem paymentSystem, Sitecore.Ecommerce.DomainModel.Payments.PaymentArgs paymentArgs)
        {
            string status;
            var checkout = Sitecore.Ecommerce.Context.Entity.GetInstance<ICheckOut>() as IInvoicePayment;
            if (checkout == null)
            {
                status = "Could not find Purchase Order Number";
                this.PaymentStatus = PaymentStatus.Failure;
                Sitecore.Diagnostics.Log.Warn(string.Format("Could not find {0} checkout data when processing invoice payment", typeof(IInvoicePayment)), this);
            }
            else if (string.IsNullOrWhiteSpace(checkout.PurchaseOrderNumber))
            {
                status = "Invalid Purchase Order";
                this.PaymentStatus = PaymentStatus.Failure;
            }
            //TODO: additional validation of purchase order number could go here if needed
            else
            {
                status = "OK";
                this.PaymentStatus = PaymentStatus.Succeeded;
            }

            var transactionData = Sitecore.Ecommerce.Context.Entity.Resolve<ITransactionData>();
            transactionData.SaveCallBackValues(paymentArgs.ShoppingCart.OrderNumber,
                                               this.PaymentStatus.ToString(),
                                               null,
                                               paymentArgs.ShoppingCart.Totals.TotalPriceIncVat.ToString(),
                                               paymentArgs.ShoppingCart.Currency.Code,
                                               string.Empty,
                                               string.Empty,
                                               status,
                                               string.Empty);
        }

        public override void ProcessCallback(Sitecore.Ecommerce.DomainModel.Payments.PaymentSystem paymentSystem, Sitecore.Ecommerce.DomainModel.Payments.PaymentArgs paymentArgs)
        {
            //not needed for offline payment
            throw new NotImplementedException();
        }
    }
}