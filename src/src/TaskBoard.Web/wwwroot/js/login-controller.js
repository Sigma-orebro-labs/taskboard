angular.module("taskboard").controller("loginController", function ($scope, $location, authService) {

    authService.onSignIn(function () {
        $location.path('/boards');
    });

    renderButton();
});
