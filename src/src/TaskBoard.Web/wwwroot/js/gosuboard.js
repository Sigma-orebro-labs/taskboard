var gb = gb || {};

var app = angular.module("taskboard", ["ngRoute", "ngTouch", "ui.bootstrap"]);

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
        .otherwise({
          redirectTo: '/boards'
      });
}]);