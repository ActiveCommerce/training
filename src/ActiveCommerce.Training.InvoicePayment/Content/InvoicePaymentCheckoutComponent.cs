using Glass.Mapper.Sc.Configuration.Attributes;

namespace ActiveCommerce.Training.InvoicePayment.Content
{
    public class InvoicePaymentCheckoutComponent : ActiveCommerce.Content.Checkout.CheckoutComponents.CheckoutComponent
    {
        [SitecoreField(FieldName = "PO Number")]
        public string PurchaseOrderNumber { get; set; }
    }
}