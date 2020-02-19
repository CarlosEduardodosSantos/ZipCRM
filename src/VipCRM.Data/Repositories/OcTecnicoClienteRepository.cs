using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using VipCRM.Core.Domain.Entites;
using VipCRM.Data.Interface.Repositories;

namespace VipCRM.Data.Repositories
{
    public class OcTecnicoClienteRepository : RepositoryBase, IOcTecnicoClienteRepository
    {
        public IEnumerable<OcTecnicoCliente> ObterOcottenciaTecnicoClientes()
        {
            using (IDbConnection cn = Connection)
            {
                var sql = "select UsuarioId, UsuarioNome, Imagem from vw_dash_OcorrenciaAtendimentoCliente Group By UsuarioId, UsuarioNome, Imagem ";
                sql += GetSelectBasic();

                cn.Open();

                using (var mult = cn.QueryMultiple(sql))
                {
                    var tecnicoCliente = mult.Read<OcTecnicoCliente>();
                    var tecnicoClienteItem = mult.Read<OcTecnicoClienteItem>();
                    
                    foreach (var ocTecnicoCliente in tecnicoCliente)
                    {
                        ocTecnicoCliente.TecnicoClienteItem =
                            tecnicoClienteItem.Where(w => w.UsuarioId == ocTecnicoCliente.UsuarioId);
                    }
                    cn.Close();

                    return tecnicoCliente;
                }
            }
        }

        public OcTecnicoCliente PendenciaPorTecnico(int usuarioId, string situacao)
        {
            using (IDbConnection cn = Connection)
            {
                var sql = "select UsuarioId, UsuarioNome, Imagem from vw_dash_OcorrenciaAtendimentoCliente " + 
                    " Where UsuarioId = @usuarioId Group By UsuarioId, UsuarioNome, Imagem ";
                sql += GetSelectBasic() +
                    " Where UsuarioId = @usuarioId And (@situacao is null Or Situacao like @situacao) " +
                    " Order By DataAgendamento, Veiculo ";

                cn.Open();

                using (var mult = cn.QueryMultiple(sql, new { usuarioId, situacao }))
                {
                    var tecnicoCliente = mult.Read<OcTecnicoCliente>().FirstOrDefault();
                    var tecnicoClienteItem = mult.Read<OcTecnicoClienteItem>();


                    tecnicoCliente.TecnicoClienteItem = tecnicoClienteItem;

                    cn.Close();

                    return tecnicoCliente;
                }
            }
        }

        public override string GetSelectBasic()
        {
            return "select * from vw_dash_OcorrenciaAtendimentoCliente";
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