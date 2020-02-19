namespace VipCRM.Core.Domain.Entites
{
    public class Deslocamento
    {
        public decimal Quilometragem { get; set; }
        public decimal ValorPedagio { get; set; }
        public decimal ValorAlimentacao { get; set; }
        public decimal ValorHospedagem { get; set; }
        public decimal ValorOutros { get; set; }
    }
}