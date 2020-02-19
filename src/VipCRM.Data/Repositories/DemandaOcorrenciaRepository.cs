using System.Collections.Generic;
using System.Data;
using Dapper;
using VipCRM.Core.Domain.Entites;
using VipCRM.Data.Interface.Repositories;

namespace VipCRM.Data.Repositories
{
    public class DemandaOcorrenciaRepository : RepositoryBase, IDemandaOcorrenciaRepository
    {
        public IEnumerable<DemandaOcorrencia> ObterTodos()
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var ocorrencias = cn.Query<DemandaOcorrencia>(GetSelectBasic());
                cn.Close();

                return ocorrencias;
            }
        }
        public override string GetSelectBasic()
        {
            return "Select * From vw_dash_demandaOcorrenciaGeral7Dias";
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