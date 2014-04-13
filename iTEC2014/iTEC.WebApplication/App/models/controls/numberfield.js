define(['models/controls/textfield'],
    function (textfield) {

        var model = function (options) {

            options = options || {};

            var self = this;

            options.type = "number";
            textfield.call(self, options);

        };

        return model;

    }
);