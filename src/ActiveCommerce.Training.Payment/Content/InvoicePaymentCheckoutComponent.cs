using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.Content.Checkout.CheckoutComponents;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace ActiveCommerce.Training.Payment.Content
{
    public class InvoicePaymentCheckoutComponent : ActiveCommerce.Content.Checkout.CheckoutComponents.CheckoutComponent
    {
        [SitecoreField(FieldName = "PO Number")]
        public string PurchaseOrderNumber { get; set; }
    }
}