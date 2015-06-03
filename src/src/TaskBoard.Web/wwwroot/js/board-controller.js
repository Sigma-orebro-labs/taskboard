angular.module("gosuboard").controller("boardController", function ($scope, $http, $routeParams) {

    function getIssuesHref() {
        return gb.href("issues", $scope.board);
    }

    function getStatesHref() {
        return gb.href("states", $scope.board);
    }

    $http.get("/api/boards/" + $routeParams.id).success(function (data) {
        $scope.board = data;

        $http.get(getIssuesHref()).success(function (issues) {
            $scope.board.issues = issues.items;
        });

        $http.get(getStatesHref()).success(function (states) {
            $scope.board.states = states.items;
        });
    });

    $scope.createIssue = function () {
        $http(gb.formDataRequest('POST', getIssuesHref(), $scope.issueToCreate)).success(function (data) {
            $scope.board.issues.push(data);
            $scope.issueToCreate = {};
        });
    };

    $scope.createState = function () {
        $http(gb.formDataRequest('POST', getStatesHref(), $scope.stateToCreate)).success(function (data) {
            $scope.board.states.push(data);
            $scope.stateToCreate = {};
        });
    };

    $scope.deleteIssue = function (issue) {
        var href = gb.href("self", issue);

        $http.delete(href).success(function () {
            var index = $scope.board.issues.indexOf(issue);
            $scope.board.issues.splice(index, 1);
        });
    };

    $scope.deleteState = function (state) {
        var href = gb.href("self", state);

        $http.delete(href).success(function () {
            var index = $scope.board.states.indexOf(state);
            $scope.board.states.splice(index, 1);
        });
    };

    $scope.changeState = function (issue, state) {
        issue.stateId = state.id;
    };

    $scope.getStateName = function (stateId) {

        if (!stateId) {
            return "No state";
        }

        var state = _.find($scope.board.states, function (state) {
            return state.id == stateId;
        });

        return state.name;
    };
});
