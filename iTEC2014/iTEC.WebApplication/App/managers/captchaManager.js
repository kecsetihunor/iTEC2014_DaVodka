define(['services/datacontext'],
    function (datacontext) {

        var captchaManager = {
            create: create
        };

        return captchaManager;

        function create(model) {
            return datacontext.captcha.create(model);
        }
    }
);