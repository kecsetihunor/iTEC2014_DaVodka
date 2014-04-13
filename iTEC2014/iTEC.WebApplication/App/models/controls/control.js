define([],
    function () {

        var model = function (options) {

            options = options || {};

            var self = this;

            window.generated_id = window.generated_id || 0;
            ++window.generated_id;

            self.cid = ko.observable('generated_id_' + window.generated_id);

        };

        return model;

    }
);