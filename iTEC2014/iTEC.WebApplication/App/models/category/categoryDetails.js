define(['models/entity', 'models/controls/hiddenfield', 'models/controls/textfield', 'models/controls/selectentity'],
    function (entity, hiddenfield, textfield, selectentity) {

        var model = function (options) {

            options = options || {};

            var self = this;

            entity.call(self, options);

            self.id = new hiddenfield({ value: options.id });
            self.name = new textfield({ text: 'Name', value: options.name, icon: 'fa-tag' });
            self.name.value = self.name.value.extend({ required: true });
            self.products = new selectentity({
                load: options.load,
                remove: options.remove,
                convert: options.convert
            });

            self.toggle = function (entity) {
                var isInGroup = !entity.inGroup();

                function callback() {
                    entity.inGroup(isInGroup);
                }

                if (isInGroup) {
                    options.add({ categoryId: self.id.value(), entityId: entity.id }).success(callback);
                } else {
                    options.delete({ categoryId: self.id.value(), entityId: entity.id }).success(callback);
                }
            };

            self.serialize = function () {
                return {
                    id: self.id.value(),
                    name: self.name.value()
                };
            }

        };

        return model;

    }
);