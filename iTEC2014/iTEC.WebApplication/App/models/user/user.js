define(['models/entity', 'models/controls/hiddenfield', 'models/controls/textfield'],
    function (entity, hiddenfield, textfield) {

        var model = function (options) {

            options = options || {};

            var self = this;

            entity.call(self, options);

            self.id = new hiddenfield({ value: options.id });
            self.username = new textfield({ text: 'Username', value: options.username, icon: 'fa-user' });
            self.password = new textfield({ text: 'Password', value: options.password, icon: 'fa-unlock-alt', type: 'password' });

            self.username.value = self.username.value.extend({ required: true });
            self.password.value = self.password.value.extend({ required: true });

            self.serialize = function () {
                return {
                    id: self.id.value(),
                    username: self.username.value(),
                    password: self.password.value()
                };
            }

            self.reset = function () {
                self.password.value(undefined);
                self.password.value.isModified(false);
            }

        };

        return model;

    }
);