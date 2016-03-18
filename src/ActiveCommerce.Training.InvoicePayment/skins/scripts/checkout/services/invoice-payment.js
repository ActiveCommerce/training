angular.module('checkout.services')


    //extends the existing checkout service. can add or override service functions this way
    .config(["$provide", function($provide) {
        $provide.decorator('checkout', ['$delegate', '$http', function ($delegate, $http) {

            $delegate.updatePurchaseOrderNumber = function (code, purchaseOrderNumber, config) {
                return $http.post('/ac/invoicepayment/UpdatePurchaseOrderNumber', { code: code, purchaseOrderNumber: purchaseOrderNumber }, config);
            };
            
            return $delegate;
        }]);
    }]);