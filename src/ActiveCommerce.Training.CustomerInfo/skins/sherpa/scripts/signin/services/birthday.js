angular.module('signin.services')

    //new AngularJS service within existing signin.services module
    .factory('birthday', ['$http', '$q', function ($http, $q) {
        
        var birthdayService = {
            updateBirthday: function (birthday, config) {
                //call added controller action to save birthday value
                return $http.post('/training/birthday/SaveBirthday', { birthday: birthday }, config);
            }
        };
        
        return birthdayService;
    }]);