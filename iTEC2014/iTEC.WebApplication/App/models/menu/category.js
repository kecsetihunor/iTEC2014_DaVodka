define(['models/controls/infinitescroll', 'models/menu/product'],
    function (infinitescroll, product) {

        var model = function (options) {

            options = options || {};

            var self = this;

            var load = options.load;

            options.load = function () {
                return load({ id: self.serialize().id });
            };

            self.id = ko.observable(options.id).extend({ required: true });
            self.name = ko.observable(options.name).extend({ required: true });
            self.count = ko.observable(options.count);
            self.products = ko.observableArray([]);
            self.selected = ko.observable(false);
            self.ready = ko.observable(true);

            self.select = function () {
                self.selected(true);
                self.ready(false);
                options.load().success(function (data) {
                    data = convert(data.result);
                    self.products(data);
                    self.ready(true);
                });
            };

            self.unselect = function () {
                self.selected(false);
            };

            self.serialize = function () {
                return {
                    id: self.id(),
                    name: self.name()
                };
            };

            function convert(products) {
                for (var index in products) {
                    products[index] = new product(products[index]);
                }
                return products;
            }

        };

        return model;

    }
);