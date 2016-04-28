Gift Cards Example
========
This is an example demonstrating how to register a payment provider responsible for accepting gift cards.
Included with the payment provider are classes for managing and storing gift cards.

## What's included in this example
GiftCardPaymentProvider
GiftCardManager
GiftCardRepository
GiftCardInfo
giftcardutility.aspx
Checkout-Gift-Card.ascx and corresponding javascript


1. `GiftCardPaymentProvider` is an example of a payment provider for gift cards, responsible for accepting payments
2. `GiftCardManager` is an example of a gift card manager, responsible for basic gift card operations
3. `GiftCardRepository` is a repository for saving and retrieving gift cards
4. `RegisterTypes` registers all types above in Unity.  The GiftCardPaymentProvider is registred with a name that is referenced when setting up the payment provider in Sitecore.
5. `GiftCardInfo` represents a gift card which has a number, balance, PIN, and description
6. `giftcardutility.aspx`is a page which will create a set of test gift cards
7. _Checkout-Gift-Card.ascx_ is the sublayout responsible for displaying the payment form during checkout
7.1 The javascript files are the Angular code responsible for interacting with the front-end `checkout` service and the back end gift card controller at /ac/giftcard/


## How to use this example
1. In Sitecore, use the Installation Wizard to install the Active Commerce Gift Cards plugin.
2. Go to the following address: <site host>/sitecore/admin/databaseutility.aspx
    a. Click the "Update Schema" button.
    b. You should see a "Success!" message when completed. 
3. In SQL Server Management Studio, run the following script. Substitute <db> with your Active Commerce Database name (e.g. [AC33SC72Sitecore_ActiveCommerce]):
INSERT INTO <db>.[dbo].[hibernate_unique_key] ([next_hi],[entity]) VALUES (1,'ActiveCommerce.GiftCards.Data.GiftCard')

4. Alternatively, you can simply "Drop Schema" and then "Generate Schema" (WARNING: you will lose any existing data in the Active Commerce database)
5. Go to the following address: <site host>/sitecore/admin/giftcardutility.aspx
    a. Click the "Generate" button. This will create a set of test gift cards. This page can be used to check gift card balances as well.
6. In Sitecore content tree, create a new Payment item (named e.g. "Gift Card") in sitecore/Content/<My Site>/Webshop Business Settings/Payment Options.
    a. Set the Code to "GiftCard"
7. In Sitecore content tree, go to /sitecore/layout/Placeholder Settings/ActiveCommerce/Checkout Step Panel
    a. Add Layout/Sublayouts/Active Commerce/Checkout/Checkout - Gift Card to Allowed Placeholders
8. In Sitecore content tree, on the <site>/Home/Active Commerce/Checkout item:
    a. Open the page in the Experience Editor.
    b. Find the billing/payment step, and within the Checkout Step Panel component, click "Add to here" to add a new component at the top (just above the Billing Contact).
    c. Select the Checkout - Gift Card rendering, and follow the instructions to create a new datasource item for the component.
    d. After adding the new payment option, hover over the component and click the Edit Payment Configuration button. Within the resulting form, select the Gift Card payment option.
    e. Click Save in the Page Editor ribbon.
9. Ensure all new and updated items have been approved in workflow, and publish changes. Verify that Gift Card is now shown as an option on checkout.

# See also
* [TDS project](../ActiveCommerce.GiftCards.Sitecore)
* *Active Commerce Developer's Cookbook*