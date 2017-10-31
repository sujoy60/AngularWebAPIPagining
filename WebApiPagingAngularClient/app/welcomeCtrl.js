(function () {
    'use strict';

    angular
        .module('app')
        .controller('welcomeCtrl', welcomeCtrl);
        //.controller('fullCtrl', fullCtrl);
    welcomeCtrl.$inject = ['$scope']; 

    function welcomeCtrl($scope) {

        activate();

        function activate() { }
    }
})();
