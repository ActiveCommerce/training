Price Testing POC
========
This a proof of concept example which demonstrates how you might implement price A/B testing in Active Commerce.

The scenario this POC was implemented for is price testing for a new product release. Using Sitecore marketing campaigns,
test groups of customers can be sent to your store and receive different pricing for the same product. This is done via a rule field
that allows you to dynamically change the default pricing tier that should be utilized. Rule conditions include Sitecore marketing campaigns.

Since Sitecore Analytics doesn't currently have a great way of tracking revenue, we can use Google Analytics campaigns
and the existing Active Commerce support for Google Ecommerce Analytics to track sales of the product for each campaign.

To make tracking in Google Analytics easier, the example also includes some extensions to Marketing Campaigns which allow
you to specify the Google campaign codes that should be triggered for the Sitecore campaign.

You can see a quick demo here:
https://www.youtube.com/watch?v=u0YQ-Kitkw8