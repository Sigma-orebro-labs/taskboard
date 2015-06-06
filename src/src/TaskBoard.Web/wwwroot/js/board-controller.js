angular.module("gosuboard").controller("boardController", function ($scope, $http, $routeParams, $q) {

    var data = {};

    function getIssuesHref(board) {
        return gb.href("issues", data.board);
    }

    function getStatesHref() {
        return gb.href("states", data.board);
    }

    $http.get("/api/boards/" + $routeParams.id).success(function (board) {

        data.board = board;

        var issuesPromise = $http.get(getIssuesHref()).success(function (issues) {
            board.issues = issues.items;
        });

        var statesPromise = $http.get(getStatesHref()).success(function (states) {
            board.states = states.items;
        });

        $q.all([issuesPromise, statesPromise]).then(function () {
            $scope.board = gb.viewModels.boardViewModel.create(board);
        });
    });

    $scope.createIssue = function (column) {

        var issueToCreate = column.getIssueToCreate();
        
        $http(gb.formDataRequest('POST', getIssuesHref(), issueToCreate)).success(function (data) {

            column.addIssue(data);
            column.clearCreateIssueForm();
        });
    };

    $scope.createState = function () {
        $http(gb.formDataRequest('POST', getStatesHref(), $scope.stateToCreate)).success(function (data) {
            $scope.board.states.push(data);
            $scope.stateToCreate = {};
        });
    };

    $scope.deleteIssue = function (column, issue) {
        var href = gb.href("self", issue);

        $http.delete(href).success(function () {
            column.removeIssue(issue);
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
            $scope.board.changeState(issue, state);
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
