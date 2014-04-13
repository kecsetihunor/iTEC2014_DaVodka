define(['services/datacontext', 'plugins/router', 'helpers/urlRewrite'],
    function (datacontext, router, urlRewrite) {

        var routes = {
            login: { route: 'login', moduleId: 'viewmodels/account/login', title: 'Login', nav: true },
            register: { route: 'register', moduleId: 'viewmodels/account/register', title: 'Register', nav: true },
            user: {
                vote: { route: 'vote(/:id)', moduleId: 'viewmodels/user/vote', hash: '#vote', title: 'Menu', nav: false },
            },
            administrator: {
                task: { route: 'task', moduleId: 'viewmodels/administrator/task', title: 'Tasks', nav: false },
                categoryHome: { route: 'category', moduleId: 'viewmodels/administrator/menu/category/home', title: 'Manage categories', nav: false },
                categoryDetails: { route: 'category/details(/:id)', moduleId: 'viewmodels/administrator/menu/category/details', hash: '#category/details', title: 'Category details', nav: false },
                productHome: { route: 'product', moduleId: 'viewmodels/administrator/menu/product/home', title: 'Manage menu', nav: false },
                productDetails: { route: 'product/details(/:id)', moduleId: 'viewmodels/administrator/menu/product/details', hash: '#product/details', title: 'Product details', nav: false },
                groupHome: { route: 'group', moduleId: 'viewmodels/administrator/groups/home', title: 'Manage groups', nav: false },
                groupDetails: { route: 'group/details(/:id)', moduleId: 'viewmodels/administrator/groups/details', hash: '#group/details', title: 'Group details', nav: false },
                userHome: { route: 'user', moduleId: 'viewmodels/administrator/users/home', title: 'Manage users', nav: false },
                userDetails: { route: 'user/details(/:id)', moduleId: 'viewmodels/administrator/users/details', hash: '#user/details', title: 'User details', nav: false },
                reportHome: { route: 'report', moduleId: 'viewmodels/administrator/report/home', title: 'Reporting', nav: false },
                reportChart: { route: 'chart', moduleId: 'viewmodels/administrator/report/chart', title: 'Reporting chart', nav: false }
            }
        };

        var allRoutes = [
            routes.login,
            routes.register,

            routes.user.vote,

            routes.administrator.task,
            routes.administrator.categoryHome,
            routes.administrator.categoryDetails,
            routes.administrator.productHome,
            routes.administrator.productDetails,
            routes.administrator.groupHome,
            routes.administrator.groupDetails,
            routes.administrator.userHome,
            routes.administrator.userDetails,
            routes.administrator.reportHome,
            routes.administrator.reportChart
        ];

        var anonymousRoutes = [
            routes.login,
            routes.register
        ];

        var userRoutes = [
            routes.user.vote
        ];

        var administratorRoutes = [
            routes.administrator.task,
            routes.administrator.categoryHome,
            routes.administrator.categoryDetails,
            routes.administrator.productHome,
            routes.administrator.productDetails,
            routes.administrator.groupHome,
            routes.administrator.groupDetails,
            routes.administrator.userHome,
            routes.administrator.userDetails,
            routes.administrator.reportHome,
            routes.administrator.reportChart
        ];

        var username = ko.observable(),
            role = ko.observable(),
            isAuthenticated = ko.observable(false),
            isAnonymous = ko.computed(function () {
                return isAuthenticated() && role() == 0;
            }),
            isAdministrator = ko.computed(function () {
                return isAuthenticated() && role() == 1;
            }),
            isUser = ko.computed(function () {
                return isAuthenticated() && role() == 2;
            }),
            isLoggedIn = ko.computed(function () {
                return isAuthenticated() && username() != undefined;
            }),
            currentRoutes = ko.computed(function () {
                var routes = [];
                if (isLoggedIn()) {
                    if (isAdministrator()) {
                        routes = administratorRoutes;
                    } else {
                        routes = userRoutes;
                    }
                } else {
                    routes = anonymousRoutes;
                }

                return routes;
            }),
            isAvailable = ko.computed(function () {
                return username() != undefined;
            }),
            avatar = ko.observable();

        currentRoutes.subscribe(function (routes) {
            changeRoutes(allRoutes, false);
            changeRoutes(routes, true);
            buildRoutes();
        });

        var navigationManager = {
            username: username,
            avatar: avatar,
            isAvailable: isAvailable,
            isLoggedIn: isLoggedIn,
            isUser: isUser,
            isAdministrator: isAdministrator,

            routes: routes,
            router: router,
            buildRoutes: buildRoutes,
            rewrite: rewrite,
            clearUser: clearUser,

            state: state,
            authenticate: authenticate,
            register: register,
            logout: logout
        };

        return navigationManager;

        function buildRoutes() {
            router.reset();
            router.guardRoute = function (routeInfo, params, instance) {
                var guard = routeExists(currentRoutes(), params.fragment);
                if (!guard) {
                    guard = currentRoutes()[0].hash;
                }
                return guard;
            };

            router.map(allRoutes)
                .mapUnknownRoutes(function (instruction) {
                    console.log('Unknown route!');
                })
                .buildNavigationModel();

            return router;
        }

        function rewrite(hash, key, value) {
            return urlRewrite.rewrite(hash, key, value);
        }

        function clearUser() {
            $.removeCookie('.CACHE');
            username(undefined);
            avatar(undefined);
        }

        function state() {
            var model = undefined;
            var id = $.cookie('.CACHE');
            if (id) {
                model = { id: id };
            }
            return datacontext.account.state(model)
                .success(function (data) {
                    isAuthenticated(data.isAuthenticated);
                    username(data.username);
                    role(data.role);
                    avatar(data.avatar);
                });
        }

        function authenticate(model) {
            return datacontext.account.authenticate(model)
                .success(function (data) {
                    isAuthenticated(data.isAuthenticated);
                    if (isAuthenticated()) {
                        username(data.username);
                        role(data.role);
                        avatar(data.avatar);

                        if (isUser()) {
                            router.navigate(routes.user.vote.route);
                        } else if (isAdministrator()) {
                            router.navigate(routes.administrator.task.route);
                        }
                    }
                });
        }

        function register(model) {
            return datacontext.account.register(model)
                .success(function () {
                    return authenticate(model);
                });
        }

        function logout() {
            return datacontext.account.logout()
                .success(function () {
                    return state()
                        .success(function () {
                            return router.navigate(routes.login.route);
                        });
                });
        }

        //#region internal

        function routeExists(array, route) {
            for (var index = 0; index < array.length; ++index) {
                var pattern = new RegExp(array[index].routePattern);
                if (pattern.test(route)) {
                    return true;
                }
            }
            return false;
        }

        function changeRoutes(array, navigation) {
            for (var index = 0; index < array.length; ++index) {
                array[index].nav = navigation;
            }
        }

        //#endregion
    }
);