(function () {
    function boardHubService($rootScope) {

        var boardHub = $.connection.boardHub;

        function attachBoard(boardViewModel, options) {
            boardHub.client.addIssue = function (issue) {
                console.log("Adding issue with title: " + issue.title);

                if (options && options.onAddIssue) {
                    options.onAddIssue(issue);
                }

                $rootScope.$apply();
            };

            //$.connection.hub.logging = true;
            $.connection.hub.start();
        }

        return {
            attachBoard: attachBoard
        };
    }

    angular.module('taskboard').factory('boardHubService', boardHubService);
})();