Extending Order Data
========
This example shows how to extend the order data stored by Active Commerce.

# Steps to Extend Order Data
1. Create your own `Order` class which inherits `ActiveCommerce.Orders.Order`.
2. Add new properties to your class as needed -- be sure to declare them as `virtual`.
3. Create an `OrderMap` class which inherits `FluentNHibernate.Mapping.SubclassMap` with your `Order` class as the generic argument.
4. In the constructor for your `OrderMap`, use Fluent NHibernate mapping syntax to map your new properties.
5. Create a configuration patch which registers your assembly in the `acConfigurationBuilder` pipeline, so that your NHibernate mappings are loaded on startup.
6. Ensure your database schema has the needed table and fields. In a development environment, you can use `/sitecore/admin/DatabaseUtility.aspx` to update or drop/create the schema. For production, either generate the needed update script or use a SQL comparison tool.
7. Create an `OrderFactory` class which inherits `ActiveCommerce.Orders.OrderFactory`. This class is used by Active Commerce to construct new orders.
8. Override the `CreateOrder` method and return your new order type.
9. In a new or existing `ITypeRegistration` class, register your `OrderFactory` with the container as the implementation of the `IOrderFactory` service.