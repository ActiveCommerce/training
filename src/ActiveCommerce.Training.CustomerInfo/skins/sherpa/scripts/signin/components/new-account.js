
angular.module('signin.components')

    //reference our new 'birthday' service here
	.controller('TrainingNewAccountCtrl', ['$scope', 'redirector', 'account', 'birthday', '$controller', '$rootScope', function ($scope, redirector, account, birthday, $controller, $rootScope) {

	    // Inherit from base controller, injecting $scope (see Active Commerce Developer's Cookbook)
	    $controller('NewAccountCtrl', { $scope: $scope });

	    $scope.newAccount = function () {
	        //NOTE: most of this function copied from original
	        $scope.submitted = true;
	        $scope.errorMessage = '';

	        account.newAccount($scope.login.email, $scope.login.password, $scope.login.passwordConfirm).then(function (result) {
	            //ADDED: call birthday service to update birthday value in profile
	            birthday.updateBirthday($scope.login.birthday);
	            
	            redirector.load($rootScope.nextStep);
	        }, function (reason) {
	            $scope.errorMessage = reason.data.Message;
	        });
	    };
	}]);