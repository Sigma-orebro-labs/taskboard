angular.module("taskboard").controller("issueDetailsController", function ($scope, $http, issue) {

    $scope.issue = angular.copy(issue);

    $scope.save = function () {

        $http(gb.formDataRequest('PUT', gb.href("self", issue), $scope.issue)).success(function (data) {

            issue.title = data.title;
            issue.description = data.description;

            $scope.$close(issue);
        });
    };

    $scope.cancel = function () {
        $scope.$dismiss();
    };
});
