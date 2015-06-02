using System;

namespace ActiveCommerce.Training.InvoicePayment.CheckOut
{
    [Serializable]
    public class CheckOut : ActiveCommerce.CheckOuts.CheckOut, IInvoicePayment
    {
        public string PurchaseOrderNumber { get; set; }
    }
}