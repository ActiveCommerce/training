using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.Content.Checkout.CheckoutComponents;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace ActiveCommerce.GiftMessage.Content
{
    public class GiftMessageCheckoutComponent : ICheckoutComponent
    {
        [SitecoreId]
        public Guid ID
        {
            get;
            set;
        }

        [SitecoreInfo(SitecoreInfoType.Language)]
        public Sitecore.Globalization.Language Language
        {
            get;
            set;
        }

        [SitecoreInfo(SitecoreInfoType.Version)]
        public int Version
        {
            get;
            set;
        }

        [SitecoreInfo(SitecoreInfoType.Url)]
        public string Url
        {
            get;
            set;
        }

        [SitecoreField(FieldName = "Title")]
        public string Title
        {
            get;
            set;
        }

        [SitecoreField(FieldName = "Header Instructions")]
        public string HeaderInstructions
        {
            get;
            set;
        }

        [SitecoreField(FieldName = "Instructions")]
        public string Instructions
        {
            get;
            set;
        }

        [SitecoreField(FieldName = "Gift Message")]
        public string GiftMessage
        {
            get;
            set;
        }
    }
}