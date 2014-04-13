define(['models/entity'],
    function (entity) {

        var model = function (options) {

            options = options || {};

            var self = this;

            entity.call(self, options);

            self.id = ko.observable(options.id).extend({ required: false });
            self.name = ko.observable(options.name).extend({ required: true });
            self.price = ko.observable(options.price).extend({ required: true });

            self.serialize = function () {
                return {
                    id: self.id(),
                    name: self.name(),
                    price: self.price()
                };
            }

        };

        return model;

    }
);