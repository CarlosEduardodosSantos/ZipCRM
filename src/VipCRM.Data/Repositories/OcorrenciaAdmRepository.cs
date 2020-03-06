using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using VipCRM.Core.Domain.Entites;
using VipCRM.Data.Interface.Repositories;

namespace VipCRM.Data.Repositories
{
    public class OcorrenciaAdmRepository : RepositoryBase, IOcorrenciaAdmRepository
    {
        public IEnumerable<OcorrenciaMonitor> ObterOcorrenciaMonitor()
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var ocorrencias = cn.Query<OcorrenciaMonitor>(GetSelectBasic());
                cn.Close();

                return ocorrencias;
            }
        }

        public IEnumerable<OcorrenciaMonitor> ObterOcorrenciaMonitorPendentes()
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var ocorrencias = cn.Query<OcorrenciaMonitor>("Select * From vw_dash_monitor_OcorrenciaPendentes");
                cn.Close();

                return ocorrencias;
            }
        }

        public IEnumerable<OcorrenciaMonitor> ObterOcorrenciaMonitorPendentes30()
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var ocorrencias = cn.Query<OcorrenciaMonitor>("Select * From vw_dash_monitor_OcorrenciaPendentesMes");
                cn.Close();

                return ocorrencias;
            }
        }
        //
        public IEnumerable<OcorrenciaMonitor> ObterOcorrenciaMonitorPendentes30Anterior()
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var ocorrencias = cn.Query<OcorrenciaMonitor>("Select * From vw_dash_monitor_OcorrenciaPendentesMesAnterior");
                cn.Close();

                return ocorrencias;
            }
        }
        public IEnumerable<OcorrenciaMonitor> ObterOcorrenciaMonitorConcluido()
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var ocorrencias = cn.Query<OcorrenciaMonitor>("Select * From vw_dash_monitor_OcorrenciaConcluidas");
                cn.Close();

                return ocorrencias;
            }
        }

        public IEnumerable<OcorrenciaMonitor> ObterOcorrenciaMonitorConcluido30()
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var ocorrencias = cn.Query<OcorrenciaMonitor>("Select * From vw_dash_monitor_OcorrenciaConcluidasMes order by Qtdetotal desc");
                cn.Close();

                return ocorrencias;
            }
        }
        //
        public IEnumerable<OcorrenciaMonitor> ObterOcorrenciaMonitorConcluido30Anterior()
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var ocorrencias = cn.Query<OcorrenciaMonitor>("Select * From vw_dash_monitor_OcorrenciaConcluidasMesAnterior");
                cn.Close();

                return ocorrencias;
            }
        }
        public IEnumerable<SabadosMonitor> ObterEscalaSabados()
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var sabados = cn.Query<SabadosMonitor>("select * from VW_SABADOS_MES");
                cn.Close();

                return sabados;
            }
        }
        
        public IEnumerable<OcorrenciaMonitor> ObterOcorrenciasDias()
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var ocorrencias = cn.Query<OcorrenciaMonitor>("select * from VW_Dash_Monitor_Ocorrencias_Abertura");
                cn.Close();

                return ocorrencias;
            }
        }

        public OcorrenciaTotalizador OcorrenciaTotalizador()
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var ocorrencias = cn.Query<OcorrenciaTotalizador>("select * from vw_dash_OcorrenciaTotalizador").FirstOrDefault();
                cn.Close();

                return ocorrencias;
            }
        }

        public IEnumerable<Localizacao> ObterLocalizacaoDiaria()
        {
            var sql = new StringBuilder();
            sql.AppendLine("select");
            sql.AppendLine("Nome,");
            sql.AppendLine("OcorrenciaId = (Select Top 1 OcorrenciaId From Location Where UsuarioId = Usuarios.Codigo Order By data_hora desc),");
            sql.AppendLine("DataHora = (Select Top 1 data_hora From Location Where UsuarioId = Usuarios.Codigo Order By data_hora desc),");
            sql.AppendLine("Latitude = (Select Top 1 Latitude From Location Where UsuarioId = Usuarios.Codigo Order By data_hora desc),");
            sql.AppendLine("Longitude = (Select Top 1 Longitude From Location Where UsuarioId = Usuarios.Codigo Order By data_hora desc),");
            sql.AppendLine("TipoOperacao = (Select Top 1 TipoOperacao From Location Where UsuarioId = Usuarios.Codigo Order By data_hora desc),");
            sql.AppendLine("Endereco = (Select Top 1 Localizacao From Location Where UsuarioId = Usuarios.Codigo Order By data_hora desc)");
            sql.AppendLine("From Usuarios");
            sql.AppendLine("Inner Join Location On Location.UsuarioId = Usuarios.Codigo");
            sql.AppendLine("Group By Nome, Codigo");

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var localizacoes = cn.Query<Localizacao>(sql.ToString());
                cn.Close();

                return localizacoes;
            }

        }

        public IEnumerable<Localizacao> ObterLocalizacaoUsuarioId(string usuarioId)
        {
            var sql = new StringBuilder();
            sql.AppendLine("select");
            sql.AppendLine("OcorrenciaId,");
            sql.AppendLine("data_hora as DataHora,");
            sql.AppendLine("Nome,");
            sql.AppendLine("Latitude,");
            sql.AppendLine("Longitude");
            sql.AppendLine("from Location");
            sql.AppendLine("Inner Join Usuarios On Location.UsuarioId = Usuarios.Codigo");
            sql.AppendLine("Where Isnull(Latitude,'') <> '' And UsuarioId = @usuarioId");

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var localizacoes = cn.Query<Localizacao>(sql.ToString(), new { usuarioId });
                cn.Close();

                return localizacoes;
            }
        }

        public override string GetSelectBasic()
        {
            return "SELECT * FROM vw_dash_monitor_Ocorrencia";
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