angular.module("taskboard").controller("boardsController", function ($scope, $http) {

    $http.get("/api/boards/").success(function (data) {
        $scope.boards = data.items;
    });

    $scope.createBoard = function () {
        $http(gb.formDataRequest('POST', '/api/boards', $scope.boardToCreate)).success(function (data) {
            $scope.boards.push(data);
            $scope.boardToCreate = {};
        });;
    };

    $scope.deleteBoard = function (board) {
        var href = gb.href("self", board);

        $http.delete(href).success(function () {
            var index = $scope.boards.indexOf(board);
            $scope.boards.splice(index, 1);  
        });
    }
});
