define([],
    function () {

        var urlRewrite = {
            rewrite: rewrite
        };

        return urlRewrite;

        function rewrite(url, key, value) {
            if (value == undefined && key == undefined) {
                value = key;
                key = url;
                url = window.location.href;
            }

            var urlRegex = new RegExp('(\\?|\\&)' + key + '=.*?(?=(&|$))'),
                queryRegex = /\?.+$/;

            if (value != undefined) {
                if (urlRegex.test(url)) {
                    url = url.replace(urlRegex, '$1' + key + '=' + value);
                } else if (queryRegex.test(url)) {
                    url = url + '&' + key + '=' + value;
                } else {
                    url = url + '?' + key + '=' + value;
                }
            } else {
                url = url.replace(urlRegex, '');
            }

            return url;
        }
    }
);