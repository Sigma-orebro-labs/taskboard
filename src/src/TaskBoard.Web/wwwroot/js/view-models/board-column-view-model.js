var gb = gb || {};
gb.viewModels = gb.viewModels || {};
gb.viewModels.boardColumnViewModel = gb.viewModels.boardColumnViewModel || {};

gb.viewModels.boardColumnViewModel.create = function (state, issues) {

    var issueToCreate = {};
    var properties = {
        isSelected: false
    };

    function addIssue(issue) {
        issues.push(issue);

        if (issueToCreate.title == issue.title) {
            clearCreateIssueForm();
        }
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

    function toggleSelected() {
        properties.isSelected = !properties.isSelected;
    }

    function isSelected() {
        return properties.isSelected;
    }

    return {
        state: state,
        issueToCreate: issueToCreate,
        getIssueToCreate: getIssueToCreate,
        issues: issues,
        addIssue: addIssue,
        removeIssue: removeIssue,
        isMatchForIssue: isMatchForIssue,
        isMatchForState: isMatchForState,
        isVisible: isVisible,
        name: name,
        toggleSelected: toggleSelected,
        isSelected: isSelected
    };
};
