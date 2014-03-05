using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.Carts;
using ActiveCommerce.IoC;
using ActiveCommerce.Web.Models;
using Sitecore.Ecommerce;
using Sitecore.Ecommerce.Data;
using Sitecore.Ecommerce.DomainModel.CheckOuts;
using Microsoft.Practices.Unity;
using Sitecore.Ecommerce.DomainModel.Orders;
using Sitecore.Ecommerce.DomainModel.Prices;

namespace ActiveCommerce.GiftMessage.Loader
{
    public class RegisterTypes : ITypeRegistration
    {
        public void Process(Microsoft.Practices.Unity.IUnityContainer container)
        {
            //checkout data holder (session-based)
            container.RegisterType<ICheckOut, ActiveCommerce.GiftMessage.CheckOut.CheckOut>();

            //checkout view model -- used for JSON model during checkout
            container.RegisterType<CheckoutViewModel, ActiveCommerce.GiftMessage.Model.CheckoutViewModel>();
            container.RegisterType(
                typeof(ActiveCommerce.Web.Models.Factories.IViewModelFactory<ShoppingCart, CheckoutViewModel>),
                typeof(ActiveCommerce.GiftMessage.Model.Factories.CheckoutViewModelFactory),
                new TransientLifetimeManager(),
                new InjectionMember[] {
                    new InjectionConstructor(new object[] {
                        new ResolvedParameter<CheckoutViewModel>()
                    }),
                    new InjectionProperty("ShopContext"),
                    new InjectionProperty("PriceFormatter")
                }
            );

            //new order type
            container.RegisterType(typeof(Order), typeof(ActiveCommerce.GiftMessage.Orders.Order), new InjectionMember[] {
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

            //new order-item mapping
            container.RegisterType(typeof(IMappingRule<>), typeof(ActiveCommerce.GiftMessage.Orders.OrderMappingRule), "OrderMappingRule", new InjectionProperty("MappingObject"));
        }

        public int SortOrder
        {
            get { return 0; }
        }
    }
}