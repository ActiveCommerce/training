Custom Discounts Examples
========

# Limited Use Discounts
This example shows how you can implement use limits on promotions, either overall or by customer.

## What's in this example
1. `LimitUse` and `LimitUsePerCustomer` custom rule conditions (and associated rule definitions in the [TDS project](../ActiveCommerce.Training.Discounts.Sitecore)) which query the orders database to check whether a promotion has been used more than X times, or X times for the current customer.
2. Extensions to the `ShoppingCartViewModel` and `ShoppingCartViewModelFactory` to allow us to display messaging (put in session by the custom conditions) regarding the reason why the discount wasn't applied (i.e. it has execeeded allowed uses).


# Buy One Get One (BOGO) Discount Actions
This example includes three promotion/discount actions which allow the creation of true "buy one get one" (BOGO) style promotions,
including buy-one-get-one (same or different products), buy-one-get-one-half-off, buy-X-get-Y, and more.

## Installation
1. Incorporate this C# project and/or code into your solution.
2. Utilize the [TDS project](../ActiveCommerce.Training.Discounts.Sitecore) to add rule elements for the four new BOGO rule actions,
and two new branch templates which ses up a basic BOGO promotions.
3. Add the *Buy One Get One, Same Product* and *Buy Product A, Get Product B* branch templates from the [TDS project](../ActiveCommerce.Training.Discounts.Sitecore)
as Insert Options on the *Promotions* folder under your *Webshop Business Settings*.

## Usage
When using these discount actions, the discount actions used in the construction of a BOGO-style offer are:

1. *Count Required Products* - Count the products in the cart which are required for the offer. If you choose a Variable Product / Product Family, any variants from the family which are in the
cart will be counted. You can also use this action multiple times with different products / families, and the counts will aggregate.

1. *For Every X Products* - Qualify the number of products required to trigger the discount. Note that if you are discounting the same product as is required, this number should be inclusive
of both the discounted and non-discounted products. e.g. For a simple buy-one-get-one, this should be 2.

1. *Add Product To Cart* - Add products to the cart for the user. You can specify the multiplier and max count for added products. Note this is optional, and auto-added products cannot be removed.
This is also not possible when doing a BOGO of a single product, since it's impossible for Active Commerce to know whether a product was added by the user or a discount rule.
It's also not advisable to use this with Variable Products / Product Families, since you don't know which variant the user wants to purchase.

1. *Discount Product* - Discount the "get one" product(s). You can specify the multiplier and max count for discounted products, as well as the discount amount and type
(dollars/currency or percentage). If you utilize this with a Variable Product / Product Family, any product from that family will be discounted.

Utilize the included branch templates to setup basic "Buy One Get One" offers for either the same product or different products. Note that the two require slightly
different configuration since the cart quantities double for the required product when you are offering the same product as the "get one."

Note that all actions also start with a "BOGO Name" that simply defaults to "BOGO." This allows you to configure multiple BOGO offers within the same rule. They are linked
by this name.

## Examples

### Buy 1 Product A, Get 1 Product B
![](images/BuyAGetB.png?raw=true)

### Buy 1 Product A, Discount Product Family B
![](images/BuyADiscountB.png?raw=true)

### Buy 1 Product A or Product B or Product C, Discount Product D
![](images/CountMany.png?raw=true)

### Buy Product A, Get 1 Product A
Maximum 3 free products.

![](images/BuyAGetA.png?raw=true)

### Buy 2 Product A, Get 1 Product A
Maximum 3 free products.

![](images/Buy2Get1.png?raw=true)

### Buy 2 Product A, Get 3 Product A
Maximum 3 free products.

![](images/Buy2Get3.png?raw=true)

### Two Offers with a Lot Going On...
* Buy 1 Product A, Discount 2 Product B (max 4 discounts)
* and Buy 2 Product C, Discount 1 Product D (max 2 discounts)

![](images/DoubleOffer.png?raw=true)


# See also
* [TDS project](../ActiveCommerce.Training.Discounts.Sitecore)