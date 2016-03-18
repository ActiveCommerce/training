Custom Payment Options Examples
========

# Invoice Payment
This example adds an Invoice Payment option to the checkout process and stores the purchase order number 
on the order upon completion.

Before continuing, you should review the _Customizing the Checkout_ section of the _Active Commerce Developer's Cookbook_.

## Checkout Step Structure
The payment step on the Checkout page is a bit more complex than others. Each payment option is a component within the
_Checkout Payments_ placeholder, and will contain multiple components which make up the various inputs of the payment option.
Review the diagram below for the rendering/placeholder structure of the payment step, depicted after we've added Invoice Payment
as an option.

![](payment-checkout-layout.png?raw=true)

## What's in the Invoice Payment Example

1. `InvoicePaymentOption`, a `PaymentProvider` implementation which accepts and validates a purchase order number. It is regsitered in Unity via `RegisterTypes`.
A new payment option needs to be added in _/sitecore/content/(site)/Webshop Business Settings/Payment Options_ which has the same Code as the
name of the component when registered in Unity (i.e. _InvoicePayment_).
2. `InvoicePaymentDetails`, a `PaymentDetails` implementation which defines the details of the invoice payment; in this case, adding a property for 
purchase order number. We also override the `Description` property so that the purchase order number will be recorded with the order payment. This
is also used for display during checkout and on the order receipt.
3. A new checkout process component for collecting and storing the invoice number. (For more information on
customizing the checkout process, reference the _Developer's Cookbook_.)
    * _Checkout-Invoice-Payment.ascx_ is the Sitecore sublayout which displays the invoice input in the checkout. It has a corresponding
    Sublayout item in Sitecore. That sublayout is added to the `ac-checkout-payment` placeholder on the Checkout page within your
    invoice payment tab (see above).
    * You must use the Page Editor to add this sublayout, since this page makes use of dynamic placeholder keys.
    * `InvoicePaymentCheckoutComponent` is the datasource item for this sublayout. It has a corresponding Sitecore Template. Checkout
    components store the form text and other messaging that you wish to display on the page, and utilize Glass Mapper to map the
    datasource item values.
    * _skins\scripts\checkout\services\invoice-payment.js_ extends the Active Commerce `checkout` service in AngularJS
    and adds a function which calls an ASP.NET MVC service (described below) to store the purchase order number before order processing.
    * _skins\scripts\checkout\components\invoice-payment.js_ defines the AngularJS controller which is referenced on
    _Checkout-Invoice-Payment.ascx_. It utilizes the client-side event model for the Active Commerce Checkout, described in the
    _Developer's Cookbook_, to validate the purchase order number and ultimately call the `checkout` service to store the
    purchase order number.
4. A new ASP.NET MVC service for receiving and storing the purchase order number on the payment details.
    * The `InvoicePaymentController` extends Active Commerce `AppControllerBase` and defines a single, simple action which 
    accepts the purchase order number and updates it on the cart's primary payment details (our new `InvoicePaymentDetails`).
    Note that the cart primary payment will already be set for us based on the selected payment tab during checkout.
    * `RegisterRoutesInitializeProcessor` registers our new controller and route. This is an _initialize_ pipeline processor which gets
    added via _xActiveCommerce.xInvoicePayment.config_.

# See also
* [TDS project](../ActiveCommerce.Training.InvoicePayment.Sitecore)