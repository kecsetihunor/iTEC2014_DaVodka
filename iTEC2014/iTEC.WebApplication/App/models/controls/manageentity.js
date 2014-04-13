define(['durandal/app', 'services/datacontext', 'managers/navigationManager', 'models/user/user'],
    function (app, datacontext, navigationManager, category) {

        var model = function (options) {

            options = options || {};

            var self = this;

            var load = options.load;
            var remove = options.remove;
            var convert = options.convert || function (list) { return list; };

            self.hash = {
                add: options.add
            };
            self.list = ko.observableArray([]);
            self.isReady = ko.observable(true);
            self.isEmpty = ko.computed(function () {
                return (self.list() == undefined || self.list().length == 0) && self.isReady();
            });

            self.load = function (model) {
                self.isReady(false);
                return load(model)
                    .success(function (list) {
                        self.list(convert(list));
                    })
                    .always(function () {
                        self.isReady(true);
                    });
            };

            self.remove = function (entity) {
                //return app.showMessage('Are you sure you want to remove it?', '<i class="fa fa-fa fa-lg fa-warning"></i> Warning!', ['Yes, I want to remove it', 'No']);
                return remove({ id: entity.id }).success(function () {
                    self.list.remove(entity);
                });
            };

        };

        return model;

    }
);