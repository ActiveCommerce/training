using System;
using System.Web;
using Sitecore.Analytics;
using Sitecore.Analytics.Pipelines.InitializeTracker;
using Sitecore.Analytics.Web;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Web;

namespace ActiveCommerce.Training.PriceTesting.Analytics
{
    /// <summary>
    /// InitializeTracker pipeline processor which is inteded to make Sitecore Analytics behave more like
    /// Google Analytics, and restart the visit when a user re-enters from a different campaign, rather than
    /// just ignoring it.
    /// 
    /// Note: This does not work yet. :)
    /// </summary>
    public class RestartVisitOnNewCampaign : InitializeTrackerProcessor
    {
        public override void Process(InitializeTrackerArgs args)
        {
            if (HttpContext.Current == null)
            {
                args.AbortPipeline();
            }

            //no need to restart visit if visit is new
            if (Tracker.CurrentVisit.VisitPageCount < 1)
            {
                return;
            }

            //look for campaign id in query string
            Guid campaign;
            var campaignStr = WebUtil.GetQueryString(Settings.GetSetting("Analytics.CampaignQueryStringKey")).Trim();
            if (string.IsNullOrEmpty(campaignStr) || !Guid.TryParse(campaignStr, out campaign))
            {
                return;
            }

            //don't restart if the campaign isn't changing
            if (!Tracker.CurrentVisit.IsCampaignIdNull() && Tracker.CurrentVisit.CampaignId == campaign)
            {
                return;
            }

            //Tracker.EndVisit(false);

            //restart visit by setting new ID
            var visitCookie = new VisitCookie();
            visitCookie.VisitId = ID.NewID.Guid;
            visitCookie.Save();
        }
    }
}