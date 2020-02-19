using System.Collections.Generic;
using AutoMapper;
using VipCRM.Application.MVC.Interface;
using VipCRM.Application.MVC.ViewModels;
using VipCRM.Core.Domain.Entites;
using VipCRM.Data.Interface.Repositories;

namespace VipCRM.Application.MVC
{
    public class RoleAppService : IRoleAppService
    {
        private readonly IRoleRepositories _roleRepositories;

        public RoleAppService(IRoleRepositories roleRepositories)
        {
            _roleRepositories = roleRepositories;
        }

        public RoleViewModel ObterPorNome(int usuarioId, string roleName)
        {
            return Mapper.Map<Role, RoleViewModel>(_roleRepositories.ObterPorNome(usuarioId, roleName));
        }

        public IEnumerable<RoleViewModel> ObterPorUsuario(int usuarioId)
        {
            return Mapper.Map<IEnumerable<Role>, IEnumerable<RoleViewModel>>(_roleRepositories.ObterPorUsuario(usuarioId));
        }
    }
}