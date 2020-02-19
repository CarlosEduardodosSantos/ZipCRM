using System.Collections.Generic;
using VipCRM.Core.Domain.Entites;

namespace VipCRM.Data.Interface.Repositories
{
    public interface IOcorrenciaProgramacaoRepository
    {
        IEnumerable<ResultOcorrenciaProgramacao> OcorrenciasPorSoftware();
        IEnumerable<ResultOcorrenciaProgramacao> OcorrenciasPorCliente();
        IEnumerable<ResultOcorrenciaProgramacao> OcorrenciasPorProgramador();
        IEnumerable<OcorrenciaProgramacao> OcorrenciaProgramadores(int softwareId, int clienteId, int programadorId);
        IEnumerable<OcorrenciaProgramacao> ObterPorUsuario(string usuarioId);
    }
}