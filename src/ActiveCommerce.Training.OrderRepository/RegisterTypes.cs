using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.IoC;
using Microsoft.Practices.Unity;

namespace ActiveCommerce.Training.OrderRepository
{
    public class RegisterTypes : ITypeRegistration
    {
        public void Process(Microsoft.Practices.Unity.IUnityContainer container)
        {
            container.RegisterType(
                typeof(ActiveCommerce.Orders.Management.IOrderRepository<>),
                typeof(OrderRepositoryExtended<>), new HierarchicalLifetimeManager());
        }

        public int SortOrder
        {
            get { return 0; }
        }
    }
}