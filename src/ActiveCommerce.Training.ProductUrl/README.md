Customizing Prouduct Url Format
========
This example shows how to implement a custom product url processor. In this case, one which
uses the product name with the ability to handle variants. It involves creating a `ProductUrlProcessor`, 
registering with Unity, and adding the corresponding "Display Product Modes" item in Sitecore.

# Create your ProductUrlProcessor
1. Creat a new class which subclasses `Sitecore.Ecommerce.Catalogs.ProductUrlProcessor`.
2. Implement the 2 required abstract methods `GetProductUrl` and `ResolveProductItem`. See [`ActiveCommerce.Training.ProductUrl.NameAndVariantProductUrlProcessor`](./NameAndVariantProductUrlProcessor.cs). 
These methods are responsible for mapping to/from a product url and Sitecore item.
3. Register your processor (`NameAndVariantProductUrlProcessor`) in Unity by creating an implementation of `ITypeRegistration`.
See [`ActiveCommerce.Training.ProductUrl.RegisterTypes`](./RegisterTypes.cs). 
    1. Make sure you specify the required `ISearchProvider` constructor injection and `ShopContext` property injection.
    2. Give this a unique name (e.g. "Item Name and Variant")

# Add "Display Product Modes" option in Sitecore
1. In Sitecore, add a new item under '/sitecore/system/Modules/Ecommerce/System/Display Product Modes'. See [TDS project](../ActiveCommerce.Training.ProductUrl.Sitecore).
2. The item Key needs to match the name you registered your class in Unity (e.g. "Item Name and Variant")

# Configure the Product Catalog(s)
1. Select this new "Display Product Modes" option for each product catalog 
(those with a template of '/sitecore/templates/ActiveCommerce/Categorization/Catalog') you wish 
to use this url format. For example, the default Product catalog at '/sitecore/content/<site>/Home/Products'.
2. There is "Display Products Mode" field on these items. Select the new option (e.g. "Item Name and Variant"), Save, and publish.
3. The new url formatting will now be used!