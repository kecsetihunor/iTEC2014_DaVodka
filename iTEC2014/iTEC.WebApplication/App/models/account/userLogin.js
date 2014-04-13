define(['models/user/user', 'models/controls/checkbox'],
    function (user, checkbox) {

        var model = function (options) {

            options = options || {};

            var self = this;

            user.call(self, options);
            
            var base = {
                serialize: self.serialize
            };

            self.remember = new checkbox({ text: "Remember me", value: options.remember || false });
            self.avatar = ko.observable((options.avatar) ? 'url(' + options.avatar + ')' : undefined);
            self.isAvailable = ko.observable(options.remember);

            self.serialize = function () {
                var object = base.serialize();
                object.remember = self.remember.value();
                return object;
            }

            self.clear = function () {
                self.username.value(undefined);
                self.username.value.isModified(false);

                self.password.value(undefined);
                self.password.value.isModified(false);

                self.avatar(undefined);

                self.isAvailable(false);
            }

        };

        return model;

    }
);