using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCommerce.Training.CustomProduct
{
    [SitecoreType]
    public class BookProduct : ActiveCommerce.Products.Product
    {
        [SitecoreField]
        public string Author { get; set; }

        [SitecoreField]
        public string Genre { get; set; }

        [SitecoreField(FieldName = "Publish Date")]
        public DateTime PublishDate { get; set; }
    }
}