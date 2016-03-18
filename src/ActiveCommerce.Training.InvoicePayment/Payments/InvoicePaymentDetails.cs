using System;
using ActiveCommerce.Payments;
using ActiveCommerce.SitecoreX.Globalization;

namespace ActiveCommerce.Training.InvoicePayment.Payments
{
    [Serializable]
    public class InvoicePaymentDetails : PaymentDetails
    {
        public string PurchaseOrderNumber { get; set; }

        // Override so that the PO number is recorded with the order.
        // Description is written to the PaymentMeans.PaymentDescription.
        public override string Description
        {
            get { return Translator.Format("Invoice-Payment-Description", PurchaseOrderNumber); }
        }
    }
}