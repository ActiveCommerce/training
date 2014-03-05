angular.module('checkout.services')


    //extends the existing checkout service. can add or override service functions this way
    .config(["$provide", function($provide) {
        $provide.decorator('checkout', ['$delegate', '$http', function ($delegate, $http) {

            $delegate.updateGiftMessage = function(giftMessage, config) {
                return $http.post('/ac/checkout/UpdateGiftMessage', { giftMessage: giftMessage }, config);
            };

            /* This would override the exsting updateShippingAddress function
            $delegate.updateShippingAddress = function(address) {
                return $http.post('/checkout/UpdateShippingAddress', address).then(function(response) {
                    console.log("update has been decorated!");
                    $delegate.set(response.data);
                });
            };*/
            
            return $delegate;
        }]);
    }]);