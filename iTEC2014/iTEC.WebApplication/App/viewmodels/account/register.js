define(['managers/navigationManager', 'managers/captchaManager', 'models/account/userRegister'],
    function (navigationManager, captchaManager, userRegister) {

        var entity = ko.observable();

        var hash = {
            login: navigationManager.routes.login.hash
        };

        var vm = {
            activate: activate,
            entity: entity,
            submit: submit,
            hash: hash
        };

        return vm;

        function activate() {
            entity(new userRegister({ create: create }));
            
            return entity().captcha.refresh();
        }

        function create(size) {
            return captchaManager.create(size);
        }

        function submit() {
            return navigationManager.register(entity().serialize())
                .fail(function () {
                    entity().captcha.refresh();
                });
        }
    }
);