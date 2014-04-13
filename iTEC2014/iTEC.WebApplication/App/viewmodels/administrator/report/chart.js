define(['services/datacontext'],
    function (datacontext) {

        var entity = ko.observable();
        var link = ko.observable();

        var vm = {
            activate: activate,
            entity: entity
        };

        return vm;

        function activate(context) {
            return datacontext.reporting.getBudgetReport({ money: context.money })
                .success(function (data) {
                    entity(data.report);
                    link(data.url)
                });
        }
    
    }
);