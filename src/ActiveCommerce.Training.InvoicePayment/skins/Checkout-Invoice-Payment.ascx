<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Checkout-Invoice-Payment.ascx.cs" Inherits="ActiveCommerce.Training.InvoicePayment.skins.Checkout_Invoice_Payment" %>

<div ng-controller="InvoicePaymentCtrl" class="checkout-component">
    
    <% if (!string.IsNullOrEmpty(Model.Title) || ActiveCommerce.SitecoreX.PageMode.IsEditing) { %>
        <h3><%=Editable(x => x.Title) %></h3>
    <% } %>
    
    <% if (!string.IsNullOrEmpty(Model.HeaderInstructions) || ActiveCommerce.SitecoreX.PageMode.IsEditing) { %>
        <div class="header-instructions"><%=Editable(x => x.HeaderInstructions) %></div>
    <% } %>

    <% if (!string.IsNullOrEmpty(Model.Instructions) || ActiveCommerce.SitecoreX.PageMode.IsEditing) { %>
        <div class="instructions"><%=Editable(x => x.Instructions) %></div>
    <% } %>
    
    <ol class="form form-horizontal" ng-form="purchaseOrderForm" ng-class="{submitted:submitted}">
        <li>
	        <label for="purchaseOrderNumber"><%=Editable(x => x.PurchaseOrderNumber)%></label>
            <input type="tel" ng-model="purchaseOrder.Number" name="purchaseOrderNumber" pattern="[0-9]*" ng-maxlength="500" restrict-numeric required />
            <label class="error" ng-show="submitted && purchaseOrderForm.purchaseOrderNumber.$error.required"><%=Translator.Render("Validation-Required") %></label>
            <label class="error" ng-show="submitted && purchaseOrderForm.purchaseOrderNumber.$error.maxlength"><%=Translator.Render("Invoice-Validation-Maxlength") %></label>
            <label class="error" ng-show="submitted && purchaseOrderForm.purchaseOrderNumber.$error.pattern"><%=Translator.Render("Invoice-Validation-Pattern") %></label>
        </li>
    </ol>

</div>