define(['services/datacontext', 'managers/navigationManager', 'models/controls/manageentity'],
    function (datacontext, navigationManager, manageentity) {

        var entity = ko.observable();
        var hash = {
            task: navigationManager.routes.administrator.task.hash
        };

        var vm = {
            activate: activate,
            entity: entity,
            hash: hash
        };

        return vm;

        function activate() {
            entity(new manageentity({
                load: datacontext.management.getUsers,
                remove: datacontext.management.deleteUser,
                add: navigationManager.routes.administrator.userDetails.hash,
                convert: function (list) {
                    for (var index in list) {
                        list[index].hash = {
                            details: navigationManager.routes.administrator.userDetails.hash + '/' + list[index].id
                        };
                    }
                    return list;
                }
            }));
            entity().load();
            return true;
        }
    
    }
);