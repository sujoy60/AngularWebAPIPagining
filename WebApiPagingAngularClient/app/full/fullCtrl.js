(function () {
    'use strict';

    angular
        .module('app')
        .controller('fullCtrl', fullCtrl);

    fullCtrl.$inject = ['$scope', 'fullFeedbackSvc'];

    function fullCtrl($scope, fullFeedbackSvc) {
        $scope.title = 'full paging';
        $scope.description = '';

        $scope.pages = fullFeedbackSvc.pages;
        $scope.info = fullFeedbackSvc.paging.info;
        $scope.options = fullFeedbackSvc.paging.options;
        
        $scope.navigate = navigate;        
        $scope.clear = optionsChanged;

        $scope.status = {
            type: "info",
            message: "ready",
            busy: false 
        };
        $scope.displayVerbatim = function (c) {
            
            $scope.description = c.verbatim;
            
        };

        activate();

        function activate() {
            //if this is the first activation of the controller load the first page
            if (fullFeedbackSvc.paging.info.currentPage === 0) {
                navigate(1);
            }
        }

        //function displayVerbatim() { alert("Full Contr");}

        function navigate(pageNumber) {
            $scope.status.busy = true;
            $scope.status.message = "loading records";            

            fullFeedbackSvc.navigate(pageNumber)
                            .then(function () {
                                $scope.status.message = "ready";
                            }, function (result) {
                                $scope.status.message = "something went wrong: " + (result.error || result.statusText);
                            })
                            ['finally'](function () {
                                $scope.status.busy = false;
                            });
        }

        function optionsChanged() {
            fullFeedbackSvc.clear();
            activate();
        }
    }
})();