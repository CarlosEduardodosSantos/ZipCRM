using System;

namespace VipCRM.Application.MVC.ViewModels
{
    public class OcTecnicoClienteItemViewModel
    {
        public string ClienteNome { get; set; }
        public DateTime DataAgendamento { get; set; }
        public string Situacao { get; set; }
        public DateTime? DataInicioVip { get; set; }
        public DateTime? DataFimVip { get; set; }
        public DateTime DataRoteiro { get; set; }
        public DateTime HoraRoteiro { get; set; }
        public string Veiculo { get; set; }
    }
}