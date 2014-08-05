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
name of the component when registered in Unity.
2. A new checkout process component for collecting and storing the invoice number. (For more information on
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
3. A new ASP.NET MVC service for receiving and storing the purchase order number, and a `ICheckOut` implementation where
the purchase order number can be stored during checkout.
    * The `CheckOut` class extends the builtin `ActiveCommerce.CheckOuts.CheckOut`, and is simply a place to store values
    temporarily during the checkout process. It is stored in session, so be sure to mark it as `[Serializable]`.
    It is regsitered in Unity via `RegisterTypes`.
    The `IInvoicePayment` interface is an abstraction that makes it easier to use this example if you already have an existing
    `ICheckOut` implementation (such as the one provided in the Gift Message example!).
    * The `CheckoutController` extends the existing Active Commerce `CheckoutController` and 
    adds a single, simple action which accepts the purchase order number and updates it in the `ICheckOut`.
    * `RegisterRoutesInitializeProcessor` removes the existing checkout route registration, and replaces it with our new controller.
    You could also just create an entirely new controller and route. This is an _initialize_ pipeline processor which gets
    added via _xActiveCommerce.xPayment.config`.
4. Extension of order data to store the purchase order number.
    * The Active Commerce `Order` template and class are extended in the
    [`ActiveCommerce.Training.OrderExtension` project](../ActiveCommerce.Training.OrderExtension).
    * The new order template extends the ActiveCommers\Orders\Order template and adds our new field.
    * `Order` adds a property for purchase order number.
    * `OrderMappingRule` allows us to transform values from `Order` before mapping them into the item. In this case,
        it's a simple pass-through of the value.
    * `SavePurchaseOrderData` is an order pipeline processor which reads the purchase order number from the `ICheckOut`,
    sets it on the `Order`, and then saves the order. This pipeline processor gets added via _xActiveCommerce.xPayment.config_.
5. Skinning of the receipt page (_Receipt-Details.ascx_) and receipt email (_Mail-OrderReceipt.ascx_) to display the purchase order number. 