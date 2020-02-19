using System.Collections.Generic;
using VipCRM.Core.Domain.Entites;

namespace VipCRM.Data.Interface.Repositories
{
    public interface IOcorrenciaImagemRepositories
    {
        OcorrenciaImagem ObterPorId(int id);
        IEnumerable<OcorrenciaImagem> ObterPorOcorrencia(int ocorrenciaId);
    }
}