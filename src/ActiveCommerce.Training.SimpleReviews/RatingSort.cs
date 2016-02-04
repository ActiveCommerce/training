using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.Products;
using ActiveCommerce.Training.SimpleReviews.Extensions;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace ActiveCommerce.Training.SimpleReviews
{
    [SitecoreType(TemplateId = "{726BAEC6-5CDE-40E4-A269-92ACF73A857B}")]
    public class RatingSort : ActiveCommerce.Products.Sorting.IProductSort
    {
        [SitecoreId]
        public virtual Guid ID { get; set; }

        [SitecoreField(FieldId = Constants.FieldIds.ProductSort.DisplayValue)]
        public virtual string DisplayValue { get; set; }


        public virtual void PopulateSearchOptions(Products.RepositorySearchOptions searchOptions)
        {
            searchOptions.ReverseSort = true;
            searchOptions.SortFunc = x => GetRating(x);
        }

        protected virtual double GetRating(ProductBaseData product)
        {
            var average = product.GetAverageRating();
            return average ?? double.MinValue;
        }
    }
}