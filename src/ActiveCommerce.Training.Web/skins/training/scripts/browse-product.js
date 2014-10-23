ActiveCommerce.BrowseProduct.config.productsPerPage = 20;

ActiveCommerce.BrowseProduct.addToCart = function(productCode) {
    //add logic here for displaying wait text or other indication that work is being done

    var url = "/ac/cart/addtocart/" + productCode;
    ActiveCommerce.Product.Cart.addProduct(url, function () {
        //disable wait text, etc
    });
    
}