define(['models/entity', 'models/controls/textfield'],
    function (entity, textfield) {

        var model = function (options) {

            options = options || {};

            var self = this;

            entity.call(self, options);

            self.money = new textfield({ text: 'Buget', value: options.money, icon: 'fa-money' });
            self.money.value = self.money.value.extend({ required: true });

            self.serialize = function () {
                return {
                    money: self.money.value()
                };
            };

        };

        return model;

    }
);