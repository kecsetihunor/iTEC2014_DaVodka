requirejs.config({
    paths: {
        'text': '../Scripts/text',
        'durandal': '../Scripts/durandal',
        'plugins': '../Scripts/durandal/plugins',
        'transitions': '../Scripts/durandal/transitions',
        'async': '../Scripts/require/async',
        'goog': '../Scripts/require/goog',
        'propertyParser': '../Scripts/require/propertyParser'
    }
});

define('jquery', function () { return jQuery; });
define('knockout', ko);

define(['durandal/system', 'durandal/app', 'durandal/viewLocator', 'helpers/bindings', 'helpers/validations'],
    function (system, app, viewLocator, bindings, validations) {
        //>>excludeStart("build", true);
        system.debug(true);
        //>>excludeEnd("build");

        app.title = 'DelightOptimisation';

        app.configurePlugins({
            router: true,
            dialog: true
        });

        app.start().then(function () {
            toastr.options.positionClass = 'toast-bottom-right';
            toastr.options.backgroundpositionClass = 'toast-bottom-right';
            //Replace 'viewmodels' in the moduleId with 'views' to locate the view.
            //Look for partial views in a 'views' folder in the root.
            viewLocator.useConvention();

            ko.validation.configure({
                messageTemplate: 'inline-validation'
            });

            //Show the app by setting the root view model for our application with a transition.
            //app.setRoot('viewmodels/shell', 'loaderEntrance');
            app.setRoot('viewmodels/shell');
        });
    }
);