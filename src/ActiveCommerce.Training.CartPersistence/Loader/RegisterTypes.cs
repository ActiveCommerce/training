using ActiveCommerce.IoC;
using ActiveCommerce.Web.Models;
using Microsoft.Practices.Unity;
using ActiveCommerce.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Ecommerce.DomainModel.Users;

namespace ActiveCommerce.Training.CartPersistence.Loader
{
    public class RegisterTypes : ITypeRegistration
    {
        public void Process(Microsoft.Practices.Unity.IUnityContainer container)
        {
            container.RegisterType(typeof(ICustomerManager<>), typeof(ActiveCommerce.Training.CartPersistence.Users.CustomerManager<>), new HierarchicalLifetimeManager(), new InjectionMember[]
                                                                          {
                                                                              new InjectionProperty("CustomerMembership")
                                                                          });

            container.RegisterType(
                typeof(ActiveCommerce.Web.Models.Factories.IViewModelFactory<ShoppingCart, ShoppingCartViewModel>),
                typeof(ActiveCommerce.Training.CartPersistence.ViewModel.ShoppingCartViewModelFactory),
                new HierarchicalLifetimeManager(),
                new InjectionMember[] {
                    new InjectionProperty("EstimatedCosts", true),
                    new InjectionProperty("ShopContext"),
                    new InjectionProperty("PriceFormatter"),
                    new InjectionProperty("CartProductViewModelFactory"),
                    new InjectionProperty("RelatedProductViewModelFactory")
                }
            );
        }

        public int SortOrder
        {
            get { return 0; }
        }
    }
}