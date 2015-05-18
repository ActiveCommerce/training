Price Rules POC
===============
This a proof of concept example which demonstrates how you can manipulate product pricing using the Sitecore Rules Engine. Note this is different than the built-in promotion rules because it affects the *displayed* price of the product, rather than adding a discount in the shopping cart.

Conceptually this would allow you to do things like:

* Dynamically discount product pricing based on various conditions (user role, current date, cart contents)
* Dynamically change the active price level from the pricing matrix based on similar conditions
* Test pricing changes using dynamic conditions (marketing campaign)

## What's included

1. `GeneralSettings` configuration extension which adds a rules field for configuring one or more pricing rules. This is mapped using Glass and registered in Unity via `RegisterTypes`.
2. An addition to the `acGetProductTotals` pipeline, `RunPricingRules`, which executes the configured rules.
3. A new rule context, `PricingRuleContext`, which extends the AC `CartRuleContext` and adds the `GetProductTotalsArgs` from the pricing pipeline so that pricing conditions and actions have access to all the inputs and outputs of the pricing pipeline.
4. Example rule actions for dynamically changing the active pricing level (`SetPriceLevelAction`) and dyanmically applying a percentage discount (`ApplyDiscountAction`).
5. Example Sitecore items in the [TDS project](../ActiveCommerce.Training.PriceRules.Sitecore) which define the rule context, and new actions, the new General Settings template, and some example rules for the Sherpa site.

## See also
* [Sitecore Community Docs - Rules Engine](http://sitecore-community.github.io/docs/documentation/Sitecore%20Fundamentals/Rules%20Engine/)
* [Sitecore Rules Engine Cookbook](https://sdn.sitecore.net/Reference/Sitecore%207/Rules%20Engine%20Cookbook.aspx)
* [TDS project](../ActiveCommerce.Training.PriceRules.Sitecore)
