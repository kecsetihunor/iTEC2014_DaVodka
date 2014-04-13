define(['models/user/user', 'models/controls/textfield'],
    function (user, textfield) {

        var model = function (options) {

            options = options || {};

            var self = this;

            user.call(self, options);

            var base = {
                serialize: self.serialize
            };

            self.email = new textfield({ text: 'Email address', value: options.password, icon: 'fa-envelope', type: 'email' });
            self.email.value = self.email.value.extend({ required: true });

            self.serialize = function () {
                var object = base.serialize();
                $.extend(object, { email: self.email.value() });
                return object;
            }

        };

        return model;

    }
);