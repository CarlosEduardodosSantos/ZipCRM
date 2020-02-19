namespace VipCRM.Application.MVC.ViewModels
{
    public class DashboardViewModel
    {
        public int Ocorrencias { get; set; }
        public int OcorrenciaPendente { get; set; }
        public int OcorrenciaRoteiro { get; set; }
        public int OcorrenciaIniciada { get; set; }
        public int OcorrenciaFinalizada { get; set; }

        //Agenda

        public int AgendamentoDia { get; set; }
        public int AgendamentosTodos { get; set; }
    }
}