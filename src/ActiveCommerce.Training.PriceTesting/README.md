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


Google vs Sitecore Campaign Behavior
========
Note that Google and Sitecore behave a bit differently when it comes to campaigns. Out of the box, Sitecore will not trigger
a campaign unless it's the first page of a visit. Google on the other hand, will treat it as if a new visit has started whenever
a new campaign is triggered (c.f. https://support.google.com/analytics/answer/2731565).

If you want the price rule example (or any other campaign-driven rules) to work if a campaign ID is passed in during an existing
visit, you have two options:

* Add a rule condition which checks whether the campaign event was triggered at all during the visit. Even if it's not a landing
page, Sitecore will trigger a page event for the campaign. This works well, but keep in mind there may be a discrepancy between Sitecore and Google
Analytics. Keep in mind as well that this could also result in multiple rules evaluating to "true," if multiple campaigns have been triggered
on the same visit.
* Alter Sitecore analytics to behave like Google Analytics, and restart the visit if the campaign ID is added or changes. This
removes the analytics discrepancy, but may have side effects on other personalization rules (e.g. visit page count).

This POC includes examples of both for reference. See also: http://stackoverflow.com/questions/24537329/making-marketing-campaigns-in-sitecore-analytics-behave-like-google-analytics/24747566