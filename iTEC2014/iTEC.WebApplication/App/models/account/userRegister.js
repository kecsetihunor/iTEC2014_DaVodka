define(['models/user/userDetails', 'models/controls/textfield', 'models/controls/captcha'],
    function (userDetails, textfield, captcha) {

        var model = function (options) {

            options = options || {};

            var self = this;

            userDetails.call(self, options);
            
            var base = {
                serialize: self.serialize
            };

            self.confirm = new textfield({ text: 'Confirm password', value: options.password, icon: 'fa-unlock-alt', type: 'password' });
            self.captcha = new captcha({ text: 'Captcha code', value: options.captcha, icon: 'fa-qrcode', create: options.create });
            
            self.confirm.value = self.confirm.value.extend({
                required: true,
                same: {
                    params: function () { return self.password.value(); },
                    message: 'The passwords must match.'
                }
            });
            self.captcha.value = self.captcha.value.extend({
                required: true
            });

            self.serialize = function () {
                var object = base.serialize();
                object.confirm = self.confirm.value();
                object.email = self.email.value();
                object.captcha = {
                    id: self.captcha.id(),
                    text: self.captcha.value()
                }
                return object;
            }

        };

        return model;

    }
);