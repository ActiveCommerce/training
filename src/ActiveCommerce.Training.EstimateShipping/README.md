Shopping Cart Estimate Shipping Example
=======================================
This example shows how you can add a way for users to estimate shipping costs on the shopping cart page. 
When a customer enters their zip/postal code, we'll calculate the rates for each available
shipping option and present those in a list. We're also saving the entered zip and automatically setting 
the cheapest option as the default on the cart, which will be reflected in the calculated Order Total.
In this example, we're limiting to US postal codes.

To add the `Shopping Cart - Estimate Shipping` component to the cart:

1. Add the sublayout (_/sitecore/layout/Sublayouts/Training/Shopping Cart - Estimate Shipping_) to the "Allowed Controls" of the `Shopping Cart Main` placeholder settings (_/sitecore/layout/Placeholder Settings/ActiveCommerce/Shopping Cart Main_)
2. Edit your Shopping Cart item (_/sitecore/content/&lt;site&gt;/Home/shop/cart_) in Page/Experience Editor mode
3. Select the `ac-shoppingcart-main` placeholder, and insert a new component just below the "Coupon Code" section. Choose to "Create New Content".
4. Repeat step 3 for the Mobile device if enabled
5. Style appropriate for your site