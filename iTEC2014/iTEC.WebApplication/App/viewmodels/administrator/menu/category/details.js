define(['services/datacontext', 'managers/navigationManager', 'models/category/categoryDetails'],
    function (datacontext, navigationManager, categoryDetails) {

        var entity = ko.observable();
        var categoryId = ko.observable();
        var hash = {
            cancel: navigationManager.routes.administrator.categoryHome.hash,
            add: ko.computed(function () {
                var hash = navigationManager.routes.administrator.productDetails.hash;
                if (categoryId()) {
                    hash += '?id=' + categoryId();
                }
                return hash;
            })
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
                load: datacontext.management.getAllProductsCompareToSpecificCategory,
                remove: datacontext.management.deleteUserFromGroup,
                add: datacontext.management.addProductToGroup,
                delete: datacontext.management.removeProductFromGroup,
                convert: function (list) {
                    for (var index in list) {
                        list[index].inGroup = ko.observable(list[index].inGroup);
                        list[index].hasChanged = ko.observable(false);
                        list[index].hash = {
                            details: hash.add().replace('?', '/' + list[index].id + '?')
                        };
                    }
                    return list;
                }
            };
            if (id) {
                categoryId(id);
                return datacontext.management.getCategory({ id: id })
                    .success(function (data) {
                        $.extend(options, data);
                        entity(new categoryDetails(options));
                        entity().products.load({ id: id })
                    });
            } else {
                entity(new categoryDetails());
                return true;
            }
        }

        function submit() {
            var object = entity().serialize();

            var action = datacontext.management.saveCategory(object);
            var entities = getChangedEntities(object.id);
            if (entities.length > 0) {
                action = action.success(function () {
                    return entity().products.chain(entities);
                });
            }
            return action.success(function () {
                return navigationManager.router.navigate(hash.cancel);
            });
        }

        function getChangedEntities(id) {
            var entities = [];
            var list = entity().products.list();
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
                            ? function (model) { return datacontext.management.addProductToCategory(model); }
                            : function (model) { return datacontext.management.removeProductFromCategory(model); }
                    });
                }
            }
            return entities;
        }
    
    }
);