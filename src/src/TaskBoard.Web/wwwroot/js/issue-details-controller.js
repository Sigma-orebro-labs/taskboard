angular.module("taskboard").controller("issueDetailsController", function ($scope, $http, issue) {
    $scope.issue = issue;
});
