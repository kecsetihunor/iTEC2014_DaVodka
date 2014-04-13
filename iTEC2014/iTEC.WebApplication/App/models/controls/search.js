define(['models/controls/infinitescroll'],
    function (infinitescroll) {

        var model = function (options) {

            options = options || {};

            var self = this;

            infinitescroll.call(self, options);

            var base = {
                serialize: self.serialize
            };

            self.icon = ko.observable(options.icon);
            self.text = ko.observable(options.text);
            self.criteria = ko.observable(options.criteria).extend({ throttle: 500 });

            self.criteria.subscribe(function (value) {
                self.load(true);
            });

            self.serialize = function () {
                return {
                    criteria: self.criteria(),
                    infinite: base.serialize()
                };
            };

        };

        return model;

    }
);