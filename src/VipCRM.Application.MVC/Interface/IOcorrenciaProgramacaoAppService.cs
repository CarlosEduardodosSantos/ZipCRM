using System.Collections.Generic;
using VipCRM.Application.MVC.ViewModels;

namespace VipCRM.Application.MVC.Interface
{
    public interface IOcorrenciaProgramacaoAppService
    {
        IEnumerable<ResultOcorrenciaProgramacaoViewModel> OcorrenciasPorSoftware();
        IEnumerable<ResultOcorrenciaProgramacaoViewModel> OcorrenciasPorCliente();
        IEnumerable<ResultOcorrenciaProgramacaoViewModel> OcorrenciasPorProgramador();
        IEnumerable<OcorrenciaProgramacaoViewModel> OcorrenciaProgramadores(int softwareId, int clienteId, int programadorId);
        IEnumerable<OcorrenciaProgramacaoViewModel> ObterPorUsuario(string usuarioId);
    }
}