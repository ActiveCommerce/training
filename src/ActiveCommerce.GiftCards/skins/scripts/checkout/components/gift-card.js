angular.module('checkout.components')
    
    .controller('GiftCardCtrl', ['$scope', 'checkout', 'message', 'localize', function ($scope, checkout, message, localize) {

        // INIT
        $scope.card = {};
        $scope.applied = [];
        $scope.checkout = {};

        checkout.get().then(function (state) {
            $scope.checkout = state;
        });

        // Enable/disable based on the balance due
        $scope.$watch("checkout.BalanceDue", function (value) {
            $scope.enabled = angular.isUndefined(value) || value > 0;
        });

        $scope.$watchCollection('checkout.Payments', function (payments) {
            $scope.applied = _.where(payments, { Code: $scope.code });
        });

        $scope.applyGiftCard = function () {
            reset();

            if ($scope.gcForm.$invalid) {
                $scope.submitted = true;
                message.alert(localize.text('Validation-All-Fields'), localize.text('Checkout-Validation-Error'));
                return;
            }

            checkout.applyGiftCard($scope.code, $scope.card).then(function () {
                $scope.card = {};
                $scope.message = localize.text('Checkout-Gift-Card-Success');
                checkout.get(true);
            }, function(reason) {
                reason && message.alert(reason, localize.text('Checkout-Gift-Card-Error'));
            });
        };

        $scope.removeGiftCard = function (payment) {
            reset();

            checkout.removeGiftCard(payment.Id).then(function () {
                checkout.get(true);
            }, function (reason) {
                reason && message.alert(reason, localize.text('Checkout-Gift-Card-Error'));
            });
        };

        var reset = function () {
            $scope.submitted = false;
            $scope.message = null;
        };
        
        // EVENTS
        $scope.$on("activated", function (e, args) {
            reset();

            if (!$scope.code || $scope.code === "") {
                $scope.message = "Payment code is missing. Please configure the payment option for the Gift Card payment.";
            }

        });
        
    }]);