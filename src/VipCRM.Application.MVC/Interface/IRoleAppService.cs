using System.Collections.Generic;
using VipCRM.Application.MVC.ViewModels;

namespace VipCRM.Application.MVC.Interface
{
    public interface IRoleAppService
    {
        RoleViewModel ObterPorNome(int usuarioId, string roleName);
        IEnumerable<RoleViewModel> ObterPorUsuario(int usuarioId); 
    }
}