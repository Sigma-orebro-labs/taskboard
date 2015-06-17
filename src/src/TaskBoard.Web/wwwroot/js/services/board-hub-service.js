(function () {
    function boardHubService($rootScope) {

        var boardHub = $.connection.boardHub;

        function disconnect() {
            $.connection.hub.stop();
        }

        function connect(callback) {

            callback = callback || function () { };

            if ($.connection.hub && $.connection.hub.state === $.signalR.connectionState.disconnected) {
                return $.connection.hub.start().done(callback);
            }

            callback();
        }

        function subscribe(board, options) {

            console.log("Subscribing for events for board: " + board.id);

            disconnect();

            boardHub.client.addIssue = function (issue) {

                console.log("Adding issue with title: " + issue.title);

                $rootScope.$apply(function () {
                    board.addIssue(issue);
                });
            };

            boardHub.client.deleteIssue = function (issueId) {

                console.log("Deleting issue with id: " + issueId);

                $rootScope.$apply(function () {
                    board.removeIssueById(issueId);
                });
            };

            boardHub.client.addState = function (state) {
                $rootScope.$apply(function () {
                    board.addColumn(state);
                });
            };

            boardHub.client.deleteState = function (stateId) {
                $rootScope.$apply(function () {
                    board.removeColumnById(stateId);
                });
            };

            boardHub.client.updateIssue = function (issue) {
                $rootScope.$apply(function () {
                    board.updateIssue(issue);
                });
            };

            //$.connection.hub.logging = true;
            connect(function () {
                boardHub.server.subscribe(board.id);
            });
        }

        function unsubscribeAll(boardIds) {

            $.connection.hub.stop();

            connect(function () {
                boardHub.server.unsubscribeMany(boardIds);
            });
        }

        function unsubscribe(boardId) {

            console.log("Unsubscribing from events for board: " + boardId);

            $.connection.hub.stop();

            connect(function () {
                boardHub.server.unsubscribe(boardId);
            });
        }

        return {
            subscribe: subscribe,
            unsubscribe: unsubscribe,
            unsubscribeAll: unsubscribeAll
        };
    }

    angular.module('taskboard').factory('boardHubService', boardHubService);
})();