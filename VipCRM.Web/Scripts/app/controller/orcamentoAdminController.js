myApp.controller('OrcamentoAdminController',
    ['$scope', '$http', '$interval', function ($scope, $http, $interval) {
        moment.lang("pt-br");

        var mytimeout = 100000;
        var limit = 10;
        var page = 1;

        $interval(callAtInterval, mytimeout);

        function callAtInterval() {
            getAll();
            //$scope.ultimasVendas();
            $scope.rankingEquipe();
        }

        getAll();
        function getAll() {

            $http({
                method: 'POST',
                url: 'OrcamentoAdmin/RankingVendedor'
            }).success(function (data, status, headers, config) {

                $scope.rankings = data;
            });

        }

        $scope.ultimasVendas = function() {
            $http({
                method: 'POST',
                url: 'OrcamentoAdmin/UltimosOrcamentos',
                data: JSON.stringify({ page, limit })
            }).success(function(data, status, headers, config) {

                $scope.orcamentos = data;
            });
        }

        $scope.totalizar = function () {
            $http({
                method: 'POST',
                url: 'OrcamentoAdmin/Totalizar'
            }).success(function (data, status, headers, config) {

                $scope.totalizar = data;
            });
        }

        $scope.rankingEquipe = function() {
            $http({
                method: 'POST',
                url: 'OrcamentoAdmin/RankingEquipe'
            }).success(function(data, status, headers, config) {

                $scope.rankingEquipes = data;
            });
        }

        $scope.Percentual = function (value) {

            return parseInt(value) + "%";
        }
        $scope.getLocation = function(latitude, longitude) {

            return "";
            /*
            return $http({
                method: 'POST',
                url: 'OrcamentoAdmin/GetLocation',
                data: JSON.stringify({ latitude, longitude })
            }).success(function(data, status, headers, config) {

                return data.display_name;
            });*/
        }

    }]);