Active Commerce Training Examples
========
Within this project you will find completed examples of exercises used during Active Commerce training,
and other examples of extension and integration points that may be helpful in implementing Active Commerce.

Note that the examples within this project should not be considered production-ready code. They are merely
intended to provide direction in your own implementation. These examples are not supported by Active Commerce,
and we provide no warranty. Use at your own risk.

# Contents

## Products and Categories

[Custom Product Class](./src/ActiveCommerce.Training.CustomProduct)
- Extend the Product domain object and Sitecore template

[Product Import](./src/ActiveCommerce.Training.ProductImport)
- Example of importing product data via the Sitecore Item API on a scheduled/batched basis.

[Product Data Provider](./src/ActiveCommerce.Training.ProductDataProvider)
- Experimental example of real-time data integration via a Sitecore Data Provider for just specific fields within a product item.

[Product Url Examples](./src/ActiveCommerce.Training.ProductUrl)
- Customizing the format of product urls.

[Custom Category](./src/ActiveCommerce.Training.CustomCategory)
- Example of creating a custom category domain object and Sitecore template


## Product Stock

[Product Stock Extension](./src/ActiveCommerce.Training.ProductStockExtension)
- Example of extending the stock data stored in the Active Commerce database, allowing that data to be edited in the Content Editor, and using that data to implement new stock business rules.

[Product Stock Update](./src/ActiveCommerce.Training.ProductStockUpdate)
- Example of updating product stock data on a scheduled/batched basis.

[Real-time Stock](./src/ActiveCommerce.Training.RealTimeStock)
- Implement a custom `ProductStockManager` to retrieve stock in real-time from an external web service.


## Product Pricing and Taxes

[Real-time Pricing](./src/ActiveCommerce.Training.RealTimePricing)
- Extend the product pricing pipeline to retrieve pricing in real-time (with a timed cache) from an external web service.

[Category-Wide Sale Pricing](./src/ActiveCommerce.Training.CategorySale)
- Modify the pricing pipeline to allow category-wide sale pricing.

[Price Comparison](./src/ActiveCommerce.Training.ComparePrice)
- Allow display of price within product comparison tables

[Price Testing POC](./src/ActiveCommerce.Training.PriceTesting)
- Experimental example of implementing pricing rules to allow A/B testing of product pricing.

[Custom Tax Calculator](./src/ActiveCommerce.Training.TaxCalculator)
- Example of implementing a custom tax calculator in order to implement custom tax logic or integrate with an external tax service.


## Checkout

[Checkout Extension - Gift Message](./src/ActiveCommerce.GiftMessage)
- Example of extending the data collected during checkout. There is a detailed overview of this example in the *Active Commerce Developer's Cookbook*

[Invoice Payment](./src/ActiveCommerce.Training.Payment)
- Extending the payment step of the checkout process and order processing to allow for payment by invoice / purchase order number.

[Checkout via API](./src/ActiveCommerce.Training.CheckoutViaApi)
- Construct a shopping cart and execute order processing directly through the Active Commerce domain layer.

## Orders and Order Processing

[Order Data Extension](./src/ActiveCommerce.Training.OrderExtension)
- Extending the Order domain object and Sitecore template. This example is referenced/utilized within others in this solution.

[Order Processing Pipeline Example](./src/ActiveCommerce.Training.OrderProcessing)
- Example of adding a new processor to the order processing pipeline for real-time order data integration.

[Batched/Scheduled Order Data Export](./src/ActiveCommerce.Training.OrderBatching)
- Example of exporting order data with an external system on a scheduled/batched basis.

[Batched/Scheduled Order Data Update](./src/ActiveCommerce.Training.OrderUpdate)
- Example of updating order status and tracking number from an external system on scheduled/batched basis.


## Shopping Cart

[Microsoft Unity "Hello World" Example](./src/ActiveCommerce.Training.HelloWorld)
- Simple example of replacing a component in Unity. In this case, the `ShoppingCartManager`.

[Shopping Cart Persistence](./src/ActiveCommerce.Training.CartPersistence)
- Persist user shopping carts beyond the ASP.NET Session, in cookies and/or user profiles.

[Product Quantity Input](./src/ActiveCommerce.Training.ProductQuantity)
- Allow a user to specify a product quantity before adding to cart.

[Discount Extensions](./src/ActiveCommerce.Training.Discounts)
- Custom discount actions, including true BOGO discounts.

## Customer Data

[Customer Info Extension](./src/ActiveCommerce.Training.CustomerInfo)
- Extend the data stored on the customer profile


## Shipping Integration

[Shipping Rates Integration](./src/ActiveCommerce.Training.ShippingIntegration)
- Get shipping rates from an external web service.


## Skinning

[Various Skinning Examples](./src/ActiveCommerce.Training.Web)
- Skinning/theming scripts, styles, HTML templates, etc.

## NHibernate

[Simple Reviews System](./src/ActiveCommerce.Training.SimpleReviews)
- Persist a custom object to the Active Commerce database with NHibernate and manage the data with Sitecore SPEAK