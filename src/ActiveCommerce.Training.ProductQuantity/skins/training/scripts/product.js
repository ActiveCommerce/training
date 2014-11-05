ActiveCommerce.Product.init = (function(baseInit) {

    return function () {
        baseInit();
        $('#options-qty').on('change', function(e) {
            var qty = $(this);
            var val = qty.val();
            if (!val || val < 1) {
                val = 1;
            }
            var link = ActiveCommerce.Product.Variants.$elements.addToCart;
            var url = link.attr('rel').split("/");
            url.splice(5, url.length - 4);
            url.push(val);
            link.attr('rel', url.join("/"));
        });
    };

})(ActiveCommerce.Product.init);


ActiveCommerce.Product.Variants.updateDisplay = (function (baseUpdateDisplay) {

    return function () {
        baseUpdateDisplay();
        // Force a call to quatity change to ensure it stays in the rel attribute
        $('#options-qty').change();
    };
})(ActiveCommerce.Product.Variants.updateDisplay);
