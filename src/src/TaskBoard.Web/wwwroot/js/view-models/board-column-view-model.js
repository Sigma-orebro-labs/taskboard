var gb = gb || {};
gb.viewModels = gb.viewModels || {};
gb.viewModels.boardColumnViewModel = gb.viewModels.boardColumnViewModel || {};

gb.viewModels.boardColumnViewModel.create = function (state, issues) {

    var issueToCreate = {};

    function addIssue(issue) {
        issues.push(issue);
    }

    function getIssueToCreate() {
        return {
            title: issueToCreate.title,
            stateId: state.id
        };
    }

    function clearCreateIssueForm() {
        issueToCreate.title = null;
    }

    function removeIssue(issue) {
        var index = issues.indexOf(issue);
        issues.splice(index, 1);
    }

    function isMatchForIssue(issue) {
        return isMatchForState(issue.stateId);
    }

    function isMatchForState(stateId) {
        return state.id == stateId || !state.id && !stateId
    }

    function isVisible() {
        return state.id || issues.length > 0;
    }

    function name() {
        return state.name;
    }

    return {
        issueToCreate: issueToCreate,
        getIssueToCreate: getIssueToCreate,
        issues: issues,
        addIssue: addIssue,
        removeIssue: removeIssue,
        isMatchForIssue: isMatchForIssue,
        isMatchForState: isMatchForState,
        isVisible: isVisible,
        name: name,
        clearCreateIssueForm: clearCreateIssueForm
    };
};
