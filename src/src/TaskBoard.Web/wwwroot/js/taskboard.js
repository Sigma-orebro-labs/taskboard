var gb = gb || {};

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
        .otherwise({
          redirectTo: '/boards'
      });
}]);