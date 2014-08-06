Extending Order Data
========
This example shows how to extend the order data stored by Active Commerce. It involves both
extending the `Order` domain object, and the Sitecore template which defines order data.

# Steps to Extend Order Data
1. Create a new Sitecore template which inherits from *ActiveCommerce/Order/Order*.
2. Add your new field(s) to the template.
3. In a configuration patch, override the `Ecommerce.Order.OrderItemTempalteId` (sic) setting. Note that the mispelling, inherited
from Sitecore Ecommerce Services, is intentional. See [*xActiveCommerce.zTraining.OrderExtension.config*](./App_Config/Include/xActiveCommerce.zTraining.OrderExtension.config).
4. Create a new `Order` class which subclasses `ActiveCommerce.Orders.Order`.
5. Add a property to this class for your new field. See [`ActiveCommerce.Training.OrderExtension.Order`](./Order.cs).
6. Create a new `OrderMappingRule` which subclasses `ActiveCommerce.Data.OrderMappingRule`. This class is responsible for mapping complex order data
into a flattened structure to be stored on a Sitecore Item.
7. Create a new property on this class which maps values from the internal mapping object (the order) to the Sitecore Item using Glass Mapper.
See [`ActiveCommerce.Training.OrderExtension.OrderMappingRule`](./OrderMappingRule.cs).
8. Register your `Order` and `OrderMappingRule` in Unity by creating an implementation of `ITypeRegistration`.
See [`ActiveCommerce.Training.OrderExtension.RegisterTypes`](./RegisterTypes.cs).
9. In your configuration patch, register your assembly with Glass Mapper. See [*xActiveCommerce.zTraining.OrderExtension.config*](./App_Config/Include/xActiveCommerce.zTraining.OrderExtension.config).
10. Populate your new property on the order and save it at the appropriate time. See the [Order Update Example](../ActiveCommerce.Training.OrderUpdate),
the [Gift Message Example](../ActiveCommerce.GiftMessage), or the [Invoice Payment Example](../ActiveCommerce.Training.Payment).