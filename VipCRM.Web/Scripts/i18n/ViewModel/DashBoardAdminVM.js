var countTimer = 10000;
moment.lang("pt-br");

function percentualGeral(value1, value2) {
    var percentual = ((value1() * 100) / value2());
    return percentual.toFixed(0) + "%";
}

function srcImagem(value) {

    return "Content/Imagem/" + value();
}

function ConvertDateTime(value) {
     
    if (value().substring(0, 6) == "/Date(") {
        var day = moment(value());
        value = moment(day, "DD/MM/YYYY").format("L");
    }
    return value;
}

function situacaoColor(value) {
    var color = (value() == "Execução") ? "green" : "aqua";
    return "label bg-" + color + " pull-right";
}

var ViewModel = function () {
    var self = this;


    self.reload = function () {

        self.Monitores.removeAll();
        self.TecnicoClientes.removeAll();

        ko.mapping.fromJS(new OcorrenciaToptalizador(), {}, self);
    }

    self.CountTecnicosClientes = function () {

        countTimer = (5000 * self.TecnicoClientes().length);

        return countTimer;
    }

    ko.mapping.fromJS(new OcorrenciaToptalizador(), {}, self);


    this.OcorrenciaPendentes = ko.computed(function () {

        return (this.Totalizador.OcorrenciasRoteiro() - this.Totalizador.OcorrenciasConcluidas());
    }, this);

    this.PercentualGeral = ko.computed(function () {

        return ((this.Totalizador.OcorrenciasConcluidas() * 100) / this.Totalizador.OcorrenciasRoteiro()).toFixed(0) + '%';
    }, this);


};

function OcorrenciaToptalizador() {
    try {


        var jsonData = $.ajax({
            url: "DashboardAdmin/CarregaControles",
            data: {},
            dataType: "json",
            type: 'GET',
            contentType: "application/json; charset=utf-8",
            async: false
        }).responseText;
        jsonData = $.parseJSON(jsonData);

        for (var i = 0; i < jsonData.Monitores.length; i++) {

            var value = jsonData.Monitores[i].DataUltAtualizacao;
            if (value.substring(0, 6) == "/Date(") {
                var dt = new Date(parseInt(value.substring(6, value.length - 2)));
                jsonData.Monitores[i].DataUltAtualizacao = dt.toTimeString().substring(0, 5);
            }
        }
        return jsonData;

    } catch (e) {
        alert(e);
        return null;
    }
};

ko.applyBindings(new ViewModel());
