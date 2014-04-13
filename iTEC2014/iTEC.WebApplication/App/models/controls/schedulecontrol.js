define(['models/controls/control'],
    function (control) {

        var model = function (options) {

            var days = ['Weekdays', 'Saturday', 'Sunday'];

            options = options || {};

            var self = this;

            control.call(self, options);

            var scheduleEntry = function (options) {

                options = options || {};

                var self = this;

                self.id = ko.observable(options.id);
                self.name = ko.observable(options.name);
                self.enabled = ko.observable(options.enabled || false);
                self.icon = ko.computed(function () {
                    return self.enabled() ? "fa-check" : "fa-times";
                });
                self.open = ko.observable(options.open || '10:00');
                self.close = ko.observable(options.close || '01:00');
                self.toggle = function () {
                    self.enabled(!self.enabled());
                };

            };
            
            var schedules = [];
            for (var index in days) {
                var day = days[index].toLowerCase();
                options[day] = options[day] || {};
                options[day].name = days[index];
                var schedule = new scheduleEntry(options[day]);
                schedules.push(schedule);
            }
            
            self.schedules = ko.observableArray(schedules);

        };

        return model;

    }
);