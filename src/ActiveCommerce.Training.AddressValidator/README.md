Address Validator
========
This example demonstrates implementing a custom address validator for the checkout process. Active Commerce includes a basic address validator that confirms address format for U.S. and Canada. If you wish to integrate with an address validation API or otherwise customize server-side address validation, you can start with this example. Client-side address validation is handled via AngularJs directives. You can also do simple address corrections with an `IAddressValidator`.

## What's in this example
1. `TrainingAddressValidator` is the implementation of `IAddressValidator`. It is responsible for validating (and possibly correcting) the provided address. This example is most definitely for demonstration purposes only.
2. `RegisterTypes` registers the address validator in Unity.

# See also
* *Active Commerce Developer's Cookbook*