define(['services/datacontext', 'managers/navigationManager', 'models/report/buget'],
    function (datacontext, navigationManager, buget) {

        var hash = {
            task: navigationManager.routes.administrator.task.hash
        };

        var entity = ko.observable();
        var link = ko.observable();
        var chart = ko.observable();

        var vm = {
            activate: activate,
            entity: entity,
            chart: chart,
            hash: hash,
            submit: submit,
            link: link,
            getProductVoteReport: getProductVoteReport,
            startSession: startSession
        };

        return vm;

        function activate() {
            entity(new buget());
            return true;
        }

        function submit() {
            return datacontext.reporting.getBudgetReport(entity().serialize())
                .success(function (data) {
                    chart(data.report);
                    link(data.url)
                });
        }

        function startSession() {
            return datacontext.reporting.startSession();
        }

        function getProductVoteReport() {
            return datacontext.reporting.getProductVoteReport()
                    .success(function (data) {
                        chart(data.report);
                        link(data.file)
                    });
        }
    
    }
);