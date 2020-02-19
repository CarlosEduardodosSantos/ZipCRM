using System;
using System.Collections.Generic;
using VipCRM.Application.MVC.ViewModels;

namespace VipCRM.Application.MVC.Interface
{
    public interface IOcorrenciaAdmAppService : IDisposable
    {
        IEnumerable<OcorrenciaMonitorViewModel> ObterOcorrenciaMonitor();
        IEnumerable<OcorrenciaMonitorViewModel> ObterOcorrenciaMonitorPendentes();
        IEnumerable<OcorrenciaMonitorViewModel> ObterOcorrenciaMonitorPendentes30();
        IEnumerable<OcorrenciaMonitorViewModel> ObterOcorrenciaMonitorPendentes30Anterior();
        OcorrenciaTotalizadorViewModel ObterOcorrenciaTotalizador();
        IEnumerable<OcTecnicoClienteViewModel> ObterTecnicoCliente();
        IEnumerable<DemandaOcorrenciaViewModel> ObterDemandaOcorrencia();
        DashboardAdminViewModel ObterDashBoardAdm();
        IEnumerable<LocalizacaoViewModel> ObterLocalizacaoDiaria();
        IEnumerable<LocalizacaoViewModel> ObterLocalizacaoUsuarioId(string usuarioId);

    }
}