var gb = gb || {};

gb.noop = function () { };

var app = angular.module("taskboard", ["ngRoute", "ngTouch", "ui.bootstrap", "xeditable"]);

app.run(function (editableOptions) {
    editableOptions.theme = 'bs3'; // bootstrap3 theme. Can be also 'bs2', 'default'
});

app.config(['$routeProvider', function ($routeProvider) {

    $routeProvider
        .when('/boards/', {
            templateUrl: 'boards.html',
            controller: 'boardsController'
        })
        .when('/boards/:id', {
            templateUrl: 'board.html',
            controller: 'boardController'
        })
        .when('/login/', {
            templateUrl: 'login.html',
            controller: 'loginController'
        })
        .otherwise({
          redirectTo: '/boards'
      });
}]);

function renderButton() {
    gapi.signin2.render('login-container', {
        'width': 200,
        'height': 50,
        'longtitle': true,
        'theme': 'dark',
        'onsuccess': onSignIn
    });
}