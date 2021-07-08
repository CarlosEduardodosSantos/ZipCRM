using System.Linq;
using System.Text;
using Dapper;
using VipCRM.Core.Domain.Entites;
using VipCRM.Data.Interface.Repositories;

namespace VipCRM.Data.Repositories
{
    public class EmpresaRepository : RepositoryBase, IEmpresaRepository
    {
        public override string GetSelectBasic()
        {
            var sql = new StringBuilder();
            sql.AppendLine("Select SIEMP_LOJA as EmpresaId,");
            sql.AppendLine("SIEMP_NOMEFANTASIA as NomeFantasia,");
            sql.AppendLine("SIEMP_LOGRADOURO as Logradouro,");
            sql.AppendLine("SIEMP_NUMERO as Numero,");
            sql.AppendLine("SIEMP_COMPLEMENTO as Complemento,");
            sql.AppendLine("SIEMP_BAIRRO as Bairro,");
            sql.AppendLine("SIEMP_MUNICIPIO as Cidade,");
            sql.AppendLine("SIEMP_CEP as Cep,");
            sql.AppendLine("SIEMP_UF as Uf,");
            sql.AppendLine("SIEMP_FONE as Fone");
            sql.AppendLine("from SIEMP");

            return sql.ToString();
        }

        public override string GetUpdateBasic()
        {
            throw new System.NotImplementedException();
        }

        public override string GetInsertBasic()
        {
            throw new System.NotImplementedException();
        }

        public Empresa GetById(string id)
        {
            var sql = $"{GetSelectBasic()} Where SIEMP_LOJA = @id";

            using (var conn = Connection)
            {
                conn.Open();
                var empresa = conn.Query<Empresa>(sql, new {id}).FirstOrDefault();
                conn.Close();
                return empresa;
            }
        }
    }
}