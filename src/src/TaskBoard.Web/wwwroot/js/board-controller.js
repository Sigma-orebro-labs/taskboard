angular.module("taskboard").controller("boardController", function (
    $scope,
    $http,
    $routeParams,
    $q,
    $modal,
    alertService,
    promptService,
    issueDetailsService,
    boardHubService,
    keyBindingService) {
    
    var data = {};

    function getIssuesHref(board) {
        return gb.href("issues", data.board);
    }

    function getStatesHref() {
        return gb.href("states", data.board);
    }

    function initialize(board) {
        $scope.board = gb.viewModels.boardViewModel.create(board);

        boardHubService.subscribe($scope.board);

        initializeKeyBindings();
    }

    function initializeKeyBindings(board) {
        var keys = keyBindingService.keys;

        keyBindingService.initialize();

        keyBindingService.register(
            $scope.board.moveSelectedColumnLeft,
            keys.left,
            {
                shift: true,
                alt: true
            });

        keyBindingService.register(
            $scope.board.moveSelectedColumnRight,
            keys.right,
            {
                shift: true,
                alt: true
            });
    }

    function cleanUp() {
        boardHubService.unsubscribe($scope.board.id);
        keyBindingService.clear();
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
            initialize(board);
        });
    });

    $scope.createIssue = function (column) {

        var issueToCreate = column.getIssueToCreate();
        
        $http(gb.formDataRequest('POST', getIssuesHref(), issueToCreate)).success(function (data) { });
    };

    $scope.createState = function () {
        $http(gb.formDataRequest('POST', getStatesHref(), $scope.stateToCreate)).success(function (state) {
            $scope.stateToCreate = {};
        });
    };

    $scope.deleteIssue = function (column, issue) {
        var href = gb.href("self", issue);
        
        promptService.showDanger("Delete issue?", "Do you really want to delete the issue?")
            .then(function () {
                $http.delete(href).success(function () { });
            });
    };

    $scope.deleteColumn = function (column) {

        if (column.issues.length > 0) {
            alertService.showError("The state cannot be deleted since there are issues currently in that state.");
            return;
        }

        promptService.showDanger("Delete state?", "Do you really want to delete the state?")
            .then(function () {
                var href = gb.href("self", column.state);

                $http.delete(href).success(function () { });
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

    $scope.showIssueDetails = function (issue) {
        issueDetailsService.show(issue).then(function () {
            alertService.showSuccess("Changes made to the issue have been saved")
        });
    };

    $scope.$on("$destroy", function () {
        cleanUp();
    });
});
