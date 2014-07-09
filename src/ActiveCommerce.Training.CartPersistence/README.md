Cart Persistence POC
========
This a proof of concept example which demonstrates how you might implement persistent carts in Active Commerce.

The example as-is handles the following use cases:
* A customer whose session expires
* A customer who has created an account, then logs in elsewhere
* Restored products which are no longer available
* Restored products which are no longer in stock, or which have less stock available than was previously in the user's cart
* Alerting the customer of a restored, or partially restored cart

The persistence and restoration processes have been abstracted into Sitecore pipelines to make them more extensible. This could include additional persistence options, such as a SQL database.

Be sure to also reference the two associated TDS projects for needed Sitecore items.

Experimental, use at your own risk. The solution currently has the following limitations:
* Only persists cart items, their quantity, and coupon code. However out of the box, this should be all that is needed.
* May not handle products which have been flagged as "hidden" since the time the cart was persisted.
* Carts are only restored for users when logging in if there is nothing in their cart. There are all kinds of edge cases where this could be problematic otherwise, most notably logging in during checkout.
* The persist and load currently happen at the start/end of the HTTP request. This made it quicker/easier to implement, but is somewhat inefficient since the cart will be re-persisted whether it's changed or not.
* The message to the user regarding failed restoration of products is non-specific regarding what products failed.
* There is no mechanism to query or report on abandoned carts of anonymous users in Sitecore or AC itself, since the cart data is stored client-side.
* Extremely large shopping carts may exceed the allowed size of a cookie (4KB). Depending on the size of your product codes, this is probably over 400 products.