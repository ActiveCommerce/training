Coveo Search Customization
========
This is an example of skinning the Coveo Search Interface and adding custom computed fields in order to create a product search utilizing Coveo's search framework.

## What's included
1. `CoveoSearch.ascx` is a skinned version of the main Coveo search control, with some quick inline CSS changes and output additions for product search results (pricing, product image). Note that this is probably not a best practice example of re-styling the Coveo search component.
2. `AbstractProductComputedField` is a base class that can be used for indexing data from Active Commerce products for the purpose of output in Coveo search results. Note that reading data from a product repository requires a site context, and indexing executes outside of any site context. This base class allows the configuration of a site context on the computed field, and utilizes a `SiteContextSwitcher` so that the product domain model can be accessed.
3. `Price` is a computed field that stores the product price. The return type of "Number" is important here to allow range searches.
4. `ProductImage` is a computed field that stores the main product image URI. In this example, it's hardcoded to 166x166 but this is potentially another configuration value you could add.
5. `xActiveCommerce.zTraining.CoveoSearch.config` configures the computed fields.

## See also
* [Coveo Developer's Community - Customizing a Search Interface](https://developers.coveo.com/display/public/SC201505/Customizing+a+Search+Interface)
* [TDS project](../ActiveCommerce.Training.CoveoSearch.Sitecore)