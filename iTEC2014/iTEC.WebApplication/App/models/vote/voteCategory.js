define(['models/entity', 'models/controls/hiddenfield', 'models/controls/textfield', 'models/controls/manageentity'],
    function (entity, hiddenfield, textfield, manageentity) {

        var model = function (options) {

            options = options || {};

            var self = this;

            entity.call(self, options);

            var loadPoints = options.loadPoints;

            var vote = options.product.vote;

            self.points = ko.observable(options.points).extend({ notify: 'always' });
            self.categories = new manageentity({
                load: options.category.load,
                convert: options.category.convert
            });
            self.products = new manageentity({
                load: options.product.load,
                convert: options.product.convert
            });
            self.selected = ko.observable(false);
            self.selectedProduct = ko.observable();

            self.selectCategory = function (category) {
                self.selected(true);
                return self.products.load({ id: category.id });
            };

            self.selectProduct = function (product) {
                if (self.selectedProduct()) {
                    self.selectedProduct().selected(false);
                }
                self.selectedProduct(product);
                if (!self.disabled()) {
                    product.selected(true);
                }
            };

            self.back = function () {
                self.selected(false);
            };

            self.limit = ko.computed(function () {
                var limit = 0;
                var product = self.selectedProduct();
                if (product) {
                    limit = product.points() + self.points();
                }
                return limit;
            });

            self.disabled = ko.computed(function () {
                return self.limit() <= 0;
            });

            self.voteProduct = function (product) {
                return vote({ productId: product.id, points: product.votePoints() })
                    .success(function (data) {
                        self.points(data);
                        product.points(product.votePoints());
                        product.selected(false);
                    });
            };

            self.loadPoints = function () {
                return loadPoints()
                    .success(function (data) {
                        self.points(data);
                    })
                    .fail(function () {
                        self.points(0);
                    });
            }

            self.serialize = function () {
                return {
                    id: self.id.value(),
                    name: self.name.value()
                };
            }

        };

        return model;

    }
);