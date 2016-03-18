using System;
using ActiveCommerce.Payments;
using Sitecore.Ecommerce.DomainModel.Payments;
using Sitecore.Diagnostics;

namespace ActiveCommerce.Training.InvoicePayment.Payments
{
    public class InvoicePaymentOption : PaymentProvider, IHasTransactionDetails, ICreatesPaymentDetails
    {
        private readonly TransactionDetails _transactionDetails = new TransactionDetails();
        public virtual TransactionDetails TransactionDetails
        {
            get { return _transactionDetails; }
        }

        public virtual PaymentDetails CreatePaymentDetails()
        {
            return new InvoicePaymentDetails();
        }

        public override void Invoke(Sitecore.Ecommerce.DomainModel.Payments.PaymentSystem paymentSystem, Sitecore.Ecommerce.DomainModel.Payments.PaymentArgs paymentArgs)
        {
            var args = Assert.ResultNotNull(paymentArgs as ActiveCommerce.Payments.PaymentArgs, "PaymentArgs must be of type ActiveCommerce.Payments.PaymentArgs");
            var details = Assert.ResultNotNull(args.PaymentDetails as InvoicePaymentDetails, "PaymentDetails must be of type InvoicePaymentDetails");

            if (string.IsNullOrWhiteSpace(details.PurchaseOrderNumber))
            {
                TransactionDetails.ProviderMessage = "Invalid Purchase Order";
                PaymentStatus = PaymentStatus.Failure;
                return;
            }

            //TODO: additional validation of purchase order number could go here if needed

            TransactionDetails.ProviderMessage = "OK";
            PaymentStatus = PaymentStatus.Succeeded;
        }

        public override void ProcessCallback(Sitecore.Ecommerce.DomainModel.Payments.PaymentSystem paymentSystem, Sitecore.Ecommerce.DomainModel.Payments.PaymentArgs paymentArgs)
        {
            //not needed for offline payment
            throw new NotImplementedException();
        }
    }
}