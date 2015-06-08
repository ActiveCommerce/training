Sales Tax Rate Provider (North America)
========
Most implementations of a rate provider will utilize a web service or other lookup to get the tax rates for a given address. In this example though, we are reading the tax rate from Sitecore XML settings for states that don't have any sort of local surtax. Keep in mind that in many states there are local surtaxes that can be applied at the county, city, county district, and city district level. You can even have different sales tax rates for two addresses within the same zip code. Unless your tax situation is very simple (e.g. tax nexus in a single state that has no local surtaxes), you will want to either utilize the Fast Tax plugin or implement a rate provider for a similar service.

## What's in this example
1. `TrainingRateProvider` is our implementation of `ISalesTaxRateProvider`. It is responsible for returning an enumeration of tax rates for a given address. In this example, that enumeration only ever has tax rates for one jurisdiction (the state). In a more complex example, it might have rates for the county, city, and districts thereof.
2. `RegisterTypes` registers the rate provider in Unity, with the name "training." We'll use this name to configure the rate provider of our tax calculator in our webshop business settings.
3. `xActiveCommerce.zTraining.TaxRateProvider.config` contains configured tax rates for a handful of states that don't have a local surtax. **Note that this list may not be complete nor accurate, including the configured rates!**



# See also
* [TDS project](../ActiveCommerce.Training.TaxRateProvider.Sitecore)
* [Wikipedia: Sales taxes in the United States (Summary Table)](http://en.wikipedia.org/wiki/Sales_taxes_in_the_United_States#Summary_table)
* *Active Commerce Developer's Cookbook*