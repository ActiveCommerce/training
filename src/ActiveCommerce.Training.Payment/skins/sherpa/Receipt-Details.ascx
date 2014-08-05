<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Receipt-Details.ascx.cs" Inherits="ActiveCommerce.Web.skins.Receipt_Details" %>

<div id="thank-you" class="col">
    <h2><%=Model.ThankYou%></h2>
    <h3><%=Model.OrderReference%> <span>#<%=Order.OrderNumber%></span></h3>
    <p><%=Model.OrderEmail%> <%=Order.CustomerInfo.Email%></p>
	<p><%=Model.OrderInstructions%></p>
</div>
					
<div id="shipping-method" class="col">

    <h4 class="header-boxed"><%=Model.ShippingAddress%></h4>
    <div class="inner">    
		<p>
            <%=Order.CustomerInfo.ShippingAddress.Name%> <%=Order.CustomerInfo.ShippingAddress.Name2%><br />
            <%=Order.CustomerInfo.ShippingAddress.Address%><br />
            <% if (!String.IsNullOrWhiteSpace(Order.CustomerInfo.ShippingAddress.Address2)) { %>
                <%=Order.CustomerInfo.ShippingAddress.Address2%><br />
            <% } %>
            <%=Order.CustomerInfo.ShippingAddress.City%>, <%=Order.CustomerInfo.ShippingAddress.State%> <%=Order.CustomerInfo.ShippingAddress.Zip%><br />
            <%=Order.CustomerInfo.ShippingAddress.Country.Title%><br />
            <%=Order.CustomerInfo.ShippingAddress.GetPhoneNumber() ?? ""%>
        </p>
	</div>
                         	
    <h4 class="header-boxed"><%=Model.BillingAddress%></h4>
    <div class="inner">    
		<p>
            <%=Order.CustomerInfo.BillingAddress.Name%> <%=Order.CustomerInfo.BillingAddress.Name2%><br />
            <% if (!String.IsNullOrWhiteSpace(Order.CustomerInfo.BillingAddress.Address)) { %>
                <%=Order.CustomerInfo.BillingAddress.Address%><br />
                <% if (!String.IsNullOrWhiteSpace(Order.CustomerInfo.BillingAddress.Address2)) { %>
                    <%=Order.CustomerInfo.BillingAddress.Address2%><br />
                <% } %>
                <%=Order.CustomerInfo.BillingAddress.City%>, <%=Order.CustomerInfo.BillingAddress.State%> <%=Order.CustomerInfo.BillingAddress.Zip%><br />
                <%=Order.CustomerInfo.BillingAddress.Country.Title%><br />
            <% } %>
            <%=Order.CustomerInfo.BillingAddress.GetPhoneNumber() ?? ""%>
        </p>
	</div>
     
    <h4 class="header-boxed"><%=Model.PaymentMethod%></h4>
    <div class="inner">    
        <p>
            <% if (PaymentProvider is Sitecore.Ecommerce.Payments.OnlinePaymentProvider){ %>
                <%=Order.PaymentSystem.Title %>
            <% } else if (PaymentProvider is ActiveCommerce.Training.Payment.InvoicePaymentOption) { %>
                <%= Translator.Text("Payment-Invoice")%>: <%=(Order as ActiveCommerce.Training.OrderExtension.Order).PurchaseOrderNumber%>
            <% } else{ %>
                <%=Order.CreditCardData.CardType%> <%= Translator.Text("Credit-Card-Ending-In")%> <%=Order.CreditCardData.CardNumberLastFour%><br />
                Expiration <%=Order.CreditCardData.ExpirationDate%><br />
                <%= Translator.Text("Credit-Card-Amount-Charged") %> <%=PriceFormatter.FormatPrice(Order.Totals.TotalPriceIncVat, false)%>
            <% } %>
        </p>
	</div>
                         
</div>
