using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.Payment.CheckOut
{
    [Serializable]
    public class CheckOut : ActiveCommerce.CheckOuts.CheckOut, IInvoicePayment
    {
        public string PurchaseOrderNumber { get; set; }
    }
}