angular.module("gosuboard").controller("boardController", function ($scope, $http, $routeParams) {
    $http.get("/api/boards/" + $routeParams.id).success(function (data) {
        $scope.board = data;

        $http.get(data.issuesLink.href).success(function (issues) {
            $scope.board.issues = issues.items;
        });
    });
});
