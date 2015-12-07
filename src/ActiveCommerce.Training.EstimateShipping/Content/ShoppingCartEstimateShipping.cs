using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace ActiveCommerce.Training.EstimateShipping.Content
{
    [SitecoreType(TemplateId = "{2F93C268-0D3A-46E2-B2D1-3E6981E3F8C0}")]
    public class ShoppingCartEstimateShipping
    {
        [SitecoreField(FieldName = "Estimate Shipping Header")]
        public virtual string EstimateShippingHeader { get; set; }

        [SitecoreField(FieldName = "Disclaimer")]
        public virtual string Disclaimer { get; set; }

        [SitecoreField(FieldName = "Submit Button")]
        public virtual string SubmitButton { get; set; }

        [SitecoreField(FieldName = "Zip Code Label")]
        public virtual string ZipCodeLabel { get; set; }

        [SitecoreField(FieldName = "Zip Code Placeholder")]
        public virtual string ZipCodePlaceholder { get; set; }
    }
}