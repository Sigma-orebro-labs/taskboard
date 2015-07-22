(function () {
    function promptService($modal) {

        function showDanger(title, prompt) {
            return show(title, prompt, "danger");
        }

        function showWarning(title, prompt) {
            return show(title, prompt, "warning");
        }

        function show(title, prompt, type) {

            type = type || "primary";

            return $modal.open({
                templateUrl: 'Content/templates/prompt.html',
                controller: function () {
                    this.title = title;
                    this.prompt = prompt;
                    this.confirmButtonClass = "btn-" + type;
                },
                controllerAs: "model"
            }).result;
        }

        return {
            show: show,
            showDanger: showDanger
        };
    }

    angular.module('taskboard').factory('promptService', promptService);
})();