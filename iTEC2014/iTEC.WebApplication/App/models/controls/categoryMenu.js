define(['models/controls/search', 'models/menu/category'],
    function (search, category) {

        var model = function (options) {

            options = options || {};

            var self = this;

            var load = {
                category: options.load.category,
                product: options.load.product
            };

            self.saved = ko.observableArray([]);
            self.saved.done = ko.observable(false);
            self.categories = new search({
                load: function () {
                    return load.category(self.serialize());
                },
                convert: convert
            });

            self.categories.select = function (category) {
                if (!category.selected()) {
                    self.saved(self.categories.value());
                    self.saved.done(self.categories.value.infinite.done());
                    self.categories.value([category]);
                    self.categories.value.infinite.done(true);
                    category.select();
                }
            };

            self.categories.unselect = function (category) {
                if (category.selected()) {
                    category.unselect();
                    self.categories.value(self.saved());
                    self.categories.value.infinite.done(self.saved.done());
                }
            };

            self.serialize = function () {
                return self.categories.serialize();
            }

            function convert(categories) {
                for (var index in categories) {
                    var options = $.extend(categories[index], { load: load.product });
                    categories[index] = new category(options);
                }
                return categories;
            }

        };

        return model;

    }
);