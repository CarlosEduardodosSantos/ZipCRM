using System;

namespace VipCRM.Application.MVC.ViewModels
{
    public class DemandaOcorrenciaViewModel
    {
        public DateTime DataOcorrencia { get; set; }
        public int Ocorrencias { get; set; }
        public int Finalizadas { get; set; }
    }
}