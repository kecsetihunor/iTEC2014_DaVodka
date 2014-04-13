define(['models/controls/control'],
    function (control) {

        var model = function (options) {

            options = options || {};

            var self = this;

            control.call(self, options);

            var menuEntry = function (options) {

                options = options || {};

                var self = this;

                self.id = ko.observable(options.id);
                self.name = ko.observable(options.name);
                self.price = ko.observable(options.price);

            };

            var menuCategory = function (options) {

                options = options || {};

                var self = this;

                self.id = ko.observable(options.id);
                self.name = ko.observable(options.name);
                self.entries = ko.observableArray(options.entries || []);
                self.color = ko.observable('category-' + (options.index || 1));

                self.remove = function (entry) {
                    self.entries.remove(entry);
                };

                self.add = function () {
                    self.entries.push(new menuEntry());
                };

            };

            self.icon = ko.observable(options.icon);
            self.categories = ko.observableArray(options.categories);

            self.remove = function (category) {
                self.categories.remove(category);
            };

            self.add = function () {
                var index = (self.categories().length) % 5 + 1;
                var category = new menuCategory({ index: index });
                self.categories.push(category);
            };

        };

        return model;

    }
);