define([],
    function () {

        var model = function (options) {

            options = options || {};

            var self = this;

            self.validate = function () {
                ko.validation.group(self, { deep: true });

                var valid = self.isValid();
                if (!valid) {
                    self.errors.showAllMessages();
                }

                return valid;
            };

        };

        return model;

    }
);