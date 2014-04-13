define([],
    function () {

        var account = {
            state: function (model) {
                return get('api/account/state', model).fail(fail);

                function fail(data) {
                    error(data.responseText, 'Failed to retrieve authentication state.');
                }
            },
            authenticate: function (model) {
                return post('api/account/authenticate', model).fail(fail);

                function fail(data) {
                    error(data.responseText, 'Failed to authenticate.');
                }
            },
            register: function (model) {
                return post('api/account/register', model).fail(fail);

                function fail() {
                    error('Failed to register.');
                }
            },
            logout: function () {
                return post('api/account/logout').fail(fail);

                function fail() {
                    error('Failed to sign out.');
                }
            }
        };

        var management = {
            //menu
            getCategories: function (model) {
                return post('api/category/getCategories', model).fail(fail);

                function fail() {
                    error('Failed to retrieve categories.');
                }
            },
            getVotedCategories: function (model) {
                return post('api/category/getVotedCategories', model).fail(fail);

                function fail() {
                    error('Failed to retrieve categories.');
                }
            },
            getCategory: function (model) {
                return post('api/category/getCategory', model).fail(fail);

                function fail() {
                    error('Failed to retrieve category.');
                }
            },
            saveCategory: function (model) {
                return post('api/category/saveCategory', model).fail(fail);

                function fail() {
                    error('Failed to save category.');
                }
            },
            deleteCategory: function (model) {
                return post('api/category/deleteCategory', model).fail(fail);

                function fail() {
                    error('Failed to delete category.');
                }
            },
            addProductToCategory: function (model) {
                return post('api/category/addProductToCategory', model).fail(fail);

                function fail() {
                    error('Failed to add product to category.');
                }
            },
            removeProductFromCategory: function (model) {
                return post('api/category/removeProductFromCategory', model).fail(fail);

                function fail() {
                    error('Failed to remove product from category.');
                }
            },
            getAllProductsCompareToSpecificCategory: function (model) {
                return post('api/product/getAllProductsCompareToSpecificCategory', model).fail(fail);

                function fail() {
                    error('Failed to retrieve products.');
                }
            },
            saveProduct: function (model) {
                return post('api/product/saveProduct', model).fail(fail);

                function fail() {
                    error('Failed to save product.');
                }
            },
            getProductsFromCategory: function (model) {
                return post('api/category/getProductsFromCategory', model).fail(fail);

                function fail() {
                    error('Failed to retrieve products from category.');
                }
            },
            getProduct: function (model) {
                return post('api/product/getProduct', model).fail(fail);

                function fail() {
                    error('Failed to retrieve product.');
                }
            },

            //users
            getUsers: function (model) {
                return post('api/user/getUsers', model).fail(fail);

                function fail() {
                    error('Failed to retrieve users.');
                }
            },
            getUser: function (model) {
                return post('api/user/getUser', model).fail(fail);

                function fail() {
                    error('Failed to retrieve user.');
                }
            },
            saveUser: function (model) {
                return post('api/user/saveUser', model).fail(fail);

                function fail() {
                    error('Failed to save user.');
                }
            },
            deleteUser: function (model) {
                return post('api/user/deleteUser', model).fail(fail);

                function fail() {
                    error('Failed to delete user.');
                }
            },
            getAllUsersCompareToSpecificGroup: function (model) {
                return post('api/user/getAllUsersCompareToSpecificGroup', model).fail(fail);

                function fail() {
                    error('Failed to retrieve users.');
                }
            },

            //groups
            getGroups: function (model) {
                return post('api/group/getGroups', model).fail(fail);

                function fail() {
                    error('Failed to retrieve groups.');
                }
            },
            getGroup: function (model) {
                return post('api/group/getGroup', model).fail(fail);

                function fail() {
                    error('Failed to retrieve group.');
                }
            },
            getGroupUsers: function (model) {
                return post('api/group/getUsersFromGroup', model).fail(fail);

                function fail() {
                    error('Failed to retrieve group.');
                }
            },
            addUserToGroup: function (model) {
                return post('api/group/addUserToGroup', model).fail(fail);

                function fail() {
                    error('Failed to add user to group.');
                }
            },
            removeUserFromGroup: function (model) {
                return post('api/group/removeUserFromGroup', model).fail(fail);

                function fail() {
                    error('Failed to delete user from group.');
                }
            },
            saveGroup: function (model) {
                return post('api/group/saveGroup', model).fail(fail);

                function fail() {
                    error('Failed to save group.');
                }
            },
            deleteGroup: function (model) {
                return post('api/group/deleteGroup', model).fail(fail);

                function fail() {
                    error('Failed to delete group.');
                }
            },
        };

        var vote = {
            getAvailablePoints: function (model) {
                return post('api/vote/getAvailablePoints', model).fail(fail);

                function fail() {
                    error('Failed to retrieve available vote points.');
                }
            },
            vote: function (model) {
                return post('api/vote/vote', model).fail(fail);

                function fail() {
                    error('Failed to vote to product.');
                }
            }
        };

        var captcha = {
            create: function (model) {
                return post('api/captcha/create', model).fail(fail);

                function fail() {
                    error('Failed to retrieve the captcha image.');
                }
            }
        };

        var reporting = {
            getWelcomeMessage: function (model) {
                return post('api/report/getWelcomeMessage', model).fail(fail);

                function fail() {
                    error('Failed to retrieve welcome message.');
                }
            },
            saveWelcomeMessage: function (model) {
                return post('api/report/saveWelcomeMessage', model).fail(fail);

                function fail() {
                    error('Failed to save welcome message.');
                }
            },
            getProductVoteReport: function (model) {
                return post('api/report/getProductVoteReport', model).fail(fail);

                function fail() {
                    error('Failed to retrieve report.');
                }
            },
            getBudgetReport: function (model) {
                return post('api/report/getBudgetReport', model).fail(fail);

                function fail() {
                    error('Failed to retrieve report.');
                }
            },
            startSession: function (model) {
                return post('api/report/startSession', model).fail(fail);

                function fail() {
                    error('Failed to start a new session.');
                }
            }
        }

        var datacontext = {
            account: account,
            management: management,
            vote: vote,
            reporting: reporting,
            captcha: captcha
        };

        return datacontext;

        function error(response, message) {
            if (typeof response !== 'string') {
                var responses = eval(response);
                if (responses && responses.length > 0) {
                    message = '';
                    for (var index in responses) {
                        message += responses[index] + '</br>';
                    }
                    toastr.error(message, 'Warning!');
                } else if (message) {
                    toastr.error(message, 'Warning!');
                }
            } else {
                toastr.error(response, 'Warning!');
            }
        }

        function get(url, model) {
            return $.ajax({
                type: 'GET',
                url: url,
                data: model
            });
        }

        function post(url, model) {
            var token = $('input[name="__RequestVerificationToken"]').val();

            return $.ajax({
                headers: { __RequestVerificationToken: token },
                type: 'POST',
                url: url,
                data: model,
                dataType: 'json'
            });
        }
    }
);