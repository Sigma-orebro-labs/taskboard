(function () {
    function keyBindingService($rootScope) {

        var handlers = [];

        function initialize() {
            document.addEventListener("keydown", dispatch, false);
        }

        function dispatch(e) {
            var keyCode = e.key || e.which;

            var matchingHandlers = handlers.filter(function (h) {
                return h.keyCode == keyCode &&
                    h.alt == e.altKey &&
                    h.shift == e.shiftKey &&
                    h.ctrl == e.ctrlKey;
            });

            matchingHandlers.forEach(function (h) {
                $rootScope.$apply(h.callback);
            });
        }

        function register(callback, keyCode, modifiers) {
            modifiers = modifiers || { };

            handlers.push({
                callback: callback,
                keyCode: keyCode,
                shift: !!modifiers.shift,
                alt: !!modifiers.alt,
                ctrl: !!modifiers.ctrl
            });
        }

        function clear() {
            document.removeEventListener("keypress", dispatch, false);
            handlers.length = 0;
        }

        var keys = {
            enter: 13,
            left: 37,
            right: 39,
            up: 38,
            down: 40
        };

        return {
            initialize: initialize,
            clear: clear,
            register: register,
            keys: keys
        };
    }

    angular.module('taskboard').factory('keyBindingService', keyBindingService);
})();