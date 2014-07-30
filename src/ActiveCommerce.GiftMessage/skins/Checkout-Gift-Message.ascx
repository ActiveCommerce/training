<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Checkout-Gift-Message.ascx.cs" Inherits="ActiveCommerce.GiftMessage.skins.Checkout_Gift_Message" %>
<%@ Import Namespace="ActiveCommerce.SitecoreX.Globalization" %>

<div ng-controller="GiftMessageCtrl" class="checkout-component">
    
    <% if (!string.IsNullOrEmpty(Model.Title) || Sitecore.Context.PageMode.IsPageEditorEditing) { %>
    <h3><%=Editable(x => x.Title) %></h3>
    <% } %>
    
    <% if (!string.IsNullOrEmpty(Model.Instructions) || Sitecore.Context.PageMode.IsPageEditorEditing) { %>
    <div class="instructions">
    <%=Editable(x => x.Instructions) %>
    </div>
    <% } %>

    <ol class="form form-horizontal" ng-form="giftMessageForm" ng-class="{submitted:submitted}">
        <li>
	        <label for="giftText"><%=Editable(x => x.GiftMessage)%></label>
            <textarea ng-model="giftMessage.Text" name="giftText" rows="5" ng-maxlength="500" ng-pattern="/^[a-zA-Z0-9\s\'\.!\?\-]+$/"></textarea>
            <label class="error" ng-show="submitted && giftMessageForm.giftText.$error.maxlength"><%=Translator.Render("Gift-Validation-Maxlength") %></label>
            <label class="error" ng-show="submitted && giftMessageForm.giftText.$error.pattern"><%=Translator.Render("Gift-Validation-Pattern") %></label>
        </li>
    </ol>
</div>