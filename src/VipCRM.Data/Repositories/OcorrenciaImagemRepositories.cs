using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using VipCRM.Core.Domain.Entites;
using VipCRM.Data.Interface.Repositories;

namespace VipCRM.Data.Repositories
{
    public class OcorrenciaImagemRepositories : RepositoryBase, IOcorrenciaImagemRepositories
    {

        public OcorrenciaImagem ObterPorId(int id)
        {
            var sql = new StringBuilder().Append(GetSelectBasic());
            sql.Append(" Where OcorrenciaImagemId = @sid");
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var imagem = cn.Query<OcorrenciaImagem>(sql.ToString(), new { sid = id }).FirstOrDefault();
                cn.Close();
                return imagem;
            }
        }

        public IEnumerable<OcorrenciaImagem> ObterPorOcorrencia(int ocorrenciaId)
        {
            var sql = new StringBuilder().Append(GetSelectBasic());
            sql.Append(" Where OcorrenciaId = @socorrenciaId");
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var imagens = cn.Query<OcorrenciaImagem>(sql.ToString(), new { socorrenciaId = ocorrenciaId });
                cn.Close();
                return imagens;
            }
        }

        public override string GetSelectBasic()
        {
            return @"Select * From OcorrenciaImagens";
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