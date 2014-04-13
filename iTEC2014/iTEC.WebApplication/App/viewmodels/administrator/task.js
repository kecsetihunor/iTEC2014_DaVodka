define(['services/datacontext', 'managers/navigationManager', 'models/welcome/welcomeMessage'],
    function (datacontext, navigationManager, welcomeMessage) {

        var hash = {
            groups: navigationManager.routes.administrator.groupHome.hash,
            users: navigationManager.routes.administrator.userHome.hash,
            menu: navigationManager.routes.administrator.categoryHome.hash,
            report: navigationManager.routes.administrator.reportHome.hash,
        };

        var entity = ko.observable();

        var vm = {
            activate: activate,
            entity: entity,
            hash: hash,
            submit: submit
        };

        return vm;

        function activate() {
            return datacontext.reporting.getWelcomeMessage()
                .success(function (data) {
                    entity(new welcomeMessage(data));
                });
        }

        function submit() {
            return datacontext.reporting.saveWelcomeMessage(entity().serialize());
        }
    
    }
);