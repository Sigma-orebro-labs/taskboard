(function () {
    function authService($rootScope, $window) {

        var onSignInCallbacks = [];

        $window.onSignIn = function (googleUser) {
            console.log("User logged in with Google");

            onSignInCallbacks.forEach(function (c) {
                $rootScope.$apply(c || gb.noop);
            });
        }

        function onSignIn(callback) {
            onSignInCallbacks.push(callback);
        }

        function signOut(callback) {
            gapi.auth2.getAuthInstance().signOut().then(function () {
                console.log("User logged out from Google");
                $rootScope.$apply(callback || gb.noop);
            })
        }

        function isSignedIn() {
            return gapi && gapi.auth2 && gapi.auth2.getAuthInstance().isSignedIn.get();
        }

        return {
            isSignedIn: isSignedIn,
            onSignIn: onSignIn,
            signOut: signOut
        };
    }

    angular.module('taskboard').factory('authService', authService);
})();