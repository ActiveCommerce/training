using System;
using System.Linq;
using Sitecore.Analytics;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Rules;
using Sitecore.Rules.Conditions;

namespace ActiveCommerce.Training.PriceTesting.Rules
{
    public class HasCampaignAtAllCondition<T> : WhenCondition<T> where T: RuleContext
    {
        private readonly Guid _pageEventGuid;

        public string CampaignId { get; set; }

        public HasCampaignAtAllCondition()
        {
            _pageEventGuid = new Guid("{F358D040-256F-4FC6-B2A1-739ACA2B2983}");
        }

        protected override bool Execute(T ruleContext)
        {
            Assert.ArgumentNotNull(ruleContext, "ruleContext");
            if (!ID.IsID(CampaignId))
            {
                Log.Warn(string.Format("Could not convert value to guid: {0}", this.CampaignId), this);
                return false;
            }
            //you could remove the VisitId check to check if the campaign was triggered on any visit
            return Tracker.Visitor.DataSet.PageEvents.Any(row => row.PageEventDefinitionId == _pageEventGuid &&
                                                                 row.Data == this.CampaignId &&
                                                                 row.VisitId == Tracker.CurrentVisit.VisitId);
        }
    }
}