angular.module("gosuboard").controller("boardController", function ($scope, $http, $routeParams, $q) {

    function getIssuesHref() {
        return gb.href("issues", $scope.board);
    }

    function getStatesHref() {
        return gb.href("states", $scope.board);
    }

    $http.get("/api/boards/" + $routeParams.id).success(function (data) {
        $scope.board = data;

        var issuesPromise = $http.get(getIssuesHref()).success(function (issues) {
            $scope.board.issues = issues.items;
        });

        var statesPromise = $http.get(getStatesHref()).success(function (states) {
            $scope.board.states = states.items;
        });

        $q.all([issuesPromise, statesPromise]).then(function () {
            $scope.boardViewModel = gb.viewModels.boardViewModel.create($scope.board.issues, $scope.board.states);
        });
    });

    $scope.createIssue = function () {
        $http(gb.formDataRequest('POST', getIssuesHref(), $scope.issueToCreate)).success(function (data) {
            $scope.board.issues.push(data);
            $scope.boardViewModel.addIssue(data);
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
            $scope.boardViewModel.removeIssue(issue);
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

        var href = gb.href("statetransitions", issue);

        $http(gb.formDataRequest('POST', href, {
            issueId: issue.id,
            stateId: state.id
        })).success(function () {
            issue.stateId = state.id;
        });
    };

    $scope.getStateName = function (stateId) {

        if (!stateId) {
            return "No state";
        }

        var state = _.find($scope.board.states, function (state) {
            return state.id == stateId;
        });

        return state ? state.name : null;
    };
});
