using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using VipCRM.Application.MVC.Interface;

namespace VipCRM.Web.Infrastructure
{
    public class CustomRoleProvider : RoleProvider
    {
        private readonly IRoleAppService _roleAppService;
        private readonly IUsuarioAppService _usuarioAppService;

        public CustomRoleProvider()
        {
            _roleAppService = DependencyResolver.Current.GetService<IRoleAppService>();
            _usuarioAppService = DependencyResolver.Current.GetService<IUsuarioAppService>();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            var user = _usuarioAppService.ObterUsuarioPorId(int.Parse(username));
            if (user == null) return new string[]{};
            
            int usuarioId = (user.PerfilId != 0) ? user.PerfilId : user.UsuarioId;

            var roles = _roleAppService.ObterPorUsuario(usuarioId).ToList();

            var rolesArray = roles.Select(u => u.RoleName).ToArray();

            return rolesArray;

        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var user = _usuarioAppService.ObterPesquisaUsuarios(username).FirstOrDefault();
            if (user == null) return false;

            int usuarioId = (user.PerfilId != 0) ? user.PerfilId : user.UsuarioId;

            return _roleAppService.ObterPorNome(usuarioId, roleName) != null;

        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}