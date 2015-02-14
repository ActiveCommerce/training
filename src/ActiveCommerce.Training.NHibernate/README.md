Simple Review System
========
This example shows how to map and persist a custom type in the Active Commerce SQL database with NHibernate. This is useful for user generated content, transactional data, etc.

## How is it done?
1. `ProductReview`, our custom type which we want to persist. The only thing special here is that all the properties of this class need to be declared `virtual`.
2. `ProductReviewMap` utilizes Fluent NHibernate to map the properties to SQL. This example shows some basic mapping directives. For more information, check out the [Fluent NHibernate documentation](https://github.com/jagregory/fluent-nhibernate/wiki/Getting-started).
3. `App_Config\Include\xActiveCommerce.zTraining.NHibernate.config` is a configuration patch that includes the addition of this assembly to the Active Commerce NHibernate configuration. Any Fluent NHibernate mappings in this assembly will be picked up.
4. `IProductReviewRepository` defines our repository interface. This is useful for mocking in your unit tests, and also makes resolving our repository through Unity cleaner. (More on that in a moment...)
5. `ProductReviewRepository` is our repository implementation that demonstrates basic use of NHibernate to do CRUD operations.
	* We use constructor injection from Unity to inject an `ISessionBuilder`. This is the Active Commerce service which manages your NHibernate sessions.
	* Note that we only construct a single NHibernate session for the repository.
	* NHibernate tracks any changes for us, so that we can have "persistence ignorance." After querying objects for example, you can simply make changes, then call `Flush()`.
	* Any new objects do need to be added to the NHibernate session. This is the purpose of the `Add()` method.
6. `RegisterTypes` is a standard `ITypeRegistration` implementation used to add our repository to the Unity container.
7. `ProductReviewController` can be used to test out some of the repository operations.
8. `RegisterRoutesProcessor` adds our test controller to the MVC route table. It is also patched in within this project's configuration patch.