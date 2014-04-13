define(['services/datacontext', 'managers/navigationManager', 'models/product/productDetails'],
    function (datacontext, navigationManager, productDetails) {

        var entity = ko.observable();
        var categoryId = ko.observable();
        var hash = {
            cancel: ko.computed(function () {
                var hash = navigationManager.routes.administrator.categoryDetails.hash;
                if (categoryId()) {
                    hash += '/' + categoryId();
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

        function activate(id, context) {
            if (context && context.id) {
                categoryId(context.id);
            }
            if (id) {
                return datacontext.management.getProduct({ id: id })
                    .success(function (data) {
                        entity(new productDetails(data));
                    });
            } else {
                entity(new productDetails());
                return true;
            }
        }

        function submit() {
            return datacontext.management.saveProduct(entity().serialize())
                .success(function () {
                    return navigationManager.router.navigate(hash.cancel());
                });
        }

    }
);