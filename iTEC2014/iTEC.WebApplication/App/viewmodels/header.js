define(['managers/navigationManager'],
    function (navigationManager) {

        var isMenuShown = ko.observable(false);

        var vm = {
            activate: activate,
            navigationManager: navigationManager,
            toggleMenu: toggleMenu,
            isMenuShown: isMenuShown
        };

        return vm;

        function activate() {
            return true;
        }

        function toggleMenu() {
            isMenuShown(!isMenuShown());
        }
    
    }
);