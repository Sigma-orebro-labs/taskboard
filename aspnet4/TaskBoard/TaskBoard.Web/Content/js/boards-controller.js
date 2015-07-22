angular.module("taskboard").controller("boardsController", function ($scope, $http, promptService, boardHubService) {

    $http.get("/api/boards/").success(function (data) {
        $scope.boards = data.items;

        var boardIds = data.items.map(function (board) {
            return board.id;
        });

        boardHubService.unsubscribeAll(boardIds);
    });

    $scope.createBoard = function () {
        $http.post('/api/boards', $scope.boardToCreate).success(function (data) {
            $scope.boards.push(data);
            $scope.boardToCreate = {};
        });;
    };

    $scope.deleteBoard = function (board) {

        promptService.showDanger("Delete board?", "Do you really want to delete the board and all its issues?")
            .then(function () {
                var href = gb.href("self", board);

                $http.delete(href).success(function () {
                    var index = $scope.boards.indexOf(board);
                    $scope.boards.splice(index, 1);
                });
            });
    }
});
