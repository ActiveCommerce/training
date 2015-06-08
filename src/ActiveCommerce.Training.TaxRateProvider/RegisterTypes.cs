using Microsoft.Practices.Unity;

namespace ActiveCommerce.Training.TaxRateProvider
{
    public class RegisterTypes : ActiveCommerce.IoC.ITypeRegistration
    {
        public void Process(Microsoft.Practices.Unity.IUnityContainer container)
        {
            container.RegisterType<ActiveCommerce.Taxes.Rates.ISalesTaxRateProvider, TrainingRateProvider>("training");
        }

        public int SortOrder
        {
            get { return 0; }
        }
    }
}