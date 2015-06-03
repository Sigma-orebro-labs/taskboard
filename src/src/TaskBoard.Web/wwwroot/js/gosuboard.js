var gb = gb || {};

var app = angular.module("gosuboard", ["ngRoute", "ngTouch"]);

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