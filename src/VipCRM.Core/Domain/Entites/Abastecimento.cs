using System;

namespace VipCRM.Core.Domain.Entites
{
    public class Abastecimento
    {
        public int AbastecimentoId { get; set; }
        public int UsuarioId { get; set; }
        public int VeiculoId { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public Veiculo Veiculo { get; set; }

        public string AbastecimentoKm { get; set; }
        public decimal AbastecimetnoValor { get; set; }
    }
}