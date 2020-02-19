using System.Collections.Generic;
using VipCRM.Core.Domain.Entites;

namespace VipCRM.Data.Interface.Repositories
{
    public interface IOcTecnicoClienteRepository
    {
        IEnumerable<OcTecnicoCliente> ObterOcottenciaTecnicoClientes();
        OcTecnicoCliente PendenciaPorTecnico(int usuarioId, string situacao);
    }
}