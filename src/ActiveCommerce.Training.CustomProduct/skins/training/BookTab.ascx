<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BookTab.ascx.cs" Inherits="ActiveCommerce.Training.CustomProduct.skins.training.BookTab" %>

<% if(ActiveCommerce.SitecoreX.PageMode.IsEditing) { %>
    <ul class="ui-tabs-nav">
        <li class="ui-state-active"><a href="#<%=Id %>"><%=ActiveCommerce.SitecoreX.Globalization.Translator.Render(TitleKey)%></a></li>
    </ul>
<%} %>

<div id="<%=Id %>" class="ui-tabs-panel">
    <div class="body">
        <%= GlassHtml.Editable(Book, x => x.Genre) %> <br />
        <%= GlassHtml.Editable(Book, x => x.Author) %> <br />
        <%= GlassHtml.Editable(Book, x => x.PublishDate) %>
    </div>
</div>