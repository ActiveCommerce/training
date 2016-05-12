angular.module('cart.services')

    .config(["$provide", function ($provide) {
        $provide.decorator('cart', ['$delegate', '$http', function ($delegate, $http) {

            $delegate.estimateShipping = function (zipcode, config) {
                return $http.post('/ac/cart/EstimateShipping', { zipcode: zipcode }, config).then(function (response) {
                    return response.data;
                });
            };

            return $delegate;
        }]);
    }]);