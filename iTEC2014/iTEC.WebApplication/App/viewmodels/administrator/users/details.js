define(['services/datacontext', 'managers/navigationManager', 'models/user/userDetails'],
    function (datacontext, navigationManager, userDetails) {

        var entity = ko.observable();
        var hash = {
            cancel: navigationManager.routes.administrator.userHome.hash
        };

        var vm = {
            activate: activate,
            entity: entity,
            submit: submit,
            hash: hash
        };

        return vm;

        function activate(id) {
            if (id) {
                return datacontext.management.getUser({ id: id })
                    .success(function (data) {
                        entity(new userDetails(data));
                    })
                    .fail(function () {
                        entity(new userDetails());
                    });
            } else {
                entity(new userDetails());
                return true;
            }
        }

        function submit() {
            return datacontext.management.saveUser(entity().serialize())
                .success(function () {
                    return navigationManager.router.navigate(navigationManager.routes.administrator.userHome.hash);
                });
        }
    
    }
);