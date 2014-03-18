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
            var url = link.attr('rel');
            url = url.substring(0, url.lastIndexOf("/")) + '/' + val;
            link.attr('rel', url);
        });
    };

})(ActiveCommerce.Product.init);