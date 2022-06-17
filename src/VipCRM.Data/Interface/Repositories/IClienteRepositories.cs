using System.Collections.Generic;
using VipCRM.Core.Domain.Entites;

namespace VipCRM.Data.Interface.Repositories
{
    public interface IClienteRepositories
    {
        Cliente ObterPorId(int id);
        IEnumerable<Cliente> ObterPorNome(string nome);
        IEnumerable<Cliente> ObterBoqueados();
        IEnumerable<RankingClientes> ObterRankingClientes(int dias);
        IEnumerable<RankingClientes> ObterCarenciaClientes(int dias);
    }
}