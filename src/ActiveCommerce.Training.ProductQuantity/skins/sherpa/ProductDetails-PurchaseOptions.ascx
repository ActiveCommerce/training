<%@ Control Language="C#" AutoEventWireup="true" Inherits="ActiveCommerce.Web.skins.ProductDetails_PurchaseOptions" %>

<div class="purchase-options">

    <% if ((Model is ActiveCommerce.Products.VariableProduct) && (Model as ActiveCommerce.Products.VariableProduct).Variants.Any()) { %>
    <ol class="form options">
        <% foreach (var option in (Model as ActiveCommerce.Products.VariableProduct).Options) { %>
        <li>
            <label for="<%=HttpUtility.HtmlEncode(option.Key) %>"><%=option.DisplayName %>:</label>
            <select name="<%=HttpUtility.HtmlEncode(option.Key) %>" id="<%=HttpUtility.HtmlEncode(option.Key) %>" data-is-color="<%=option.IsColor %>" class="required">
                <option value=""><%=String.Format("{0} {1}", Translator.Text("Product-Select-Option"), option.DisplayName)%></option>
                <% foreach (var value in option.Values) { %>
                <option value="<%=HttpUtility.HtmlEncode(value.Value) %>"><%=value.DisplayValue%></option>
                <% } %>
            </select>
        </li>    
        <% } %>
    </ol>

    <script type="text/javascript">
        extend('ActiveCommerce.Product.Variants.serverConfig', {
            store: <%= ViewModel.Variants.ToJSON() %>
        });
    </script>
    <% } %>

	<div id="btn-add-to-cart">
	    <label>Quantity: <input name="qty" id="options-qty" type="text" value="1" /></label> <br />
        <a href="#" rel="/ac/cart/addtocart/<%=Model.Code %>/1" class="add-to-cart bttn bttn-primary bttn-block causes-validation <%=ViewModel.IsInStock ? "" : "disabled" %>" data-analytics-id="<%=Model.SKU %>~<%=Model.Name %>" data-analytics-category="ActiveCommerce-ProductDetail" data-waittext="<%=Translator.Text("Product-Add-To-Cart-Adding") %>"><%=Translator.Render("Product-Add-To-Cart")%></a>
    </div>
	
</div><!-- purchase-options -->