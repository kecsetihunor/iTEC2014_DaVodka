define(['models/controls/textfield'],
    function (textfield) {

        var model = function (options) {

            options = options || {};

            var self = this;

            textfield.call(self, options);

            self.action = {
                icon: ko.observable(options.action.icon),
                click: function () {
                    return options.action.click(self);
                }
            };

        };

        return model;

    }
);