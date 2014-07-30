using ActiveCommerce.Training.CartPersistence.Common;

namespace ActiveCommerce.Training.CartPersistence.Pipelines.Mvc
{
    public class PersistShoppingCart : Analytics.PersistShoppingCart
    {
        protected override bool PersistenceActive()
        {
            return CartPersistenceContext.IsActive;
        }
    }
}
