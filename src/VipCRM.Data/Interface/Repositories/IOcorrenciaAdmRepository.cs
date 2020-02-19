using System;
using System.Collections.Generic;
using VipCRM.Core.Domain.Entites;

namespace VipCRM.Data.Interface.Repositories
{
    public interface IOcorrenciaAdmRepository
    {
        IEnumerable<OcorrenciaMonitor> ObterOcorrenciaMonitor();
        IEnumerable<OcorrenciaMonitor> ObterOcorrenciaMonitorPendentes();
        IEnumerable<OcorrenciaMonitor> ObterOcorrenciaMonitorPendentes30();
        IEnumerable<OcorrenciaMonitor> ObterOcorrenciaMonitorPendentes30Anterior();
        IEnumerable<OcorrenciaMonitor> ObterOcorrenciaMonitorConcluido();
        IEnumerable<OcorrenciaMonitor> ObterOcorrenciaMonitorConcluido30();
        IEnumerable<OcorrenciaMonitor> ObterOcorrenciaMonitorConcluido30Anterior();
        IEnumerable<SabadosMonitor> ObterEscalaSabados();
        IEnumerable<OcorrenciaMonitor> ObterOcorrenciasDias();
        OcorrenciaTotalizador OcorrenciaTotalizador();
        IEnumerable<Localizacao> ObterLocalizacaoDiaria();
        IEnumerable<Localizacao> ObterLocalizacaoUsuarioId(string usuarioId);
    }
}