define(['managers/navigationManager', 'models/account/userLogin'],
    function (navigationManager, userLogin) {

        var entity = ko.observable();

        var hash = {
            register: navigationManager.routes.register.hash
        };

        var vm = {
            activate: activate,
            entity: entity,
            submit: submit,
            hash: hash,
            clear: clear
        };

        return vm;

        function activate() {
            entity(new userLogin({
                username: navigationManager.username(),
                avatar: navigationManager.avatar(),
                remember: navigationManager.isAvailable(),
                isAvailable: navigationManager.isAvailable()
            }));

            return true;
        }

        function clear() {
            entity().clear();
            navigationManager.clearUser();
        }

        function submit() {
            return navigationManager.authenticate(entity().serialize());
        }
    }
);