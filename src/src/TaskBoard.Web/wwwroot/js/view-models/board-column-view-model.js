var gb = gb || {};
gb.viewModels = gb.viewModels || {};
gb.viewModels.boardColumnViewModel = gb.viewModels.boardColumnViewModel || {};

gb.viewModels.boardColumnViewModel.create = function (state, issues) {

    function addIssue(issue) {
        issues.push(issue);
    }

    function removeIssue(issue) {
        var index = issues.indexOf(issue);
        issues.splice(index, 1);
    }

    function isMatch(issue) {
        return state.id == issues.stateId || !state.id && !issue.stateId
    }

    function isVisible() {
        return state.id || issues.length > 0;
    }

    function name() {
        return state.name;
    }

    return {
        issues: issues,
        addIssue: addIssue,
        removeIssue: removeIssue,
        isMatch: isMatch,
        isVisible: isVisible,
        name: name
    };
};
