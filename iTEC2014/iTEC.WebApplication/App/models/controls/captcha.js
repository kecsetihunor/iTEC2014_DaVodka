define(['models/controls/textfield'],
    function (textfield) {

        var model = function (options) {

            options = options || {};

            var self = this;

            textfield.call(self, options);

            var last = new Date().getTime() - 2100;
            var create = options.create;

            self.ready = ko.observable(false);
            self.id = ko.observable();
            self.image = ko.observable(options.value);
            self.size = ko.observable({
                width: 237,
                height: 78
            });
            self.refresh = function () {
                var ticks = new Date().getTime();
                if (ticks - last > 2000) {
                    last = ticks;

                    clear();
                    return create(self.size()).success(function (data) {
                        var difference = new Date().getTime() - ticks;

                        function replace() {
                            set(data);
                        }

                        if (difference < 500) {
                            setTimeout(replace, 500 - difference);
                        } else {
                            replace();
                        }
                    });
                }
            }

            function clear() {
                self.ready(false);
                self.id(undefined);
                self.image(undefined);
                reset();
            };

            function reset() {
                self.value(undefined);
                self.value.isModified(false);
            }

            function set(data) {
                self.id(data.id);
                self.image('url(' + data.image + ')');
                self.ready(true);
            };

        };

        return model;

    }
);