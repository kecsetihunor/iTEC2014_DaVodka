define(['models/controls/manageentity'],
    function (manageentity) {

        var model = function (options) {

            options = options || {};

            var self = this;

            manageentity.call(self, options);

            self.toggle = function (entity) {
                entity.hasChanged(true);
                entity.inGroup(!entity.inGroup());
            };

            self.chain = function (entities, index) {
                if (index === undefined) {
                    index = 0;
                }
                var entity = entities[index];
                var action = entity.callback(entity.model);
                if (++index < entities.length) {
                    action = action.success(function () {
                        return self.chain(entities, index);
                    });
                }
                return action;
            };

        };

        return model;

    }
);