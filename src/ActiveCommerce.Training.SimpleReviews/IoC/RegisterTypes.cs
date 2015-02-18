using ActiveCommerce.IoC;
using Microsoft.Practices.Unity;

namespace ActiveCommerce.Training.SimpleReviews.IoC
{
    public class RegisterTypes : ITypeRegistration
    {
        public void Process(Microsoft.Practices.Unity.IUnityContainer container)
        {
            /*
             * Resolving our repository through unity allows injection of the Active Commerce
             * ISessionBuilder, which will manage your NHibernate sessions and ensure they are
             * properly Disposed.
             */
            container.RegisterType<IProductReviewRepository, ProductReviewRepository>();
        }

        public int SortOrder
        {
            get { return 0; }
        }
    }
}