using System;

namespace VipCRM.Core.Domain.Entites
{
    public class Orcamento
    {
        public int OrcamentoId { get; set; }
        public string Vendedor { get; set; }
        public string Equipe { get; set; }
        public string Cliente { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}