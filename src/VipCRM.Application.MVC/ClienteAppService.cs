using System.Collections.Generic;
using AutoMapper;
using VipCRM.Application.MVC.Interface;
using VipCRM.Application.MVC.ViewModels;
using VipCRM.Core.Domain.Entites;
using VipCRM.Data.Interface.Repositories;

namespace VipCRM.Application.MVC
{
    public class ClienteAppService : IClienteAppService
    {
        private readonly IClienteRepositories _clienteRepositories;

        public ClienteAppService(IClienteRepositories clienteRepositories)
        {
            _clienteRepositories = clienteRepositories;
        }

        public ClienteViewModel ObterPorId(int id)
        {
            return Mapper.Map<Cliente, ClienteViewModel>(_clienteRepositories.ObterPorId(id));
        }

        public IEnumerable<ClienteViewModel> ObterPorNome(string nome)
        {
            return
                Mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteViewModel>>(_clienteRepositories.ObterPorNome(nome));
        }

        public IEnumerable<ClienteViewModel> ObterBoqueados()
        {
            return
                Mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteViewModel>>(_clienteRepositories.ObterBoqueados());
        }
    }
}