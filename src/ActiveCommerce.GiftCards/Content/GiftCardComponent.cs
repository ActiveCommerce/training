using Glass.Mapper.Sc.Configuration.Attributes;

namespace ActiveCommerce.GiftCards.Content
{
    [SitecoreType(TemplateId = "2738d008-d1c2-4242-b08f-6bde9d8abc3d")]
    public class GiftCardComponent : ActiveCommerce.Content.Checkout.CheckoutComponents.PaymentComponent
    {
        [SitecoreField(FieldName = "Apply Button" )]
        public virtual string ApplyButton { get; set; }
        
        [SitecoreField(FieldName = "Remove Button" )]
        public virtual string RemoveButton { get; set; }
        
        [SitecoreField(FieldName = "Card Number" )]
        public virtual string CardNumber { get; set; }
        
        [SitecoreField(FieldName = "Pin" )]
        public virtual string Pin { get; set; }
    }
}