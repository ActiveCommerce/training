angular.module('checkout.components')


    .controller('InvoicePaymentCtrl', ['$scope', 'localize', 'checkout', function($scope, localize, checkout) {

        //init our model
        $scope.purchaseOrder = '';

        // EVENTS
        
        //$scope.$on("activated", function (e, args) {
        //    
        //});

        $scope.$on("validate", function (e, args) {

            // Only if we're the selected payment
            if (!$scope.selected) return;

            if ($scope.purchaseOrderForm.$invalid) {
                args.reject(localize.text('Validation-All-Fields'));
                return;
            }
            
            //could call services here for server-side validation as well
        });

        $scope.$on("process", function (e, args) {
            
            // Only if we're the selected payment
            if (!$scope.selected) return;

            //pass the update operation back to the event args. must return a promise.
            args.defer(checkout.updatePurchaseOrderNumber($scope.purchaseOrder.Number));
        });


    }]);