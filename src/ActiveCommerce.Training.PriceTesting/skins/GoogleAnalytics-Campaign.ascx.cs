using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ActiveCommerce.Training.PriceTesting.Analytics;
using Sitecore.Analytics;
using Sitecore.StringExtensions;

namespace ActiveCommerce.Training.PriceTesting.skins
{
    public partial class GoogleAnalytics_Campaign : System.Web.UI.UserControl
    {
        protected CampaignItem Model;

        protected virtual void Page_Load(object sender, EventArgs e)
        {
            if (Tracker.CurrentVisit.IsCampaignIdNull())
            {
                this.Visible = false;
                return;
            }

            var campaignId = Tracker.CurrentVisit.CampaignId;
            Model = (CampaignItem)Tracker.DefinitionItems.Campaigns[campaignId].InnerItem;
        }

        protected virtual string CampaignString
        {
            get
            {
                if (Model == null)
                {
                    return string.Empty;
                }

                var query = new StringBuilder();
                if (!string.IsNullOrEmpty(Model.GoogleCampaign))
                {
                    query.Append("{0}={1}&".FormatWith("utm_campaign", HttpUtility.JavaScriptStringEncode(Model.GoogleCampaign)));
                }
                if (!string.IsNullOrEmpty(Model.GoogleSource))
                {
                    query.Append("{0}={1}&".FormatWith("utm_source", HttpUtility.JavaScriptStringEncode(Model.GoogleSource)));
                }
                if (!string.IsNullOrEmpty(Model.GoogleMedium))
                {
                    query.Append("{0}={1}&".FormatWith("utm_medium", HttpUtility.JavaScriptStringEncode(Model.GoogleMedium)));
                }
                if (query.Length == 0)
                {
                    return string.Empty;
                }
                return query.ToString(0, query.Length - 1);
            }
        }
    }
}