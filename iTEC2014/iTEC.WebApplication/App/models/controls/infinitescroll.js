define(['models/controls/control'],
    function (control) {

        var model = function (options) {

            options = options || {};
            options.infinite = options.infinite || {};

            var self = this;

            control.call(self, options);

            var load = options.load;

            self.value = ko.observableArray(options.value || []);
            self.value.infinite = {
                counter: ko.observable(0),
                entity: ko.observable(options.infinite.entity || 0),
                size: ko.observable(options.infinite.size || 40),
                isUpdating: ko.observable(false),
                done: ko.observable(false)
            };
            self.convert = options.convert || function (list) { return list };
            self.load = function (isReset) {
                if (isReset) {
                    self.reset();
                }

                if (!(self.value.infinite.isUpdating() || self.value.infinite.done())) {
                    var counter = self.value.infinite.counter() + 1;
                    self.value.infinite.counter(counter);

                    self.value.infinite.isUpdating(true);
                    return load()
                        .success(function (data) {
                            if (isResponseCurrent(counter)) {
                                self.value.infinite.entity(data.infinite.entity);
                                self.value.infinite.done(data.done);
                                self.value.pushAll(self.convert(data.result));
                            }
                        })
                        .always(function () {
                            if (isResponseCurrent(counter)) {
                                self.value.infinite.isUpdating(false);
                            }
                        });
                }

                function isResponseCurrent(counter) {
                    return self.value.infinite.counter() == counter;
                }
            };
            self.serialize = function () {
                return {
                    entity: self.value.infinite.entity(),
                    size: self.value.infinite.size()
                };
            }
            self.reset = function () {
                self.value.infinite.entity(undefined);
                self.value.infinite.isUpdating(false);
                self.value.infinite.done(false);
                self.value([]);
            };

        };

        return model;

    }
);