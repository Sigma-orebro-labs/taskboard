var gb = gb || {};
gb.viewModels = gb.viewModels || {};
gb.viewModels.boardViewModel = gb.viewModels.boardViewModel || {};

(function () {

    function createColumnViewModel(state, issuesInCurrentState) {
        return gb.viewModels.boardColumnViewModel.create(state, issuesInCurrentState);
    }

    function createStateColumns(issues, states) {
        return states.map(function (state) {

            var issuesInCurrentState = issues.filter(function (issue) {
                return issue.stateId == state.id;
            });

            return createColumnViewModel(state, issuesInCurrentState);
        });
    }

    function getColumnForIssue(issue, columns) {
        return _.find(columns, function (c) {
            return c.isMatchForIssue(issue);
        });
    }

    function getColumnForState(state, columns) {
        return _.find(columns, function (c) {
            return c.isMatchForState(state.id);
        });
    }

    gb.viewModels.boardViewModel.create = function (board) {
        
        var columns = createStateColumns(board.issues, board.states);

        function addIssue(issue) {
            var column = getColumnForIssue(issue, columns);

            column.addIssue(issue);
        }

        function addColumn(state) {
            var columnViewModel = createColumnViewModel(state, []);
            columns.push(columnViewModel);
        }

        function removeIssue(issue) {
            var column = getColumnForIssue(issue, columns);

            column.removeIssue(issue);
        }

        function removeIssueById(issueId) {
            var issue = getIssueById(issueId);

            if (issue) {
                removeIssue(issue);
            }
        }

        function getIssueById(issueId) {
            for (var i = 0; i < columns.length; i++) {
                for (var j = 0; j < columns[i].issues.length; j++) {
                    var issue = columns[i].issues[j];

                    if (issue.id == issueId) {
                        return issue;
                    }
                }
            }

            return null;
        }

        function changeState(issueId, stateId) {

            var issue = getIssueById(issueId);
            var currentColumn = getColumnForIssue(issue, columns);
            var newColumn = getColumnForState({ id: stateId }, columns);

            currentColumn.removeIssue(issue);
            newColumn.addIssue(issue);

            issue.stateId = stateId;
        }

        function removeColumnById(stateId) {
            var column = getColumnForState({ id: stateId }, columns);
            removeColumn(column);
        }

        function removeColumn(column) {
            var index = columns.indexOf(column);
            columns.splice(index, 1);
        }

        function addIssue(issue) {
            var column = getColumnForIssue(issue, columns);
            column.addIssue(issue);
        }

        function updateIssue(updatedIssue) {
            var issue = getIssueById(updatedIssue.id);

            issue.title = updatedIssue.title;
            issue.description = updatedIssue.description;
        }

        function getSelectedColumn() {
            return _.find(columns, function (c) {
                return c.isSelected()
            });
        }

        function moveSelectedColumnLeft() {
            var column = getSelectedColumn();
            var index = columns.indexOf(column);

            if (index > 0) {
                columns.splice(index, 1);
                columns.splice(index - 1, 0, column);
            }
        }

        function moveSelectedColumnRight() {
            var column = getSelectedColumn();
            var index = columns.indexOf(column);

            if (index < columns.length - 1) {
                columns.splice(index, 1);
                columns.splice(index + 1, 0, column);
            }
        }

        function canColumnMoveLeft(column) {
            return column.state.order > 0;
        }

        function canColumnMoveRight(column) {
            return column.state.order < columns.length - 1;
        }

        function updateStates(states) {
            states.forEach(function (updatedState) {
                var existingState = getColumnForState({ id: updatedState.id }, columns).state;
                existingState.order = updatedState.order;
                existingState.name = updatedState.name;
            });
        }

        function toggleColumnSelection(column) {
            columns.forEach(function (c) {
                if (c != column) {
                    c.deselect();
                }
            })

            column.toggleSelected();
        }

        return {
            id: board.id,
            columns: columns,
            addColumn: addColumn,
            removeColumn: removeColumn,
            removeColumnById: removeColumnById,
            states: board.states,
            name: board.name,
            columns: columns,
            addIssue: addIssue,
            removeIssue: removeIssue,
            removeIssueById: removeIssueById,
            changeState: changeState,
            addIssue: addIssue,
            updateIssue: updateIssue,
            moveSelectedColumnLeft: moveSelectedColumnLeft,
            moveSelectedColumnRight: moveSelectedColumnRight,
            selectedColumn: getSelectedColumn,
            canColumnMoveLeft: canColumnMoveLeft,
            canColumnMoveRight: canColumnMoveRight,
            updateStates: updateStates,
            toggleColumnSelection: toggleColumnSelection
        };
    };
})();

