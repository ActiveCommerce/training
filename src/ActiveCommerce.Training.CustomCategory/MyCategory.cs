using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace ActiveCommerce.Training.CustomCategory
{
    [SitecoreType(TemplateId = "{3F4588FD-0F17-4A1C-9C66-FB85E70AAEF6}")]
    public class MyCategory : ActiveCommerce.Products.Browsing.ProductCategory, IMyCategoryBase
    {
        [SitecoreField]
        public string MyField { get; set; }
    }
}