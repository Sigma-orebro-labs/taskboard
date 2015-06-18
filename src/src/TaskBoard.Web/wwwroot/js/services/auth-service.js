(function () {
    function authService($rootScope, $window) {

        var onSignInCallbacks = [];

        $window.onSignIn = function (googleUser) {
            console.log("User logged in with Google");

            var profile = googleUser.getBasicProfile();

            console.log('ID: ' + profile.getId());
            console.log('Name: ' + profile.getName());
            console.log('Image URL: ' + profile.getImageUrl());
            console.log('Email: ' + profile.getEmail());

            onSignInCallbacks.forEach(function (c) {
                var callback = c || gb.noop;
                $rootScope.$apply(function () {
                    callback(profile);
                });
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