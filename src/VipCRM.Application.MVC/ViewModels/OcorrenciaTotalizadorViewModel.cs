namespace VipCRM.Application.MVC.ViewModels
{
    public class OcorrenciaTotalizadorViewModel
    {
        public int OcorrenciasRoteiro { get; set; }
        public int OcorrenciasConcluidas { get; set; }
        public int OcorrenciasExecucao { get; set; }
        public int OcorrenciaAbertaGeral { get; set; }
        public int OcorrenciaPendentes { get; set; }
        public int OcorrenciaConcluidasTotal { get; set; }
        public int OcorrenciaPendentesTotal { get; set; }
    }
}