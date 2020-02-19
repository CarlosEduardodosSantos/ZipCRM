using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using VipCRM.Core.Domain.Entites;
using VipCRM.Data.Interface.Repositories;

namespace VipCRM.Data.Repositories
{
    public class ClienteRepositories : RepositoryBase, IClienteRepositories
    {

        public Cliente ObterPorId(int id)
        {
            var sql = new StringBuilder().Append(GetSelectBasic());
            sql.Append(" Where Codigo = @sid");
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var cliente = cn.Query<Cliente>(sql.ToString(), new { sid = id }).FirstOrDefault();
                cn.Close();
                return cliente;

            }
        }

        public IEnumerable<Cliente> ObterPorNome(string nome)
        {
            var sql = new StringBuilder().Append(GetSelectBasic());
            sql.Append(" Where nome like @snome ");
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var clientes = cn.Query<Cliente>(sql.ToString(), new { snome = "%"+nome+"%" });
                cn.Close();

                return clientes;

            }
        }

        public IEnumerable<Cliente> ObterBoqueados()
        {
            var sql = new StringBuilder().Append(GetSelectBasic());
            sql.Append(" Where Sit_Contrato = 'B'");
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var clientes = cn.Query<Cliente>(sql.ToString());
                cn.Close();

                return clientes;

            }
        }

        public override string GetSelectBasic()
        {
            return @"Select 
	                    Codigo as ClienteId,
	                    CNPJ as Cnpj,
	                    Nome as Nome,
	                    Isnull(Razão,'') as RazaoSocial,
	                    Isnull(Cep,'') as Cep,
	                    Endereço as Endereco,
	                    Isnull(End_num,'') as Numero,
	                    Isnull(Bairro,'') as Bairro,
	                    Isnull(Cidade,'') as Cidade,
	                    Estado as Uf
                    From Clientes";
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