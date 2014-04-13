define(['services/datacontext', 'managers/navigationManager', 'models/group/groupDetails'],
    function (datacontext, navigationManager, groupDetails) {

        var entity = ko.observable();
        var hash = {
            cancel: navigationManager.routes.administrator.groupHome.hash
        };

        var vm = {
            activate: activate,
            entity: entity,
            submit: submit,
            hash: hash
        };

        return vm;

        function activate(id) {
            var options = {
                load: datacontext.management.getAllUsersCompareToSpecificGroup,
                remove: datacontext.management.deleteUserFromGroup,
                add: datacontext.management.addUserToGroup,
                delete: datacontext.management.removeUserFromGroup,
                convert: function (list) {
                    for (var index in list) {
                        list[index].inGroup = ko.observable(list[index].inGroup);
                        list[index].hasChanged = ko.observable(false);
                    }
                    return list;
                }
            };
            if (id) {
                return datacontext.management.getGroup({ id: id })
                    .success(function (data) {
                        $.extend(options, data);
                        entity(new groupDetails(options));
                        entity().users.load({ id: id });
                    });
            } else {
                entity(new groupDetails(options));
                return true;
            }
        }

        function submit() {
            var object = entity().serialize();

            var action = datacontext.management.saveGroup(object);
            var entities = getChangedEntities(object.id);
            if (entities.length > 0) {
                action = action.success(function () {
                    return entity().users.chain(entities);
                });
            }
            return action.success(function () {
                return navigationManager.router.navigate(navigationManager.routes.administrator.groupHome.hash);
            });
        }

        function getChangedEntities(id) {
            var entities = [];
            var list = entity().users.list();
            for (var index in list) {
                if (list[index].hasChanged()) {
                    var isInGroup = list[index].inGroup();
                    var model = (isInGroup)
                        ? { groupId: id, entityId: list[index].id }
                        : { id: list[index].id }
                    entities.push({
                        id: list[index].id,
                        model: model,
                        callback: (isInGroup)
                            ? function (model) { return datacontext.management.addUserToGroup(model); }
                            : function (model) { return datacontext.management.removeUserFromGroup(model); }
                    });
                }
            }
            return entities;
        }

    }
);