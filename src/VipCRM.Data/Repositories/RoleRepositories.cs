using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using VipCRM.Core.Domain.Entites;
using VipCRM.Data.Interface.Repositories;

namespace VipCRM.Data.Repositories
{
    public class RoleRepositories : RepositoryBase, IRoleRepositories
    {

        public Role ObterPorNome(int usuarioId, string roleName)
        {
            var sql = new StringBuilder().Append(GetSelectBasic());
            sql.Append(" Where iduser = @susuarioId");
            sql.Append(" And RoleName = @roleName");
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var role = cn.Query<Role>(sql.ToString(), new { sroleName = roleName, susuarioId = usuarioId }).FirstOrDefault();
                cn.Close();
                return role;
            }
        }

        public IEnumerable<Role> ObterPorUsuario(int usuarioId)
        {
            var sql = new StringBuilder().Append(GetSelectBasic());
            sql.Append(" Where iduser = @susuarioId");
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var role = cn.Query<Role>(sql.ToString(), new { susuarioId = usuarioId });
                cn.Close();

                return role;
            }
        }

        public override string GetSelectBasic()
        {
            return @"Select 
	                    iduser as UsuarioId,
	                    componente as RoleName,
	                    modulo as RoleDescription
                    From usuarios_direitos Roles ";
        }

        public override string GetUpdateBasic()
        {
            throw new System.NotImplementedException();
        }

        public override string GetInsertBasic()
        {
            throw new System.NotImplementedException();
        }
    }
}