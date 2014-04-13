define([],
    function () {

        var model = function (options) {

            options = options || {};

            var self = this;

            self.steps = ko.observableArray(options.steps);

            self.createStep = function (name, active) {
                return {
                    name: ko.observable(name),
                    active: ko.observable(active)
                };
            };

        };

        return model;

    }
);