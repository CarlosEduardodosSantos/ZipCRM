using System.Collections.Generic;

namespace VipCRM.Application.MVC.ViewModels
{
    public class DashboardAdminViewModel
    {
        public IEnumerable<OcorrenciaMonitorViewModel> OcorrenciaMonitorViewModel { get; set; }
        public IEnumerable<OcTecnicoClienteViewModel> OcTecnicoClienteViewModel { get; set; }
        public IEnumerable<DemandaOcorrenciaViewModel> DemandaOcorrencia { get; set; }
    }
}