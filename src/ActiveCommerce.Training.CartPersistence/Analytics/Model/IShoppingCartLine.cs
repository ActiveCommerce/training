using Sitecore.Analytics.Model.Framework;

namespace ActiveCommerce.Training.CartPersistence.Analytics.Model
{
    public interface IShoppingCartLine : IElement, IValidatable
    {
        string ProductCode { get; set; }
        uint Quantity { get; set; }
    }
}
