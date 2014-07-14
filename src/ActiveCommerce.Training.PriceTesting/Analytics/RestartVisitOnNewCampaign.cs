using System;
using System.Net;
using System.Web;
using Sitecore.Analytics;
using Sitecore.Analytics.Data.DataAccess;
using Sitecore.Analytics.Data.DataAccess.DataSets;
using Sitecore.Analytics.Pipelines.CreateVisits;
using Sitecore.Analytics.Pipelines.InitializeTracker;
using Sitecore.Analytics.Web;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Sites;
using Sitecore.Web;

namespace ActiveCommerce.Training.PriceTesting.Analytics
{
    /// <summary>
    /// InitializeTracker pipeline processor which is inteded to make Sitecore Analytics behave more like
    /// Google Analytics, and restart the visit when a user re-enters from a different campaign, rather than
    /// just ignoring it.
    /// </summary>
    public class RestartVisitOnNewCampaign : InitializeTrackerProcessor
    {
        public override void Process(InitializeTrackerArgs args)
        {
            if (HttpContext.Current == null)
            {
                args.AbortPipeline();
                return;
            }

            //no need to restart visit if visit is new
            if (Tracker.Visitor.Settings.IsNew || Tracker.Visitor.Settings.IsFirstRequest || Tracker.CurrentVisit.VisitPageCount < 1)
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

            var current = HttpContext.Current;
            var cookie = current.Response.Cookies["SC_ANALYTICS_SESSION_COOKIE"];
            if (cookie == null)
            {
                cookie = new HttpCookie("SC_ANALYTICS_SESSION_COOKIE");
                current.Response.Cookies.Add(cookie);
            }
            cookie.Value = "";
        }

    }
}