Product Link Provider
=====================
This example shows how to add a custom link provider to allow Active Commerce products to be used in links 
more generically (such as Generic Link field and links in Rich Text fields), while maintaining product url resolution.

Note we're simply overriding the default Sitecore LinkProvider. An alternative (better) approach would be to 
implement a pipeline-based LinkProvider, as described here: http://thegrumpycoder.com/post/78684655662/sitecore-pipeline-enabled-linkprovider