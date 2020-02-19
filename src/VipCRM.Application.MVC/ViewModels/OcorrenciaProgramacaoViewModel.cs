using System;

namespace VipCRM.Application.MVC.ViewModels
{
    public class OcorrenciaProgramacaoViewModel
    {
        public int OcorrenciaId { get; set; }
        public string Software { get; set; }
        public string Cliente { get; set; }
        public int ClienteId { get; set; }
        public string Topico { get; set; }
        public string Descricao { get; set; }
        public int TecnicoId { get; set; }
        public string Tecnico { get; set; }
        public string Programador { get; set; }
        public string Situacao { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime DataEncaminhamento { get; set; }
        public DateTime DataFinalizado { get; set; }
        public string Prioridade { get; set; }
        public string ObsDev { get; set; }
        public string StatusDev { get; set; }
    }
}