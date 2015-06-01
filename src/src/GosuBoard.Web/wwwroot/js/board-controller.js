angular.module("gosuboard").controller("boardController", function ($scope, $http, $routeParams) {
    $http.get("/api/boards/" + $routeParams.id).success(function (data) {
        $scope.board = data;

        var issuesHref = gb.href("issues", data);

        $http.get(issuesHref).success(function (issues) {
            $scope.board.issues = issues.items;
        });

        $scope.createIssue = function () {
            $http(gb.formDataRequest('POST', issuesHref, $scope.issueToCreate)).success(function (data) {
                $scope.board.issues.push(data);
                $scope.issueToCreate = {};
            });
        };

        $scope.deleteIssue = function (issue) {
            var href = gb.href("self", issue);

            $http.delete(href).success(function () {
                var index = $scope.board.issues.indexOf(issue);
                $scope.board.issues.splice(index, 1);
            });
        }
    });
});
