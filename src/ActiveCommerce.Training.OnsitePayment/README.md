Payment Gateway Integration
========
This example shows how to build a payment gateway integration for on-site, "integrated" payment. Typically this means collecting the credit card on the checkout page, and using a web service or API from the payment gateway to authorize, capture, void, credit, etc.

## What's in this example
1. `MockServiceOnsitePaymentProvider` is a commented example of a full payment provider implementation, including implementation of `IReservable` and `ICreditable`.
2. `RegisterTypes` registers the payment provider in Unity with a registration name that is referenced when setting up the payment provider in Sitecore.
3. `MockService` is the mock payment gateway API created for demonstration purposes. It doesn't really do anything.


# See also
* [TDS project](../ActiveCommerce.Training.OnsitePayment.Sitecore)
* *Active Commerce Developer's Cookbook*