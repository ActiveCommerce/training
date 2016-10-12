<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Checkout-Gift-Card.ascx.cs" Inherits="ActiveCommerce.GiftCards.skins.Checkout_Gift_Card" %>
<%@ Import Namespace="ActiveCommerce.SitecoreX.Globalization" %>

<% if (ActiveCommerce.SitecoreX.PageMode.IsEditing && Model.PaymentOption == null) { %>
<div class="editor-info-bar">
    Click the 'Edit Payment Configuration' button to configure the payment option (required).
</div>
<% } %>

<div ng-show="enabled" ng-controller="GiftCardCtrl" class="gift-card checkout-component" ng-init="code='<%=Model.PaymentOption != null ? Model.PaymentOption.Code : string.Empty %>'">

    <% if (!string.IsNullOrEmpty(Model.Title) || ActiveCommerce.SitecoreX.PageMode.IsEditing) { %>
    <h3><%=Editable(x => x.Title) %></h3>
    <% } %>
    
    <% if (!string.IsNullOrEmpty(Model.HeaderInstructions) || ActiveCommerce.SitecoreX.PageMode.IsEditing) { %>
        <div class="header-instructions"><%=Editable(x => x.HeaderInstructions) %></div>
    <% } %>

    <% if (!string.IsNullOrEmpty(Model.Instructions) || ActiveCommerce.SitecoreX.PageMode.IsEditing) { %>
    <div class="instructions">
    <%=Editable(x => x.Instructions) %>
    </div>
    <% } %>
    
    <div ac-alert message="{{message}}"></div>
    
    <ol class="form form-horizontal" ng-form="gcForm" ng-class="{submitted:submitted}">
	    <li class="gift-card-number">
	        <label for="number"><%=Editable(x => x.CardNumber)%></label>
            <input type="text" ng-model="card.CardNumber" name="number" ac-enter="applyGiftCard()" required />
            <label class="error" ng-show="submitted && gcForm.number.$error.required"><%=Translator.Render("Validation-Required") %></label>
        </li>
        
        <li class="gift-card-pin">
            <label for="pin"><%=Editable(x => x.Pin)%></label>
            <input type="text" class="input-mini" ng-model="card.Pin" name="pin" ac-enter="applyGiftCard()" autocomplete="off" />
        </li>
        
        <li class="gift-card-apply">
            <button type="button" ng-click="applyGiftCard()" class="bttn bttn-sm bttn-alt"><%=Editable(x => x.ApplyButton)%></button>
        </li>
    </ol>
    
    <ul class="applied">
        <li ng-repeat="payment in applied">
            <span class="gift-card-description">{{payment.Description}}</span> <span class="gift-card-amount">{{payment.Amount}}</span> <a ng-click="removeGiftCard(payment)"><%=Editable(x => x.RemoveButton)%></a>
        </li>
    </ul>

</div>