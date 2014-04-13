define(['managers/navigationManager'],
    function (navigationManager) {

        var hash = {
            search: ko.observable(navigationManager.routes.user.search.hash),
            place: ko.observable(navigationManager.routes.owner.place.hash),
            login: ko.observable(navigationManager.routes.login.hash),
            join: ko.observable(navigationManager.routes.register.hash),
            register: ko.observable(navigationManager.routes.register.hash + '/owner')
        };

        var vm = {
            activate: activate,
            navigationManager: navigationManager,
            hash: hash
        };

        return vm;

        function activate() {
            return true;
        }

    }
);