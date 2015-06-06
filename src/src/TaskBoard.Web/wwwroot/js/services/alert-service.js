(function () {
    function alertService($timeout) {

        var alerts = [];
        
        function showError(message) {
            show("danger", message);
        }

        function showWarning(message) {
            show("warning", message);
        }

        function showInfo(message) {
            show("info", message);
        }

        function showSuccess(message) {
            show("success", message);
        }

        function show(type, msg) {

            console.log("showing alert (" + type + "): " + msg);

            var alert = {
                type: type,
                msg: msg,
                close: function () {
                    close(this);
                },
                isClosable: true
            };

            $timeout(function () {
                close(alert);
            }, 10 * 1000);

            return alerts.push(alert);
        }

        function close(alert) {
            var index = alerts.indexOf(alert);
            return alerts.splice(index, 1);
        }

        function clear() {
            alerts.length = 0;
        }

        return {
            show: show,
            showError: showError,
            showWarning: showWarning,
            showInfo: showInfo,
            showSuccess: showSuccess,
            close: close,
            clear: clear,
            alerts: alerts
        };
    }

    angular.module('taskboard').factory('alertService', alertService);
})();