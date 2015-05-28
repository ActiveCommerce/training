using System;
using Sitecore.Analytics.Model.Framework;

namespace ActiveCommerce.Training.CartPersistence.Analytics.Model
{
    [Serializable]
    public class ShoppingCart : Facet, IShoppingCart, IFacet, IElement, IValidatable
    {
        private const string COUPONCODE = "CouponCode";
        private const string SHOPPINGCARTLINES = "ShoppingCartLines";

        public ShoppingCart()
        {
            base.EnsureAttribute<string>(COUPONCODE);
            base.EnsureDictionary<IShoppingCartLine>(SHOPPINGCARTLINES);
        }

        public string CouponCode
        {
            get
            {
                return base.GetAttribute<string>(COUPONCODE);
            }
            set
            {
                base.SetAttribute<string>(COUPONCODE, value);
            }
        }

        public Sitecore.Analytics.Model.Framework.IElementDictionary<IShoppingCartLine> ShoppingCartLines
        {
            get
            {
                return base.GetDictionary<IShoppingCartLine>(SHOPPINGCARTLINES);
            }
        }

    }
}
