angular.module("taskboard").controller("accountController", function ($scope, $location, authService) {

    authService.onSignIn(function () {
    });

    $scope.signOut = function () {
        authService.signOut(function () {
            $location.path('/login');
        });
    }

    $scope.isSignedIn = authService.isSignedIn;
});
