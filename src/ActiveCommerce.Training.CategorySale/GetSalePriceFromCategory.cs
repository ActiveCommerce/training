using ActiveCommerce.Prices.Pipelines.GetProductTotals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.Extensions;
using Sitecore.StringExtensions;
using Sitecore.Data.Fields;

namespace ActiveCommerce.Training.CategorySale
{
    public class GetSalePriceFromCategory : IGetProductTotalsProcessor
    {
        public void Process(GetProductTotalsArgs args)
        {
            var product = args.Product;
            var category = product.GetCurrentCategoryItem() ?? product.GetDefaultCategoryItem();
            if (category == null || category["SalePrice"].IsNullOrEmpty())
            {
                return;
            }
            if (!category["FromDate"].IsNullOrEmpty())
            {
                var date = new DateField(category.Fields["FromDate"]);
                if (DateTime.Now < date.DateTime)
                {
                    return;
                }
            }
            if (!category["ToDate"].IsNullOrEmpty())
            {
                var date = new DateField(category.Fields["ToDate"]);
                if (DateTime.Now > date.DateTime)
                {
                    return;
                }
            }
            var salePrice = 0m;
            var havePrice = Decimal.TryParse(category["SalePrice"], out salePrice);
            if (!havePrice)
            {
                return;
            }
            if (args.HasPrice && salePrice < args.Totals.PriceExVat)
            {
                args.Totals.DiscountExVat = args.Totals.PriceExVat - salePrice;
                args.Totals.PriceExVat = salePrice;
                args.HasPrice = true;
            }
            else if (!args.HasPrice)
            {
                args.Totals.PriceExVat = salePrice;
                args.HasPrice = true;
            }
        }
    }
}