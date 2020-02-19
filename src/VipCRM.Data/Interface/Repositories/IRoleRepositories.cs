using System.Collections.Generic;
using VipCRM.Core.Domain.Entites;

namespace VipCRM.Data.Interface.Repositories
{
    public interface IRoleRepositories
    {
        Role ObterPorNome(int usuarioId, string roleName);
        IEnumerable<Role> ObterPorUsuario(int usuarioId);
    }
}