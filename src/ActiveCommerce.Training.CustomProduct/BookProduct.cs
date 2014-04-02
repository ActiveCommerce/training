using Glass.Mapper.Sc.Configuration.Attributes;
using System;

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