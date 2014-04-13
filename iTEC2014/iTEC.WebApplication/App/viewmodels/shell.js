define(['managers/navigationManager'],
    function (navigationManager) {

        var shell = {
            activate: activate,
            navigationManager: navigationManager
        };

        return shell;

        function activate() {
            return boot();
        }

        function boot() {
            return navigationManager.state()
                .success(function () {
                    return navigationManager.buildRoutes().activate();
                });
        }
    });