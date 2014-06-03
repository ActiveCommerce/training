Custom Category Template
========
The builtin category sublayouts for Active Commerce are dependent on Glass type inference.

This means that if you would like to inherit from our templates, even if you are not adding
your own fields, you'll need to create your own domain object with a [SitecoreType] attribute
that includes your template ID, and ensure your assembly is in the Active Commerce glassConfiguration.

For more info on Inferred Types in Glass: http://glass.lu/docs/tutorial/sitecore/tutorial17/tutorial17.html