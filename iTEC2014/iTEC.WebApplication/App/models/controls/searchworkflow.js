define(['models/controls/workflow'],
    function (workflow) {

        var model = function (options) {

            options = options || {};

            var self = this;
            
            workflow.call(self, options);

            var steps = [];
            steps.push(self.createStep('Setting the reservation options', false));
            steps.push(self.createStep('Finding the place', false));
            steps.push(self.createStep('Finding the table', false));
            steps.push(self.createStep('Confirm reservation', false));

            options.step = (options.step == undefined || options.step < 0) ? 0 : options.step;
            options.step = (options.step >= steps.length) ? steps.length - 1 : options.step;
            steps[options.step].active(true);

            self.steps(steps);

        };

        return model;

    }
);