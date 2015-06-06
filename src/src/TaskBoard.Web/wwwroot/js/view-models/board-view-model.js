var gb = gb || {};
gb.viewModels = gb.viewModels || {};
gb.viewModels.boardViewModel = gb.viewModels.boardViewModel || {};

(function () {

    function addNoStateColumn(issues, columns) {
        var issuesWithoutState = issues.filter(function (issue) {
            return issue.stateId == null || issue.stateId == undefined;
        });

        var noStateColumn = gb.viewModels.boardColumnViewModel.create({
            name: "No state",
            id: null
        }, issuesWithoutState);

        // Insert first (to the left)
        columns.splice(0, 0, noStateColumn);
    }

    function createStateColumns(issues, states) {
        return states.map(function (state) {

            var issuesInCurrentState = issues.filter(function (issue) {
                return issue.stateId == state.id;
            });

            return gb.viewModels.boardColumnViewModel.create(state, issuesInCurrentState);
        });
    }

    function getColumnForIssue(issue, columns) {
        return _.find(columns, function (c) {
            return c.isMatchForIssue(issue);
        });
    }

    function getColumnForState(state, columns) {
        var a = _.find(columns, function (c) {
            return c.isMatchForState(state.id);
        });
        return a;
    }

    gb.viewModels.boardViewModel.create = function (board) {
        
        var columns = createStateColumns(board.issues, board.states);

        addNoStateColumn(board.issues, columns);

        function addIssue(issue) {
            var column = getColumnForIssue(issue, columns);

            column.addIssue(issue);
        }

        function removeIssue(issue) {
            var column = getColumnForIssue(issue, columns);

            column.removeIssue(issue);
        }

        function getVisibleColumns() {
            return columns.filter(function (c) {
                return c.isVisible()
            });
        }

        function changeState(issue, state) {
            var currentColumn = getColumnForIssue(issue, columns);
            var newColumn = getColumnForState(state, columns);

            currentColumn.removeIssue(issue);
            newColumn.addIssue(issue);

            issue.stateId = state.id;
        }

        return {
            columns: columns,
            states: board.states,
            name: board.name,
            getVisibleColumns: getVisibleColumns,
            addIssue: addIssue,
            removeIssue: removeIssue,
            changeState: changeState
        };
    };
})();

