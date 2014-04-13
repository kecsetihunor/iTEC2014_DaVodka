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
                load: datacontext.management.getGroups,
                remove: datacontext.management.deleteGroup,
                add: navigationManager.routes.administrator.groupDetails.hash,
                convert: function (list) {
                    for (var index in list) {
                        list[index].hash = {
                            details: navigationManager.routes.administrator.groupDetails.hash + '/' + list[index].id
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