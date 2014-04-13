define(['models/entity', 'models/controls/textfield'],
    function (entity, textfield) {

        var model = function (options) {

            options = options || {};

            var self = this;

            entity.call(self, options);

            self.messageBody = new textfield({ text: 'Welcome message', value: options.messageBody, icon: 'fa-comments' });
            self.messageBody.value = self.messageBody.value.extend({ required: true });

            self.serialize = function () {
                return {
                    messageBody: self.messageBody.value()
                };
            }

        };

        return model;

    }
);