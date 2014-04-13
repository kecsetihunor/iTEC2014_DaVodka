define(['services/datacontext', 'managers/navigationManager', 'models/vote/voteCategory'],
    function (datacontext, navigationManager, voteCategory) {

        var entity = ko.observable();

        var vm = {
            activate: activate,
            entity: entity
        };

        return vm;

        function activate(categoryId) {
            entity(new voteCategory({
                category: {
                    load: datacontext.management.getVotedCategories,
                    convert: function (list) {
                        for (var index in list) {
                            list[index].points = ko.observable(list[index].points);
                        }
                        return list;
                    }
                },
                product: {
                    load: datacontext.management.getProductsFromCategory,
                    vote: datacontext.vote.vote,
                    convert: function (list) {
                        for (var index in list) {
                            list[index].votePoints = ko.observable(list[index].points);
                            list[index].points = ko.observable(list[index].points);
                            list[index].selected = ko.observable(false);
                        }
                        return list;
                    }
                },
                loadPoints: datacontext.vote.getAvailablePoints,
            }));
            entity().categories.load();
            datacontext.reporting.getWelcomeMessage().success(function (data) {
                toastr.info(data.messageBody, 'Welcome!');
            });
            return entity().loadPoints();
        }
    
    }
);