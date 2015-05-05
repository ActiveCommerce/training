<%@ Control Language="c#" AutoEventWireup="true" Inherits="Coveo.UI.CoveoSearch" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Import Namespace="Coveo.UI" %>
<%@ Register TagPrefix="coveoui" Namespace="Coveo.UI.Controls" Assembly="Coveo.UI" %>

<!-- When customizing this component, ensure to use "Coveo.$" instead of the regular jQuery "$" to
     avoid any conflicts with Sitecore's Page Editor/Experience Editor.  -->

 <style>
	.CoveoResult {
		float: left;
		border-bottom: 0;
		width: 202px;
		height: 220px;
		margin: 0;
	}
	
	
 </style>
	 
<div id="body" class="wrapper generic-page">
    <div class="container-wide">
        <div id="content">
            <div class="container">
	 
<coveoui:ErrorSummary runat="server" />
<coveoui:WhenConfigured runat="server">
    <script type="text/javascript" src="/Coveo/js/cultures/<%= Model.CultureName %>.js"></script>
    <script type="text/javascript">
        Coveo.$(function() {
            CoveoForSitecore.componentsOptions = <%= Model.GetJavaScriptInitializationOptions() %>;
        });
    </script>

    <!-- This hidden input is required to bypass a problem with the Enter key causing a form submission
         if the form has exactly one text field, or only when there is a submit button present. -->
    <input type="text" class="fix-submit" />

    <div class="search-container">
        <div id="search" class="CoveoSearchInterface" data-enable-history="<%= Model.EnableHistory %>"
                                                      data-results-per-page="<%= Model.ResultsPerPage %>"
                                                      data-excerpt-length="<%= Model.ExcerptLength %>"
                                                      data-hide-until-first-query="<%= Model.HideUntilFirstQuery %>"
                                                      data-auto-trigger-query="<%= Model.AutoTriggerQuery %>">

            <% if (Model.AnalyticsEnabled) { %>
                <div class="CoveoAnalytics"
                    data-user="<%= Model.GetAnalyticsUser() %>"
                    data-endpoint="<%= Model.GetAnalyticsEndpoint() %>"
                    data-token="<%= Model.GetAnalyticsToken() %>"
                    data-search-hub="<%= Model.GetAnalyticsCurrentSiteName() %>"
                    data-send-to-cloud="<%= Model.CoveoAnalyticsEnabled %>">
                </div>
            <% } %>
            <div class="coveo-results-section">
                <% if (Model.DisplayFacets) { %>
                    <div class="coveo-results-column float-right">
                <% } else { %>
                    <div class="coveo-results-column">
                <% } %>
                    <% if (Model.DisplaySearchBox) { %>
                        <div class="coveo-searchBox-column">
                            <div class="CoveoSearchBox" data-activate-omnibox="<%= Model.ActivateOmniBox %>"
                                <% if (Model.ActivateOmniBox) { %>
                                    data-omnibox-delay="<%= Model.SearchBoxSuggestionsDelay %>"
                                <% } %>
                                <% if (Model.IsSearchAsYouTypeActivated) { %>
                                    data-enable-search-as-you-type="true"
                                    data-search-as-you-type-delay="<%= Model.SearchBoxSuggestionsDelay %>"
                                <% } %>
                            ></div>
                        </div>
                    <% } %>
                    </div>
                <% if (Model.DisplayFacets) { %>
                    <div class="coveo-facet-column">
                        <% if (Model.DisplayLogo) { %>
                            <div class="coveo-logo-column">
                                <div class="coveo-logo"></div>
                            </div>
                        <% } %>
                        <sc:placeholder key="coveo-facets" runat="server"></sc:placeholder>
                        &nbsp;
                    </div>
                <% } %>
                <% if (Model.DisplayFacets) { %>
                    <div class="coveo-results-column float-right">
                <% } else { %>
                    <div class="coveo-results-column">
                <% } %>
                    <% if (Model.DisplayBreadcrumb) { %>
                        <div class="CoveoBreadcrumb"></div>
                    <% } %>
                    <div class="coveo-results-header">
                        <div class="coveo-summary-section">
                            <% if (Model.DisplayQuerySummary) { %>
                                <span class="CoveoQuerySummary"></span>
                            <% } %>
                            <% if (Model.DisplayQueryDuration) { %>
                                <span class="CoveoQueryDuration"></span>
                            <% } %>
                        </div>

                        <% if (Model.DisplaySorting) { %>
                            <div class="coveo-sort-section">
                                <sc:placeholder key="coveo-sorts" runat="server"></sc:placeholder>
                            </div>
                        <% } %>
                    </div>
                    <div class="CoveoHiddenQuery"></div>

                    <% if (Model.DisplayDidYouMean) { %>
                        <div class="CoveoDidYouMean"></div>
                    <% } %>

                    <% if (Model.DisplayErrorReport) { %>
                        <div class="CoveoErrorReport"></div>
                    <% } %>

                    <% if (Model.DisplayResultList) { %>
                        <% if (Model.DisplayTopPager) { %>
                            <div class="CoveoPager" data-number-of-pages="<%= Model.PagerNumberOfPages %>"
                                                    data-show-previous-next="<%= Model.PagerShowPreviousNext %>"></div>
                        <% } %>
                        <div class="CoveoResultList" data-wait-animation="fade"
                                                     data-enable-infinite-scroll="<%= Model.EnableInfiniteScroll %>"
                                                     data-infinite-scroll-page-size="<%= Model.InfiniteScrollPageSize %>">

                            <script class="result-template" type="text/x-underscore-template">

								{{ if (raw.<%= ToCoveoFieldName("_ac_productimage", false) %>) { }}
									{{=image(raw['<%=ToCoveoFieldName("_ac_productimage", false)%>'], {alt : "Anything", width: 166, height: 166})   }}
								{{ } }}
							
                                <div class='coveo-title'>

                                    {{ if (raw.<%= ToCoveoFieldName("HasLayout", false) %> === "1" || raw.syssource !== "<%= Model.IndexSourceName %>") { }}
                                        <a class='CoveoResultLink'>{{=title?highlight(title, titleHighlights):clickUri}}</a>
                                    {{ } else { }}
                                        <span class='CoveoResultTitle'>{{=title?highlight(title, titleHighlights):''}}</span>
                                    {{ } }}

                                </div>
                                
								<span class="product-price">${{= raw.<%= ToCoveoFieldName("_ac_price", false) %> }}</span>
								
                            </script>
                        </div>

                        <% if (Model.DisplayBottomPager) { %>
                            <div class="CoveoPager" data-number-of-pages="<%= Model.PagerNumberOfPages %>"
                                                    data-show-previous-next="<%= Model.PagerShowPreviousNext %>"></div>
                        <% } %>
                    <% } %>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        Coveo.$(function() {
            Coveo.$('#search').coveoForSitecore('init', CoveoForSitecore.componentsOptions);
        });
    </script>
</coveoui:WhenConfigured>

            </div>
        </div>
    </div>
</div>

<% if (!Model.IsConfigured && SitecoreContext.IsEditingInPageEditor()) { %>
    <script type="text/javascript">
        Coveo.$(function() {
            Coveo.PageEditorDeferRefresh.triggerUpdate();
        });
    </script>
<% } %>
