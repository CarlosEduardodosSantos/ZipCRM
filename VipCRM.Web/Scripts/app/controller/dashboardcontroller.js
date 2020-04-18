myApp.controller('MainCtrl', ['$scope', '$http', '$interval', function($scope, $http, $interval) {
    moment.lang("pt-br");

    var mytimeout = 100000;


    $interval(callAtInterval, mytimeout);

    function callAtInterval() {
        getAll();

        $scope.obterLocalizacao();
    }


    getAll();

    function getAll() {

        $http({
            method: 'POST',
            url: 'DashboardAdmin/CarregaControles'
        }).success(function (data, status, headers, config) {

            $scope.Totalizador = data.Totalizador;
            $scope.Monitores = data.Monitores;
            $scope.TecnicoClientes = data.TecnicoClientes;
            $scope.MonitoresPendentes = data.MonitoresPendentes;
            $scope.MonitoresPendentes30 = data.MonitoresPendentes30;
            $scope.MonitoresPendentes30Anterior = data.MonitoresPendentes30Anterior;
            $scope.MonitoresConcluido = data.MonitoresConcluido;
            $scope.MonitoresConcluido30 = data.MonitoresConcluido30;
            $scope.MonitoresConcluidoInt30 = data.MonitoresConcluidoInt30;
            $scope.MonitoresConcluidoExt30 = data.MonitoresConcluidoExt30;
            $scope.MonitoresConcluidoTotal30 = data.MonitoresConcluidoTotal30;
            $scope.MonitoresConcluido30Anterior = data.MonitoresConcluido30Anterior; //data.MonitoresConcluido30Anterior;
            $scope.MonitoresEscalaSabado = data.MonitoresEscalaSabado;
            $scope.MonitoresOcorrenciasDias = data.MonitoresOcorrenciasDias;

            //$scope.timeout = $scope.TecnicoClientes.length * 5000;

            mytimeout = 5000; //$scope.TecnicoClientes.length  * 5000; 

        }).error(function(data, status, headers, config) {
            alert("Erro: " + data.error);
        });
    }


    $scope.TotalPendencia = function() {

        return 0; // ($scope.Totalizador.OcorrenciasRoteiro - $scope.Totalizador.OcorrenciasConcluidas);
    }

    $scope.ConvertDateTime = function(value) {

        if (value.substring(0, 6) == "/Date(") {
            var day = moment(value);
            value = moment(day, "DD/MM/YYYY").format("L");
        }
        return value;
    }
    $scope.openPdv = function(ocorrenciaId) {

        $http({
            method: 'GET',
            url: 'http://maissolucoesvip.ddns.com.br:3103/api/ocorrencia/filePdf/' + ocorrenciaId
        }).success(function (data, status, headers, config) {


            var fileName = "rat_" + ocorrenciaId + ".pdf";
            var byteCharacters = atob(data);
            var byteNumbers = new Array(byteCharacters.length);
            for (var i = 0; i < byteCharacters.length; i++) {
                byteNumbers[i] = byteCharacters.charCodeAt(i);
            }
            var byteArray = new Uint8Array(byteNumbers);
            var blob = new Blob([byteArray], { type: 'application/pdf' });

            if (window.navigator && window.navigator.msSaveOrOpenBlob) { // IE workaround
                window.navigator.msSaveOrOpenBlob(blob, fileName);
            } else {
                var fileURL = URL.createObjectURL(blob);
                window.open(fileURL);
            }


        }).error(function (data, status, headers, config) {
            alert("Erro ao gerar o pdf");
            });
    }

    $scope.jsonTime = function (value) {

        if (!value) return "";

        if (value.substring(0, 6) == "/Date(") {
            var day = moment(value);
            value = moment(day).format("HH:mm");
        }
        return value;
    }

    $scope.percentualRoteiro = function (value1, value2) {
        if (value2 == 0) return "0%";
        var percentual = ((value1 * 100) / value2);
        return parseInt(percentual) + "%";
    }

    $scope.QualMeuIndex = function (index, situacao, tipo) {

        var usuarioId = 0;

        if (tipo == 1)
            usuarioId = $scope.Monitores[index].UsuarioId;
        else
            usuarioId = $scope.MonitoresPendentes[index].UsuarioId;

        $http({
            method: 'POST',
            url: 'DashboardAdmin/PendenciaPorTecnico',
            data: JSON.stringify({ usuarioId: usuarioId, situacao: situacao })
        }).success(function (data, status, headers, config) {

            $scope.Tecnico = data.tecnico;

        }).error(function (data, status, headers, config) {
            alert("Erro");
        });
    }
    $scope.obterLocalizacao = function () {
        
        $http({
            method: 'POST',
            url: 'DashboardAdmin/LocalizacaoDiaria'
        }).success(function (data, status, headers, config) {

            $scope.Localizacoes = data.localizacoes;
            //if ($scope.Localizacoes.length > 0)
            //    $scope.obterEndereco();


        }).error(function (data, status, headers, config) {
            alert("Erro: " + data.error);
        });

    }
    $scope.obterEndereco = function () {

        $($scope.Localizacoes).each(function (i, obj) {
            var latitude = obj.Latitude;
            var longitude = obj.Longitude;

            $http({
                method: 'POST',
                url: "https://maps.google.com/maps/api/geocode/json?key=AIzaSyC7oozBs2TgaHd-6aBiFFit3DSUNXeBo5o&latlng=" + latitude + "," + longitude + "&sensor=true"
            }).success(function (data, status, headers, config) {
                console.log(data);
                obj.Endereco = data.results[0].formatted_address;
                console.log(obj);
                

                });

        });

    }
    /*
    $scope.onTimeout = function () {
        
        getAll();
        
        mytimeout = $timeout($scope.onTimeout, $scope.timeout);
    }

    $scope.stop = function () {
        $timeout.cancel(mytimeout);
    }
    */
    $scope.myColor = function (date) {
        var now = moment().format("L");
        var day = moment(date).format("L");

        if (day !== now)
            return "#ff6347";
        else {
            return "";
        }
    }

}]);

myApp.controller('ProgramacarCtrl', ['$scope', '$http', '$timeout', function ($scope, $http, $timeout) {
    moment.lang("pt-br");


    getAll();
    function getAll() {

        $http({
            method: 'POST',
            url: 'DashBoardProgramacao/CarregaControles'
        }).success(function (data, status, headers, config) {

            $scope.Totalizador = data.Totalizador;
            $scope.Monitores = data.Monitores;
            $scope.TecnicoClientes = data.TecnicoClientes;

            alert("Entrou");

            $scope.timeout = $scope.TecnicoClientes.length * 5000;

            mytimeout = $timeout($scope.onTimeout, scope.timeout);

        }).error(function (data, status, headers, config) {
            alert("Erro: " + data.error);
        });
    }

}]);