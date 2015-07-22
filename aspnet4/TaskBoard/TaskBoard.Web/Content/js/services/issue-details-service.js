(function () {
    function issueDetailsService($modal) {

        function show(issue) {
            return $modal.open({
                templateUrl: 'Content/templates/issue-details.html',
                controller: "issueDetailsController",
                resolve: {
                    issue: function () {
                        return issue;
                    }
                }
            }).result;
        }

        return {
            show: show,
        };
    }

    angular.module('taskboard').factory('issueDetailsService', issueDetailsService);
})();