(function () {
    function issueDetailsService($modal) {

        function show(issue) {

            return $modal.open({
                templateUrl: 'templates/issue-details.html',
                controller: "issueDetailsController",
                resolve: {
                    issue: function () {
                        return issue;
                    }
                }
            });
        }

        return {
            show: show,
        };
    }

    angular.module('taskboard').factory('issueDetailsService', issueDetailsService);
})();