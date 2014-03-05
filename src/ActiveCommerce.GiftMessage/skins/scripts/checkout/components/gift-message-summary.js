angular.module('checkout.components')

    .controller('GiftMessageSummaryCtrl', ['$scope', 'checkout', function($scope, checkout) {

        // INIT
        $scope.giftMessage = {};

        checkout.get().then(function (state) {
            $scope.giftMessage = state.GiftMessage;
        });

    }]);