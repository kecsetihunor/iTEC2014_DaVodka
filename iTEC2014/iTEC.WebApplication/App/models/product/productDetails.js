define(['models/entity', 'models/controls/hiddenfield', 'models/controls/textfield'],
    function (entity, hiddenfield, textfield) {

        var model = function (options) {

            options = options || {};

            var self = this;

            entity.call(self, options);

            self.id = new hiddenfield({ value: options.id });
            self.name = new textfield({ text: 'Name', value: options.name, icon: 'fa-tag' });
            self.name.value = self.name.value.extend({ required: true });
            self.price = new textfield({ text: 'Points', value: options.price, icon: 'fa-money' });
            self.price.value = self.price.value.extend({ required: true });

            self.serialize = function () {
                return {
                    id: self.id.value(),
                    name: self.name.value(),
                    price: self.price.value()
                };
            };

        };

        return model;

    }
);