angular.module("gosuboard").controller("boardsController", function ($scope, $http) {
    $http.get("/api/boards/").success(function (data) {
        $scope.boards = data.items;
    });

    $scope.createBoard = function () {
        $http({
            method: 'POST',
            url: "/api/boards",
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            transformRequest: function (obj) {
                var str = [];
                for (var p in obj)
                    str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                return str.join("&");
            },
            data: $scope.boardToCreate
        }).success(function (data) {
            $scope.boards.push(data);
        });
    };

    $scope.deleteBoard = function (board) {
        var selfLink = _.find(board.links, function (x) { return x.rel === "self"; });
        $http.delete(selfLink.href).success(function () {
            var index = $scope.boards.indexOf(board);
            $scope.boards.splice(index, 1);  
        });
    }
});
