using System;
using System.Linq;
using ActiveCommerce.Carts;
using ActiveCommerce.Extensions;
using ActiveCommerce.Products;
using ActiveCommerce.Rules;
using Microsoft.Practices.Unity;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Ecommerce.DomainModel.Carts;
using Sitecore.Rules.Actions;

namespace ActiveCommerce.Training.Discounts.BuyOneGetOne
{
    public class AddDiscountProductToCart<T> : RuleAction<T> where T : PromoRuleContext
    {
        public string BogoName { get; set; }

        public ID ItemId { get; set; }

        public uint Multiplier { get; set; }

        public string MaxCountString { get; set; }

        public uint MaxCount
        {
            get
            {
                uint count = 0;
                uint.TryParse(MaxCountString, out count);
                return count;
            }
        }

        public AddDiscountProductToCart()
        {
            BogoName = Constants.DefaultBogoName;
            Multiplier = 1;
        }

        public override void Apply(T ruleContext)
        {
            Assert.ArgumentNotNull(ruleContext, "ruleContext");
            if (ruleContext.Cart == null)
            {
                return;
            }
            var item = Sitecore.Context.Database.GetItem(ItemId);
            if (item == null || !item.Template.DescendsFrom(TemplateIDs.ProductBase))
            {
                // Item doesn't exist or not a product
                return;
            }
            var countParam = string.Format(Constants.BogoCountParam, BogoName);
            if (!ruleContext.Parameters.ContainsKey(countParam))
            {
                return;
            }
            var count = ruleContext.Parameters[countParam] as uint?;
            if (count == null || count == 0)
            {
                return;
            }
            var toAdd = count * Multiplier;
            toAdd = MaxCount > 0 && MaxCount < toAdd ? MaxCount : toAdd;

            var repository = Sitecore.Ecommerce.Context.Entity.Resolve<IAdvancedProductRepository>();
            var product = repository.GetProduct<ProductBaseData>(item);
            var line = ruleContext.Cart.ShoppingCartLines.SingleOrDefault(x => x.Product.Code.Equals(product.Code));
            if (line != null && line.Quantity >= toAdd)
            {
                return;
            }
            if (line != null && line.Quantity < toAdd)
            {
                toAdd = toAdd - line.Quantity;
            }
            AddToCart(product.Code, toAdd.Value);
        }

        protected virtual void AddToCart(string productCode, uint quantity)
        {
            var cartManager = Sitecore.Ecommerce.Context.Entity.Resolve<IShoppingCartManager>();
            try
            {
                cartManager.AddProduct(productCode, quantity);
            }
            catch (InsufficientStockException e)
            {
                if (e.Available == 0)
                {
                    return;
                }
                AddToCart(productCode, (uint)e.Available);
            }
        }
    }
}