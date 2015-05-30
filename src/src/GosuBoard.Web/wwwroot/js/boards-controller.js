angular.module("gosuboard").controller("boardsController", function ($scope, $http) {
    $http.get("/api/boards/").success(function (data) {
        $scope.boards = data.items;
    });
});
