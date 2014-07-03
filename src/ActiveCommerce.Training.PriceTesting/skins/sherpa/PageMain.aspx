<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="PageMain.aspx.cs" Inherits="ActiveCommerce.Web.skins.PageMain" %>
<%@ OutputCache Location="None" VaryByParam="none" %>
<!DOCTYPE html>
<html class="no-js" lang="<%=Sitecore.Context.Language.Name %>">
    <head id="head" runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />

        <%-- Viewport/Title/Description added in MainContainer code-behind --%>
        <%-- Meta fields, styles, scripts placed in order below --%>
        <asp:Placeholder runat="server" ID="uxHeadMeta" />
        <%: Bundles.RenderStylesheets() %>
        <%  if (!Sitecore.Context.PageMode.IsPageEditor) { %>
        <style type="text/css">
            /* For AngularJs cloaking (since we're not loading the js in head): http://docs.angularjs.org/api/ng.directive:ngCloak */
            [ng\:cloak], [ng-cloak], [data-ng-cloak], [x-ng-cloak], .ng-cloak, .x-ng-cloak {
                display: none !important;
            }
        </style>
        <% } %>
        <%: Bundles.RenderScripts("head")%>

        <%-- Google Analytics is last, per their spec --%>
        <ac:SkinnedSublayout runat="server" Path="/~skin~/GoogleAnalytics.ascx" />
        
        <%-- ADDED: Campaign Tracking --%>
        <ac:SkinnedSublayout runat="server" Path="/~skin~/GoogleAnalytics-Campaign.ascx" />
    </head>

    <body runat=server id="mainbody" >
    <form id="mainform" runat="server">
        <sc:Placeholder ID="PlaceholderMain" runat="server" Key="main" />
    </form>
    <%  if (!Sitecore.Context.PageMode.IsPageEditor) { %>
        <%: Bundles.RenderScripts() %>
        <%: Bundles.RenderHtmlTemplates() %>
    <% } %>
    </body>
</html>
