using Microsoft.Practices.Unity;
using ActiveCommerce.IoC;

namespace ActiveCommerce.Training.RealTimeStock
{
    public class RegisterTypes : ITypeRegistration
    {
        public void Process(Microsoft.Practices.Unity.IUnityContainer container)
        {
            container.RegisterType<ActiveCommerce.Products.Stock.IProductStockManager, ProductStockManager>();
        }

        public int SortOrder
        {
            get { return 0; }
        }
    }
}