(function () {
    'use strict';

    angular
        .module('app')
        .controller('simpleCtrl', simpleCtrl);

    simpleCtrl.$inject = ['$scope', 'simpleFeedbackSvc'];

    function simpleCtrl($scope, simpleFeedbackSvc) {
        $scope.title = 'simple paging';
        $scope.description = '';

        $scope.clubs = simpleFeedbackSvc.clubs;
        $scope.loadClubs = loadClubs;

        $scope.clear = simpleFeedbackSvc.clear;
        
        $scope.options = simpleFeedbackSvc.paging.options;
        $scope.info = simpleFeedbackSvc.paging.info;

        $scope.status = {
            type: "info",
            message: "ready",
            busy: false
        };        
        
        activate();

        $scope.displayVerbatim = function (c) {

            $scope.description = c.verbatim;

        };

        function activate() {
            //if this is the first activation of the controller load the first page
            if (simpleFeedbackSvc.paging.info.currentPage === 0) {
                simpleFeedbackSvc.load();
            }
        }
        //function displayVerbatim() { alert("reached Sim"); }
        function loadClubs() {
            $scope.status.busy = true;
            $scope.status.message = "loading records";

            simpleFeedbackSvc.load()
                            .then(function (result) {
                                $scope.status.message = "ready";
                            }, function (result) {
                                $scope.status.message = "something went wrong: " + (result.error || result.statusText);
                            })
                            ['finally'](function () {
                                $scope.status.busy = false;
                            });
        }

        function optionsChanged() {
            simpleFeedbackSvc.clear();            
        }
    }
})();
