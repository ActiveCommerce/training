using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace ActiveCommerce.Training.CustomCategory
{
    [SitecoreType(TemplateId = "{01AE2C69-6932-4498-9270-1B99FA200F9A}")]
    public class MyCatalog : ActiveCommerce.Products.Browsing.ProductCatalog, IMyCategoryBase
    {
        [SitecoreField]
        public string MyField { get; set; }
    }
}