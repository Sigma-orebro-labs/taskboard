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

    function getMatchingColumn(issue, columns) {
        return _.find(columns, function (c) {
            return c.isMatch(issue);
        });
    }

    gb.viewModels.boardViewModel.create = function (issues, states) {
        
        var columns = createStateColumns(issues, states);

        addNoStateColumn(issues, columns);

        function addIssue(issue) {
            var column = getMatchingColumn(issue, columns);

            column.addIssue(issue);
        }

        function removeIssue(issue) {
            var column = getMatchingColumn(issue, columns);

            column.removeIssue(issue);
        }

        function getVisibleColumns() {
            return columns.filter(function (c) {
                return c.isVisible()
            });
        }

        return {
            columns: columns,
            getVisibleColumns: getVisibleColumns,
            addIssue: addIssue,
            removeIssue: removeIssue
        };
    };
})();

