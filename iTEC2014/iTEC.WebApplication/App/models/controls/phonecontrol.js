define(['models/controls/control'],
    function (control) {

        var model = function (options) {

            options = options || {};

            var self = this;

            var phoneEntry = function (options) {

                options = options || {};

                var self = this;

                self.id = ko.observable(options.id);
                self.number = ko.observable(options.number);

            };

            control.call(self, options);
            
            self.phones = ko.observableArray(options.phones);

            self.remove = function (phone) {
                self.phones.remove(phone);
            };

            self.add = function () {
                if (isEveryPhoneDefined(self.phones())) {
                    self.phones.push(new phoneEntry());
                }
            };

            function isEveryPhoneDefined(phones) {
                var isDefined = true;
                for (var index in phones) {
                    isDefined &= (phones[index].number() != undefined && phones[index].number().length > 0);
                }
                return isDefined;
            }

        };

        return model;

    }
);