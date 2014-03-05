angular.module('checkout.components')


    .controller('GiftMessageCtrl', ['$scope', 'localize', 'checkout', function($scope, localize, checkout) {

        //init our model
        $scope.giftMessage = [];

        // EVENTS
        $scope.$on("activated", function (e, args) {
            //must extend the CheckoutViewModel and CheckoutViewModelFactory for this to work
            checkout.get().then(function (state) {
                $scope.giftMessage = state.GiftMessage;
                $scope.giftMessageForm.$setDirty();
            });
        });

        $scope.$on("validate", function (e, args) {

            if ($scope.giftMessageForm.$invalid) {
                args.reject(localize.text('Validation-All-Fields'));
                return;
            }
            
            //could call services here for server-side validation as well
        });

        $scope.$on("process", function (e, args) {
            //pass the update operation back to the event args. must return a promise.
            args.defer(checkout.updateGiftMessage($scope.giftMessage.Text));
        });


    }]);