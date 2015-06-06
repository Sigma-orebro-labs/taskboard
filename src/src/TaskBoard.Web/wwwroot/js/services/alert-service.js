(function () {
    function alertService() {

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
            return alerts.push({
                type: type,
                msg: msg,
                close: function () {
                    close(this);
                },
                isClosable: true
            });
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