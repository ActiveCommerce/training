Gift Cards Example
========
This is an example demonstrating how to register a payment provider responsible for accepting gift cards.
Included with the payment provider are classes for managing and storing gift cards.

## What's included in this example
1. `GiftCardPaymentProvider` is an example of a payment provider for gift cards, responsible for accepting payments
1. `GiftCardManager` is an example of a gift card manager, responsible for basic gift card operations
1. `GiftCardRepository` is a repository for saving and retrieving gift cards
1. `RegisterTypes` registers all types above in Unity.  The GiftCardPaymentProvider is registred with a name that is referenced when setting up the payment provider in Sitecore.
1. `GiftCardInfo` represents a gift card which has a number, balance, PIN, and description
1. `giftcardutility.aspx` is a page which will create a set of test gift cards
1. `Checkout-Gift-Card.ascx` is the sublayout responsible for displaying the payment form during checkout
    * The javascript files are the Angular code responsible for interacting with the front-end `checkout` service and the back end gift card controller at /ac/giftcard/


## How to use this example
1. Incorporate this C# project and/or code into your solution.
1. Utilize the [TDS project](../ActiveCommerce.GiftCards.Sitecore) to add the new template, sublayout, and tranlation dictionary items.
1. Go to the following address: &lt;site host&gt;/sitecore/admin/databaseutility.aspx
    * Click the "Update Schema" button.
    * You should see a "Success!" message when completed. 
    * In SQL Server Management Studio, run the following script. Substitute &lt;db&gt; with your Active Commerce Database name (e.g. [AC33SC81Sitecore_ActiveCommerce]):
      ```
      INSERT INTO <db>.[dbo].[hibernate_unique_key] ([next_hi],[entity]) VALUES (1,'ActiveCommerce.GiftCards.Data.GiftCard')
      ```
   * Alternatively, you can simply "Drop Schema" and then "Generate Schema" (**WARNING: you will lose any existing data in the Active Commerce database**)
1. Go to the following address: &lt;site host&gt;/sitecore/admin/giftcardutility.aspx
    * Click the "Generate" button. This will create a set of test gift cards. This page can be used to check gift card balances as well.
1. In Sitecore content tree, create a new Payment item (named e.g. "Gift Card") in sitecore/Content/&lt;My Site&gt;/Webshop Business Settings/Payment Options.
    * Set the Code to "GiftCard"
1. In Sitecore content tree, go to /sitecore/layout/Placeholder Settings/ActiveCommerce/Checkout Step Panel
    * Add Layout/Sublayouts/Active Commerce/Checkout/Checkout - Gift Card to Allowed Placeholders
1. In Sitecore content tree, on the &lt;site host&gt;/Home/Active Commerce/Checkout item:
    * Open the page in the Experience Editor.
    * Find the billing/payment step, and within the Checkout Step Panel component, click "Add to here" to add a new component at the top (just above the Billing Contact).
    * Select the Checkout - Gift Card rendering, and follow the instructions to create a new datasource item for the component.
    * After adding the new payment option, hover over the component and click the Edit Payment Configuration button. Within the resulting form, select the Gift Card payment option.
    * Click Save in the Page Editor ribbon.
1. Ensure all new and updated items have been approved in workflow, and publish changes. 
1. Verify that Gift Card is now shown as an option on checkout. You should now be able to use the generated gift cards as payment.

# See also
* [TDS project](../ActiveCommerce.GiftCards.Sitecore)
* *Active Commerce Developer's Cookbook*