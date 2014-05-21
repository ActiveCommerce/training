using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.IoC;
using Microsoft.Practices.Unity;
using Sitecore.Ecommerce.Data;
using Sitecore.Ecommerce.DomainModel.Orders;
using Sitecore.Ecommerce.DomainModel.Prices;

namespace ActiveCommerce.Training.OrderBatching
{
    public class RegisterTypes : ITypeRegistration
    {
        public void Process(Microsoft.Practices.Unity.IUnityContainer container)
        {
            //new base order type
            container.RegisterType(typeof(Order), typeof(ActiveCommerce.Training.OrderExtension.Order), new InjectionMember[] {
                new InjectionConstructor(new object[] {
                    new ResolvedParameter<OrderStatus>("New")
                }),
                new InjectionProperty("OrderLines"),
                new InjectionProperty("Currency"),
                new InjectionProperty("CustomerInfo"),
                new InjectionProperty("NotificationOption"),
                new InjectionProperty("PaymentSystem"),
                new InjectionProperty("ShippingProvider"),
                new InjectionProperty("Totals", new ResolvedParameter(typeof(Totals), "Order")),
                new InjectionProperty("Status")
            });
            container.RegisterType(typeof(IMappingRule<>), typeof(ActiveCommerce.Training.OrderExtension.OrderMappingRule), "OrderMappingRule", new InjectionProperty("MappingObject"));
        }

        public int SortOrder
        {
            get { return 0; }
        }
    }
}