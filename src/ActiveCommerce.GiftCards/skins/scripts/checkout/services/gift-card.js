angular.module('checkout.services')

    .config(["$provide", function ($provide) {
        $provide.decorator('checkout', ['$delegate', '$http', function ($delegate, $http) {

            $delegate.applyGiftCard = function (code, card, config) {
                /// <summary>Applies a gift card</summary>
                /// <param name="code" type="String">The payment code</param>
                /// <param name="card" type="Object">The gift card details</param>
                /// <param name="config" type="Object">$http configuration (optional)</param>
                /// <returns type="Promise">A deferred promise</returns>

                return $http.post('/ac/giftcard/Apply', { code: code, card: card }, config);
            };

            $delegate.removeGiftCard = function (id, config) {
                /// <summary>Removes a gift card</summary>
                /// <param name="id" type="String">The payment id</param>
                /// <param name="config" type="Object">$http configuration (optional)</param>
                /// <returns type="Promise">A deferred promise</returns>

                return $http.post('/ac/giftcard/Remove', { id: id }, config);
            };

            return $delegate;
        }]);
    }]);