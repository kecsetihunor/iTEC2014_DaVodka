define(['models/entity', 'models/controls/hiddenfield', 'models/controls/textfield', 'models/controls/selectentity'],
    function (entity, hiddenfield, textfield, selectentity) {

        var model = function (options) {

            options = options || {};

            var self = this;

            entity.call(self, options);

            self.id = new hiddenfield({ value: options.id });
            self.name = new textfield({ text: 'Name', value: options.name, icon: 'fa-globe' });
            self.name.value = self.name.value.extend({ required: true });
            self.points = new textfield({ text: 'Points', value: options.points, icon: 'fa-money' });
            self.points.value = self.points.value.extend({ required: true });
            self.users = new selectentity({
                load: options.load,
                remove: options.remove,
                convert: options.convert
            });

            self.serialize = function () {
                return {
                    id: self.id.value(),
                    name: self.name.value(),
                    points: self.points.value()
                };
            };

        };

        return model;

    }
);