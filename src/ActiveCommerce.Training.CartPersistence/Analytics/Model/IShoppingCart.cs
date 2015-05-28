using Sitecore.Analytics.Model.Framework;

namespace ActiveCommerce.Training.CartPersistence.Analytics.Model
{
    public interface IShoppingCart : IFacet, IElement, IValidatable
    {
        string CouponCode { get; set; }
        IElementDictionary<IShoppingCartLine> ShoppingCartLines { get; }
    }
}
