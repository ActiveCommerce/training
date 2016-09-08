<%@ Control Language="c#" AutoEventWireup="true" Inherits="Coveo.UI.CoveoSearch" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Import Namespace="Coveo.UI" %>
<%@ Register TagPrefix="coveoui" Namespace="Coveo.UI.Controls" Assembly="Coveo.UIBase" %>

<!-- When customizing this component, ensure to use "Coveo.$" instead of the regular jQuery "$" to
     avoid any conflicts with Sitecore's Page Editor/Experience Editor.  -->

<style>
	.CoveoResult {
		float: left;
		border-bottom: 0;
		width: 204px;
		height: 220px;
	}
    .coveo-facet-column label {
        line-height: inherit;
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

    <div id="<%= Model.Id %>"
         class="CoveoSearchInterface"
         data-design='new'
         data-enable-history="<%= Model.EnableHistory %>"
         data-results-per-page="<%= Model.ResultsPerPage %>"
         data-excerpt-length="<%= Model.ExcerptLength %>"
         data-hide-until-first-query="<%= Model.HideUntilFirstQuery %>"
         data-auto-trigger-query="<%= Model.AutoTriggerQuery %>"
        <% if (Model.IsMaximumAgeSet) { %>
            data-maximum-age="<%= Model.MaximumAge %>"
        <% } %>
        <% if (Model.UseCustomQueryPipeline) { %>
            data-pipeline="<%: Model.QueryPipelineName %>"
        <% } %>>

        <% if (Model.AnalyticsEnabled) { %>
            <div class="CoveoAnalytics"
                 data-anonymous="<%= Model.IsUserAnonymous %>"
                 data-endpoint="<%= Model.GetAnalyticsEndpoint() %>"
                 data-search-hub="<%= Model.GetAnalyticsCurrentPageName() %>"
                 data-send-to-cloud="<%= Model.CoveoAnalyticsEnabled %>">
            </div>
        <% } %>

        <div class="coveo-main-section">
            <% if (Model.DisplayTabs) { %>
                <div class="coveo-tab-section coveo-placeholder-fix">
                    <sc:placeholder key="coveo-tabs" runat="server"></sc:placeholder>
                </div>
            <% } %>
            <% if (Model.DisplayFacets) { %>
                <div class="coveo-facet-column">
                    <% if (Model.DisplayLogo) { %>
                        <div class="coveo-logo"></div>
                    <% } %>
                    <sc:placeholder key="coveo-facets" runat="server"></sc:placeholder>
                    &nbsp;
                </div>
            <% } %>
            <div class="coveo-results-column">
                <% if (Model.DisplaySearchbox) { %>
                    <div class="CoveoSearchbox CoveoSearchPageSearchbox"
                         data-auto-focus="<%= Model.AutoFocus %>"
                         data-enable-lowercase-operators="<%= Model.EnableLowercaseOperators %>"
                         data-enable-partial-match="<%= Model.EnablePartialMatch %>"
                         data-partial-match-keywords="<%= Model.PartialMatchKeywords %>"
                         data-partial-match-threshold="<%= Model.PartialMatchThreshold %>"
                         data-enable-question-marks="<%= Model.EnableQuestionMarks %>"
                         data-enable-wildcards="<%= Model.EnableWildcards %>"
                        <% if (Model.EnableOmnibox) { %>
                            data-enable-omnibox="true"
                            data-omnibox-timeout="<%= Model.OmniboxTimeout %>"
                            data-enable-field-addon="<%= Model.OmniboxEnableFieldAddon %>"
                            data-enable-simple-field-addon="<%= Model.OmniboxEnableSimpleFieldAddon %>"
                            data-enable-top-query-addon="<%= Model.OmniboxEnableTopQueryAddon %>"
                            data-enable-reveal-query-suggest-addon="<%= Model.OmniboxEnableRevealQuerySuggestAddon %>"
                            data-enable-query-extension-addon="<%= Model.OmniboxEnableQueryExtensionAddon %>"
                        <% } %>
                        <% if (Model.IsSearchAsYouTypeEnabled) { %>
                            data-enable-search-as-you-type="true"
                            data-search-as-you-type-delay="<%= Model.SearchboxSuggestionsDelay %>"
                        <% } %>
                    ></div>
                <% } %>
                <% if (Model.DisplayBreadcrumb) { %>
                    <div class="CoveoBreadcrumb"></div>
                <% } %>
                <div class="coveo-results-header">
                    <div class="coveo-summary-section">
                        <% if (Model.DisplayQuerySummary) { %>
                            <span class="CoveoQuerySummary"
                                  data-enable-search-tips="<%= Model.QuerySummaryEnableSearchTips %>"
                                  data-only-display-search-tips="<%= Model.QuerySummaryOnlyDisplaySearchTips %>"></span>
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
                    <div class="CoveoDidYouMean"
                         data-enable-auto-correction="<%= Model.EnableDidYouMeanAutoCorrection %>"></div>
                <% } %>

                <% if (Model.DisplayErrorReport) { %>
                    <div class="CoveoErrorReport"></div>
                <% } %>

                <% if (Model.DisplayResultList) { %>
                    <% if (Model.DisplayTopPager) { %>
                        <div class="CoveoPager" data-number-of-pages="<%= Model.PagerNumberOfPages %>"
                                                data-max-number-of-pages="<%= Model.PagerMaxNumberOfPages %>"
                                                data-enable-navigation-button="<%= Model.EnablePagerNavigationButton %>"></div>
                    <% } %>
                    <div class="CoveoResultList" data-wait-animation="fade"
                                                 data-enable-infinite-scroll="<%= Model.EnableInfiniteScroll %>"
                                                 data-infinite-scroll-page-size="<%= Model.InfiniteScrollPageSize %>">

                        <script class="result-template" type="text/underscore">
                            <div class="coveo-result-frame">
                                <div class="coveo-result-row">
                                    <div class="coveo-result-cell">
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
                                    </div>
                                </div>
                            </div>
                        </script>
                    </div>

                    <% if (Model.DisplayBottomPager) { %>
                        <div class="CoveoPager" data-number-of-pages="<%= Model.PagerNumberOfPages %>"
                                                data-max-number-of-pages="<%= Model.PagerMaxNumberOfPages %>"
                                                data-enable-navigation-button="<%= Model.EnablePagerNavigationButton %>"></div>
                    <% } %>
                <% } %>
            </div>
        </div>
    </div>
    <script class='result-template' id='<%= Model.Id %>CoveoQuickviewTemplate' type='text/underscore'>
        <div class='coveo-quick-view-header'>
            <table class='CoveoFieldTable'>
                <tr data-field='@sysdate' data-caption='Date' data-helper='dateTime' />
                <tr data-field='@sysauthor' data-caption='Author' />
                <tr data-field='@clickuri' data-html-value='true' data-caption='URL' data-helper='anchor' data-helper-options='{text: "{{= clickUri }}", target: "_blank"}'>
            </table>
            <div class='CoveoQuickviewDocument'></div>
        </div>
    </script>
    <script type="text/javascript">
        Coveo.$(function() {
            Coveo.$('#<%= Model.Id %>').coveoForSitecore('init', CoveoForSitecore.componentsOptions);
        });
    </script>
</coveoui:WhenConfigured>

            </div>
        </div>
    </div>
</div>

<% if (Model.HasErrors) { %>
    <div class="CoveoServerError">
        <h3><%= Model.Labels[LocalizedStringKeys.FRIENDLY_SEARCH_UNAVAILABLE_TITLE] %></h3>
        <h4><%= Model.Labels[LocalizedStringKeys.FRIENDLY_SEARCH_UNAVAILABLE_DETAIL] %></h4>
    </div>

    <% if (SitecoreContext.IsEditingInPageEditor()) { %>
        <script type="text/javascript">
            Coveo.$(function() {
                Coveo.PageEditorDeferRefresh.triggerUpdate();
            });
        </script>
    <% } %>
<% } %>
