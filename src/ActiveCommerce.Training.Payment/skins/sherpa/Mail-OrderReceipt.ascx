<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Mail-OrderReceipt.ascx.cs" Inherits="ActiveCommerce.Web.skins.Mail_OrderReceipt" %>
<%@ Import Namespace="ActiveCommerce.Extensions" %>

<ac:SkinnedSublayout runat="server" Path="/~skin~/Mail-Header.ascx"/>

<table cellpadding="0" cellspacing="0" border="0" width="100%">
    <tr>
        <td width="100%" bgcolor="#fff" align="center">
        	<table cellpadding="0" cellspacing="0" border="0" width="700">
        		<tr>
        			<td colspan="3" height="20"></td>
        		</tr>
        		<tr>
        			<td align="left" width="514" valign="top">
        				<table cellpadding="0" cellspacing="0" border="0" width="100%">
        					<tr>
        						<td bgcolor="#fff">
        							<table cellpadding="0" cellspacing="0" border="0" width="100%">
        								<tr>
        									<td height="20" colspan="3"></td>
        								</tr>
        								<tr>
        									<td width="20"></td>
											<td width="464" valign="top">
												<h1 style="font-family:Arial, Helvetica, sans-serif;font-size:27px;margin:0 0 5px 0;padding:0;font-weight:normal;"><%=Model.Email.TitleFormatted %></h1>
												<h2 style="font-family:Arial, Helvetica, sans-serif;font-size:17px;margin:0 0 10px 0;padding:0;"><%=Model.OrderReference %> <span style="color:#b00;">#<%=Model.Order.OrderNumber%></span></h2>
												<%=Model.Email.MessageFormatted %>
                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
													<tr>
														<td width="220" valign="top">
															<h3 style="margin:0 0 10px;font-family:Arial, Helvetica, sans-serif; color:#333; padding: 5px 10px; font-size: 14px; background: #f5f5f5;"><%=Model.ShippingAddress %></h3>
															<p style="font-family:Arial, Helvetica, sans-serif;font-size:12px;color:#333;padding:0 10px;">
                                                                <%=Model.Order.CustomerInfo.ShippingAddress.Name %> <%=Model.Order.CustomerInfo.ShippingAddress.Name2 %><br />
																<%=Model.Order.CustomerInfo.ShippingAddress.Address %><br />
                                                                <% if (!string.IsNullOrWhiteSpace(Model.Order.CustomerInfo.ShippingAddress.Address2)) { %>
                                                                    <%=Model.Order.CustomerInfo.ShippingAddress.Address2 %><br />
                                                                <% } %>
																<%=Model.Order.CustomerInfo.ShippingAddress.City %>, <%=Model.Order.CustomerInfo.ShippingAddress.State %> <%=Model.Order.CustomerInfo.ShippingAddress.Zip %><br />
																<%=Model.Order.CustomerInfo.ShippingAddress.GetPhoneNumber() %>
															</p>
															<br />
															<h3 style="margin:0 0 10px;font-family:Arial, Helvetica, sans-serif; color:#333; padding: 5px 10px; font-size: 14px; background: #f5f5f5;"><%=Model.BillingAddress %></h3>
															<p style="font-family:Arial, Helvetica, sans-serif;font-size:12px;color:#333;padding:0 10px;">
																<%=Model.Order.CustomerInfo.BillingAddress.Name %> <%=Model.Order.CustomerInfo.BillingAddress.Name2 %><br />
                                                                <% if (!string.IsNullOrWhiteSpace(Model.Order.CustomerInfo.BillingAddress.Address)) { %>
																    <%=Model.Order.CustomerInfo.BillingAddress.Address %><br />
                                                                    <% if (!string.IsNullOrWhiteSpace(Model.Order.CustomerInfo.BillingAddress.Address2)) { %>
                                                                        <%=Model.Order.CustomerInfo.BillingAddress.Address2 %><br />
                                                                    <% } %>
																    <%=Model.Order.CustomerInfo.BillingAddress.City %>, <%=Model.Order.CustomerInfo.BillingAddress.State %> <%=Model.Order.CustomerInfo.BillingAddress.Zip %><br />
																<% } %>
                                                                <%=Model.Order.CustomerInfo.BillingAddress.GetPhoneNumber() %>
															</p>
															<br />
															<h3 style="margin:0 0 10px;font-family:Arial, Helvetica, sans-serif; color:#333; padding: 5px 10px; font-size: 14px; background: #f5f5f5;"><%=Model.PaymentMethod %></h3>
															<p style="font-family:Arial, Helvetica, sans-serif;font-size:12px;color:#333;padding:0 10px;">
                                                                <% if (Model.PaymentProvider is Sitecore.Ecommerce.Payments.OnlinePaymentProvider)
                                                                   { %>
                                                                    <%=Model.Order.PaymentSystem.Title %>
                                                                <% } else if (Model.PaymentProvider is ActiveCommerce.Training.Payment.InvoicePaymentOption) { %>
                                                                    <%= Translator.Text("Payment-Invoice")%>: <%=(Model.Order as ActiveCommerce.Training.OrderExtension.Order).PurchaseOrderNumber%>
                                                                <% } else{ %>
                                                                    <%=Model.Order.CreditCardData.CardType %> <%=Translator.Text("Credit-Card-Ending-In") %> <%=Model.Order.CreditCardData.CardNumberLastFour %><br />
				                                                    <%=Translator.Text("Credit-Card-Expiration") %> <%=Model.Order.CreditCardData.ExpirationDate %><br />
				                                                    <%=Translator.Text("Credit-Card-Amount-Charged") %> <%=PriceFormatter.FormatPrice(Model.Order.Totals.TotalPriceIncVat, false)%>
                                                                <% } %>
															</p>
														</td>
														<td width="10"></td>
														<td width="224" valign="top">
															<h3 style="margin:0 0 10px; font-family:Arial, Helvetica, sans-serif; color:#333; padding: 10px; font-size: 21px; background: #f5f5f5;"><%=Model.OrderSummaryHeader %></h3>
															<table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                <% foreach (var product in Model.OrderSummary.Products) { %>
                                                                <tr>
																	<td width="10"></td>
																	<td>
																		<h4 style="margin:0 0 5px; font-family:Arial, Helvetica, sans-serif; color:#333; font-size: 15px;" valign="top"><%=product.Name %></h4>
                                                                        <% if (!String.IsNullOrEmpty(product.Details)) { %>
																		<p style="font-family:Arial, Helvetica, sans-serif;font-size:10px;color:#333;padding:0 0 10px;margin:0;"><%=product.Details %></p>
                                                                        <% } %>
																		<p style="font-family:Arial, Helvetica, sans-serif;font-size:10px;color:#333;padding:0 0 10px;margin:0;"><%=Translator.Render("Quantity") %>: <%=product.Quantity %></p>
																	</td>
																	<td align="right" valign="top">
																		<p style="font-family:Arial, Helvetica, sans-serif;font-size:12px;color:#333;padding:0 0 10px;margin:0;"><%=product.Price %></p>
																	</td>
																	<td width="10"></td>
																</tr>
																<tr><td colspan="4"><hr style="height: 0;margin: 10px 0 20px;"></td></tr>
																<% } %>
                                                                <% foreach (var total in Model.OrderSummary.Totals) { %>
                                                                <tr>
																	<td width="10"></td>
																	<td style="font-family:Arial, Helvetica, sans-serif;font-size:12px;color:#333;padding:0;margin:0;"><%=total.Description %></td>
																	<td align="right" valign="top" style="font-family:Arial, Helvetica, sans-serif;font-size:12px;color:#333;padding:0 0 5px;margin:0;"><%=total.Cost %></td>
																	<td width="10"></td>
																</tr>
                                                                <% } %>
																<tr>
																	<td width="10"></td>
																	<td valign="top" style="font-family:Arial, Helvetica, sans-serif;font-size:12px;color:#333;padding:0;margin:0;font-weight:bold;"><%=Translator.Text("Checkout-Total")%></td>
																	<td align="right" valign="top" style="font-family:Arial, Helvetica, sans-serif;font-size:26px;color:#333;padding:0 0 5px;margin:0;font-weight:bold;"><%=Model.OrderSummary.CartTotal %></td>
																	<td width="10"></td>
																</tr>
															</table>
														</td>
													</tr>
												</table>
                                                <p><a href="<%=Model.Order.GetOrderDetailsUrl() %>"><%=Translator.Render("Receipt-Order-Details-Link") %></a></p>
												<%=Model.Email.FootnoteFormatted %>
											</td>
											<td width="20"></td>
        								</tr>
										<tr>
        									<td height="20" colspan="3"></td>
        								</tr>
        							</table>
        						</td>
        					</tr>
        				</table>
        			</td>
					<td width="20"></td>
					<td align="left" width="166" valign="top">
						<ac:SkinnedSublayout runat="server" Path="/~skin~/Mail-PromoBanners.ascx"/>
					</td>
        		</tr>
				<tr>
        			<td colspan="3" height="20"></td>
        		</tr>
				<tr>
					<td>
						<%=Model.Email.FooterFormatted %>
					</td>
					<td></td>
					<td></td>
				</tr>
				<tr>
        			<td colspan="3" height="20"></td>
        		</tr>
        	</table>
        </td>
	</tr>
</table>