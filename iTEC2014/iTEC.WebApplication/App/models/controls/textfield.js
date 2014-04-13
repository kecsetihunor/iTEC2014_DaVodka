define(['models/controls/control'],
    function (control) {

        var model = function (options) {

            options = options || {};

            var self = this;

            control.call(self, options);

            self.text = ko.observable(options.text);
            self.value = ko.observable(options.value);
            self.type = ko.observable(options.type || 'text');
            self.icon = ko.observable(options.icon);

        };

        return model;

    }
);