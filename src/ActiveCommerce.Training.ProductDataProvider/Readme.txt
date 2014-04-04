This shows an example of a data provider which reads external field values only.

It is a prototype based on the idea that products would be created (and deleted?) via
a scheduled process, and then have values read in real time from an external source.

Missing from this example is a better way of handling the Sitecore caches. Ideally
we would allow the product data to be cached, but would clear data, item, and product caches
when data changed in the external system. The same mechanism could be used to reindex the
product when data has changed in the external system.