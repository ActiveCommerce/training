Custom Customer Profile Data Example
========

Active Commerce ultimately utilizes Sitecore Security, and thus ASP.NET Membership to store customer profile data
(at least out of the box!). The `CustomProperties` dictionary on the `CustomerInfo` class provides and easy way
to store custom values on the user profile. Note that to do this, you **must** add a field of the same name to the
*Customer* user profile template in the core database (/sitecore/templates/Ecommerce/Security/Profiles/Customer).

To provide easy access to custom user profile fields, you can subclass `CustomerInfo` and add properties which
get/set values in `CustomProperties`. This example project shows an example of doing this via a "birthday"
profile field which is populated as part of new user registration.

After populating values in `CustomProperties` or your own `CustomerInfo`, you need to save the changes via the
`UpdateCustomerProfile` method of `ICustomerManager`.

    var manager = Sitecore.Ecommerce.Context.Entity.Resolve<ICustomerManager<Sitecore.Ecommerce.DomainModel.Users.CustomerInfo>>();
    var customer = manager.CurrentUser;
    //populate CustomProperties or cast to sublass and set values
    manager.UpdateCustomerProfile(customer);

Note -- if you're just looking to extend `CustomerInfo`, this example has a lot more than you need. We wanted to
show a working example, so there's additional work being done here to add a new field to the new account form.

It's also worth noting that you can see the values of custom fields you add in the Sitecore User Manager, which can be accessed
via the Sitecore Desktop.

# What's in the Customer Info Example
1. A custom `CustomerInfo` class which adds a *Birthday* property. It's registered in Unity via `RegisterTypes`.
2. A skinned version of the *Account-Login-NewAccount.ascx* sublayout which adds a new field to account creation, and changes the AngularJS controller which handles this form.
3. *skins\sherpa\scripts\signin\components\new-account.js* defines a new `TrainingNewAccountCtrl` AngularJS controller which extends the existing `NewAccountCtrl`
(a process described in the *Active Commerce Developer's Cookbook*). It redefines the `newAccount` function to include a call to the new `birthday` service.
4. *skins\sherpa\scripts\signin\services\birthday.js* defines a new AngularJS service which can be used to save the value of the added birthday field.
5. `BirthdayController` is an MVC controller invoked by the birthday service. It does the work of populating and saving our extended customer profile with the birthday value.
* The controller route is registered in `RegisterRoutesProcessor`, an *initialize* pipeline processor that is configured in *xActiveCommerce.xCustomerInfo.config*.

# See also
* [TDS project with *Customer* template additions](../ActiveCommerce.Training.CustomerInfo.Sitecore.Core)