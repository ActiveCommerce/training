using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;

namespace ActiveCommerce.Training.TaxCalculator
{
    public class RegisterTypes : ActiveCommerce.IoC.ITypeRegistration
    {
        public void Process(Microsoft.Practices.Unity.IUnityContainer container)
        {
            container.RegisterType<ActiveCommerce.Taxes.ITaxCalculator, TrainingTaxCalculator>("training");
        }

        public int SortOrder
        {
            get { return 0; }
        }
    }
}