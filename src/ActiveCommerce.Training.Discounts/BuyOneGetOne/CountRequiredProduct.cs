using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.Carts;
using ActiveCommerce.Extensions;
using ActiveCommerce.Products;
using ActiveCommerce.Rules;
using Sitecore.Diagnostics;
using Microsoft.Practices.Unity;
using Sitecore.Rules.Actions;

namespace ActiveCommerce.Training.Discounts.BuyOneGetOne
{
    public class CountRequiredProduct<T> : RuleAction<T> where T : PromoRuleContext
    {
        public string BogoName { get; set; }

        public string RequiredProduct { get; set; }

        public CountRequiredProduct()
        {
            BogoName = Constants.DefaultBogoName;
        }

        public override void Apply(T ruleContext)
        {
            Assert.ArgumentNotNull(ruleContext, "ruleContext");
            if (ruleContext.Cart == null || ruleContext.Cart.ShoppingCartLines == null)
            {
                return;
            }
            var item = Sitecore.Context.Database.GetItem(RequiredProduct);
            if (item == null || !item.Template.DescendsFrom(TemplateIDs.ProductBase))
            {
                // Item doesn't exist or not a product
                return;
            }
            var repository = Sitecore.Ecommerce.Context.Entity.Resolve<IAdvancedProductRepository>();
            var product = repository.GetProduct<ProductBaseData>(item);
            IEnumerable<ShoppingCartLine> lines = null;
            var cartLines = ruleContext.Cart.ShoppingCartLines;
            var variableProduct = product as VariableProduct;
            if (variableProduct != null)
            {
                lines = cartLines.Where(x => variableProduct.Variants.Any(y => x.Product.Code.Equals(y.Code))).Cast<ShoppingCartLine>();
            }
            else
            {
                lines = cartLines.Where(x => x.Product.Code.Equals(product.Code)).Cast<ShoppingCartLine>();
            }
            var shoppingCartLines = lines as IList<ShoppingCartLine> ?? lines.ToList();
            if (!shoppingCartLines.Any())
            {
                return;
            }
            var quantity = shoppingCartLines.Sum(x => x.Quantity);
            var countParam = string.Format(Constants.BogoCountParam, BogoName);
            uint? existing = 0;
            if (ruleContext.Parameters.ContainsKey(countParam))
            {
                existing = ruleContext.Parameters[countParam] as uint?;
            }
            ruleContext.Parameters[countParam] = (uint)quantity + (existing ?? 0);
        }
    }
}