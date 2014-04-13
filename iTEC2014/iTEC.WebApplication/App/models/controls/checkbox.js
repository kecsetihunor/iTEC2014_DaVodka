define(['models/controls/textfield'],
    function (textfield) {

        var model = function (options) {

            options = options || {};

            var self = this;

            textfield.call(self, options);

            self.icon = ko.computed(function () {
                return self.value() ? "fa-check" : "fa-times";
            });

        };

        return model;

    }
);