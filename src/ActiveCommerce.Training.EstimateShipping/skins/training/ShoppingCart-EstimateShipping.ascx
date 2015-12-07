<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShoppingCart-EstimateShipping.ascx.cs" Inherits="ActiveCommerce.Training.EstimateShipping.skins.training.ShoppingCart_EstimateShipping" %>

<div id="estimate-shipping" ng-controller="EstimateShippingCtrl" ng-if="cart.TotalCount > 0">
    <h4><%=Editable(x => x.EstimateShippingHeader) %></h4>
    <div class="input">
        <label for="zipcode"><%=Editable(x => x.ZipCodeLabel) %></label>
        <input id="zipcode" ng-model="zipcode" ac-enter="estimateShipping()" name="zipcode" type="text" placeholder="<%=Model.ZipCodePlaceholder%>"/>
        <button type="button" ng-click="estimateShipping()" class="apply bttn bttn-sm bttn-alt"><%=Editable(x => x.SubmitButton) %></button>
    </div>
    <label class="error" ng-show="message">{{message}}</label>
    <p class="disclaimer" ng-show="options.length > 0"><%=Editable(x => x.Disclaimer) %></p>
    
    <ul>
        <li ng-repeat="option in options">
            {{option.Title}} - {{option.PriceDisplay}} <span class="discounts" ng-if="option.Price != option.DiscountedPrice">({{option.Discounts.join(',')}})</span>
        </li>
    </ul>
</div>