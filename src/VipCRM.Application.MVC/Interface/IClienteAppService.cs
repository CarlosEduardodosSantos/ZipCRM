using System.Collections.Generic;
using VipCRM.Application.MVC.ViewModels;

namespace VipCRM.Application.MVC.Interface
{
    public interface IClienteAppService
    {
        ClienteViewModel ObterPorId(int id);
        IEnumerable<ClienteViewModel> ObterPorNome(string nome);
        IEnumerable<ClienteViewModel> ObterBoqueados();
        IEnumerable<RankingClientesViewModel> ObterRankingClientes(int dias);
        IEnumerable<RankingClientesViewModel> ObterCarenciaClientes(int dias);
    }
}