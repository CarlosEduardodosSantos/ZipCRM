using System;

namespace VipCRM.Core.Domain.Entites
{
    public class DemandaOcorrencia
    {
        public DateTime DataOcorrencia { get; set; }
        public int Ocorrencias { get; set; }
        public int Finalizadas { get; set; }
    }
}