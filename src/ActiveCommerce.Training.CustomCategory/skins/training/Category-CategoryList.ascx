<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Category-CategoryList.ascx.cs" Inherits="ActiveCommerce.Web.skins.CategoryCategoryList" %>
<%@ Import Namespace="ActiveCommerce.Training.CustomCategory" %>

<div id="body" class="wrapper category-page">
    <div class="container">	

        <ac:SkinnedSublayout runat="server" Path="/~skin~/Breadcrumb.ascx" />
        
		<div class="category-header">
		    <div class="intro">
		        <h1><%=Editable(x => x.Title) %></h1>
			    <p><%=Editable(x => x.Description)%></p>
                
                
                <%-- Training Stuff Here --%>
                <% if (Model is IMyCategoryBase) { %>
                    <p><%=Editable(x => (x as IMyCategoryBase).MyField)%></p>
                <% } %>
                

		    </div>
            <% if (!String.IsNullOrEmpty(Model.HeaderImage.GetUri()) || Sitecore.Context.PageMode.IsPageEditorEditing) { %>
            <div class="banner">
                <%=RenderImage(x => x.HeaderImage, new ImageParameters().FromSettings("Browse.Header"), true) %>
		    </div>
            <% } %>
	    </div> <!-- header -->
		
        <div id="content">

            <div class="sidebar">
                <h2><%=Editable(x => x.Title) %></h2>
			    <div class="sub-nav">
                    <ul>
                    <% foreach (var category in Model.Categories) { %>
                        <li><a href="<%=category.GetUrl() %>"><%=GlassHtml.Editable(category, x => x.Title) %></a></li>
                    <% } %>
                    </ul>
			    </div>       
            </div><!-- local -->
				
            <div class="main">
            <% var even = true;
                foreach(var category in Model.Categories) {
                    even = !even; %>
                <div class="category<%=even ? " even" : "" %>">
                    <div class="image">
                        <a href="<%=category.GetUrl() %>">
                            <%=GlassHtml.RenderImage(category, x => x.Image, new ImageParameters().FromSettings("Browse.Category"), true)%>
                        </a>
                    </div>
                    <a class="details" href="<%=category.GetUrl() %>">
                        <span class="name"><%=GlassHtml.Editable(category, x => x.Title) %></span>
                        <% if (!String.IsNullOrEmpty(category.ShortDescription) || Sitecore.Context.PageMode.IsPageEditorEditing) { %>
                        <span class="desc"><%=GlassHtml.Editable(category, x => x.ShortDescription) %></span>
                        <% } %>
                    </a>
                </div>
            <% } %>
            </div><!-- main-->

        </div>
                
    </div>
</div>
