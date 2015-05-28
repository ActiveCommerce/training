using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.OrderExtension
{
    public class PurchaseOrderPaymentMeans : ActiveCommerce.Orders.PaymentMeans
    {
        public virtual string PurchaseOrderNumber { get; set; }
    }
}