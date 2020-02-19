using System.Collections.Generic;
using VipCRM.Core.Domain.Entites;

namespace VipCRM.Data.Interface.Repositories
{
    public interface IDemandaOcorrenciaRepository
    {
        IEnumerable<DemandaOcorrencia> ObterTodos();
    }
}