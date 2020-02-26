using System;

namespace VipCRM.Application.MVC.ViewModels
{
    public class AbastecimentoViewModel
    {
        public int AbastecimentoId { get; set; }
        public int UsuarioId { get; set; }
        public int VeiculoId { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public VeiculoViewModel Veiculo { get; set; }

        public string AbastecimentoKm { get; set; }
        public decimal AbastecimetnoValor { get; set; }
    }
}