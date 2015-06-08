using System;
using ActiveCommerce.IoC;
using ActiveCommerce.Validation;
using Microsoft.Practices.Unity;

namespace ActiveCommerce.Training.AddressValidator
{
    public class RegisterTypes : ITypeRegistration
    {
        public void Process(Microsoft.Practices.Unity.IUnityContainer container)
        {
            container.RegisterType<IAddressValidator, TrainingAddressValidator>();
        }

        public int SortOrder
        {
            get { return 0; }
        }
    }
}