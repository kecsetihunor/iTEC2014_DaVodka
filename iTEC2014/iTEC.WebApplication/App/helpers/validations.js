/*
 * Determines if a field is required or not based on a function or value
 * Parameter: boolean function, or boolean value
 * Example
 *
 * viewModel = {
 *   var vm = this;
 *   vm.isRequired = ko.observable(false);
 *   vm.requiredField = ko.observable().extend({ conditional_required: vm.isRequired});
 * }   
*/
ko.validation.rules['conditional'] = {
    validator: function (val, condition) {
        var required = false;
        if (typeof condition == 'function') {
            required = condition();
        }
        else {
            required = condition;
        }

        if (required) {
            return !(val == undefined || val == null || val.length == 0);
        }
        else {
            return true;
        }
    },
    message: 'This field is required2222.'
}

/*
 * Ensures a field has the same value as another field (E.g. "Confirm Password" same as "Password"
 * Parameter: otherField: the field to compare to
 * Example
 *
 * viewModel = {
 *   var vm = this;
 *   vm.password = ko.observable();
 *   vm.confirmPassword = ko.observable();
 * }   
 * viewModel.confirmPassword.extend( areSame: { params: viewModel.password, message: "Confirm password must match password" });
*/
ko.validation.rules['same'] = {
    validator: function (val, otherField) {
        return val === ko.validation.utils.getValue(otherField);
    },
    message: 'Confirm password must match password'
};