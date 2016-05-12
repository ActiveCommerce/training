angular.module('cart.components')

    .controller('EstimateShippingCtrl', ['$scope', 'cart', 'localize', function ($scope, cart, localize) {

        $scope.zipcode = "";
        $scope.options = [];

        $scope.$watch("cart.TotalCount", function (newValue, oldValue) {
            if (newValue != oldValue) {
                // if cart items changed, then reset the shipping options as these may have changed
                $scope.options = [];
            }
        });

        $scope.estimateShipping = function () {
            $scope.message = null;

            cart.estimateShipping($scope.zipcode).then(function (options) {
                $scope.options = options;

                if (options.length == 0) {
                    $scope.message = localize.text('Checkout-No-Shipping-Option');
                } else {
                    // refresh the cart totals as these may have changed (due to Estimated Shipping)
                    cart.get().then(function (result) {
                        $scope.cart.Totals = result.Totals;
                        $scope.cart.CartTotal = result.CartTotal;
                    });
                }
            }, function (reason) {
                if (reason && reason.data && reason.data.Message) {
                    $scope.message = reason.data.Message;
                }
            });
        };
    }]);