angular.module('taskboard').controller('AlertController', function ($scope, alertService) {
    $scope.alerts = alertService.alerts;

    $scope.closeAlert = function (index) {
        alertService.close(index);
    };
});