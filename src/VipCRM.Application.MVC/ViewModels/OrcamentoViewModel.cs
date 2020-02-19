using System;

namespace VipCRM.Application.MVC.ViewModels
{
    public class OrcamentoViewModel
    {
        public int OrcamentoId { get; set; }
        public string Vendedor { get; set; }
        public string Equipe { get; set; }
        public string Cliente { get; set; }
        public DateTime Data { get; set; }
        public string DataToString => Data.ToString("dd-MM-yyyy");
        public decimal Valor { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}