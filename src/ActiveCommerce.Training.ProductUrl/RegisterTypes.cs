using ActiveCommerce.IoC;
using Microsoft.Practices.Unity;

namespace ActiveCommerce.Training.ProductUrl
{
    public class RegisterTypes : ITypeRegistration
    {
        public void Process(Microsoft.Practices.Unity.IUnityContainer container)
        {
            container.RegisterType(typeof(Sitecore.Ecommerce.Catalogs.ProductUrlProcessor), typeof(NameAndVariantProductUrlProcessor), "Item Name and Variant", new HierarchicalLifetimeManager(), new InjectionMember[] {
                new InjectionConstructor(new ResolvedParameter<Sitecore.Ecommerce.Search.ISearchProvider>("CompositeSearchProvider")),
                new InjectionProperty("ShopContext")
            });
        }

        public int SortOrder
        {
            get { return 0; }
        }
    }
}