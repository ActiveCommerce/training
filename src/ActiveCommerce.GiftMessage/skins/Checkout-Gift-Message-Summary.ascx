<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Checkout-Gift-Message-Summary.ascx.cs" Inherits="ActiveCommerce.GiftMessage.skins.Checkout_Gift_Message_Summary" %>

<div ng-controller="GiftMessageSummaryCtrl" class="checkout-component">

    <% if (!string.IsNullOrEmpty(Model.Title) || Sitecore.Context.PageMode.IsPageEditorEditing) { %>
    <h3><%=Editable(x => x.Title) %></h3>
    <% } %>
    
    <p>
        {{giftMessage.Text}}
    </p>
</div>