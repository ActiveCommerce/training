using ActiveCommerce.Training.CartPersistence.Common;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.Mvc
{
    public class PersistShoppingCart : Analytics.PersistShoppingCart
    {
        protected override bool PersistingIsActive()
        {
            return CartPersistenceContext.IsActive;
        }
    }
}
