<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GoogleAnalytics-Campaign.ascx.cs" Inherits="ActiveCommerce.Training.PriceTesting.skins.GoogleAnalytics_Campaign" %>
<%@ Import Namespace="Sitecore.Analytics" %>

<%
    if (Model != null) {
%>
        <script type="text/javascript">
            _gaq.push(['_set', 'campaignParams', '<%=CampaignString%>'])
        </script>
<%
    }
%>