Product Stock Extension Example
===============================
This example shows how to extend and utilize the product stock data that Active Commerce stores in its database, and allow that data to be maintained in the Sitecore Content Editor.

The two use cases which are loosely implemented are:
* Alternate, regional stock levels for particular Active Commerce sites (e.g. regional warehouses)
* Implementation of an out-of-stock threshold or "buffer," at which point the product appears to be out of stock

## How is it implemented?
1. `ProductStock` extends the existing Active Commerce `ProductStock` class and adds the additional fields we want to store in the Active Commerce database. Note that all the properties of this class need to be declared `virtual`.
2. `ProductStockMap` utilizes Fluent NHibernate to map the properties to SQL. This example shows some basic mapping directives. For more information, check out the [Fluent NHibernate documentation](https://github.com/jagregory/fluent-nhibernate/wiki/Getting-started).
3. `xActiveCommerce.zTraining.ProductStockExtension.config` contains a few important configuration patches:
    * Configures the new fields in the `productStock` data provider and maps them to the fields added to the *Product Base Stock* template in the associated [TDS project](../ActiveCommerce.Training.ProductStockExtension.Sitecore). This allows this data to be editied/maintained via the Sitecore Content Editor, and update directly in the Active Commerce database.
    * Patches our assembly into the configuration of a processor in the `acConfigurationBuilder` pipeline, which builds the NHibernate configuration for the Active Commerce database. This will ensure that the mappings in `ProductStockMap` will be applied when you run an "Update Schema" from */sitecore/admin/DatabaseUtility.aspx*.
    * Sets a custom attribute on the *sherpa_winter_outfitters* site definition, `stockRegion`, which we will utilize in our custom `ProductStockManager`.
4. `ProductStockManager` overrides the `GetStock` method of its Active Commerce base class to implement the business logic for our two use cases.
5. `RegisterTypes` registers the new `ProductStock` and `ProductStockManager` classes with Microsoft Unity.

## See Also
* [TDS project](../ActiveCommerce.Training.ProductStockExtension.Sitecore)