using System;

namespace VipCRM.Core.Domain.Entites
{
    public class OcorrenciaMonitor
    {
        public int UsuarioId { get; set; }
        public string UsuarioNome { get; set; }
        public string ClienteNome { get; set; }
        public string UsuarioVeiculo { get; set; }
        public int QtdeOcorrencia { get; set; }
        public int QtdeOcorrenciaConcluida { get; set; }
        public int QtdeConcluida { get; set; }
        public int QtdeConcluida30 { get; set; }
        public int QtdeConcluida30Anterior { get; set; }
        public int QtdeOcorrenciaPendente { get; set; }
        public int QtdeOcorrenciaPendente30 { get; set; }
        public int QtdeOcorrenciaPendente30Anterior { get; set; }
        public int QtdeOcorrenciaExecucao { get; set; }
        public DateTime DataUltAtualizacao { get; set; }
        public int ocorrencia { get; set; }
        public string nomecliente { get; set; }
        public string hora { get; set; }
        public string qtdehoras { get; set; }
        public string prioridade { get; set; }
        public string problema { get; set; }
    }
}