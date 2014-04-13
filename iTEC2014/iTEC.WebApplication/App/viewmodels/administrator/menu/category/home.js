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
                load: datacontext.management.getCategories,
                remove: datacontext.management.deleteCategory,
                add: navigationManager.routes.administrator.categoryDetails.hash,
                convert: function (list) {
                    for (var index in list) {
                        list[index].hash = {
                            details: navigationManager.routes.administrator.categoryDetails.hash + '/' + list[index].id
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