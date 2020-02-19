using System.Collections.Generic;
using VipCRM.Application.MVC.ViewModels;

namespace VipCRM.Application.MVC.Interface
{
    public interface IDashBoardAdminAppService
    {
        IEnumerable<OcorrenciaMonitorViewModel> RoteiroDia();
        IEnumerable<OcorrenciaMonitorViewModel> RoteiroPendentes();
        IEnumerable<OcorrenciaMonitorViewModel> RoteiroPendentes30();
        IEnumerable<OcorrenciaMonitorViewModel> RoteiroPendentes30Anterior();
        IEnumerable<OcorrenciaMonitorViewModel> RoteiroConcluido();
        IEnumerable<OcorrenciaMonitorViewModel> RoteiroConcluido30();
        IEnumerable<OcorrenciaMonitorViewModel> RoteiroConcluido30Anterior();
        IEnumerable<OcorrenciaMonitorViewModel> ObterOcorrenciasDias();
        IEnumerable<SabadosMonitorViewModel> EscalaSabados();
        OcorrenciaTotalizadorViewModel Totalizador();
        IEnumerable<OcTecnicoClienteViewModel> OcorrenciasPorTecnico();
        IEnumerable<DemandaOcorrenciaViewModel> DemandaOcorrencia();
        OcTecnicoClienteViewModel PendenciaPorTecnico(int usuarioId, string situacao);
        IEnumerable<LocalizacaoViewModel> ObterLocalizacaoDiaria();
        IEnumerable<LocalizacaoViewModel> ObterLocalizacaoUsuarioId(string usuarioId);
    }
}