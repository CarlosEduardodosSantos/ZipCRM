using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using VipCRM.Core.Domain.Entites;
using VipCRM.Core.Domain.ValueObjects;
using VipCRM.Data.Interface.Repositories;

namespace VipCRM.Data.Repositories
{
    public class AgendaRepositories : RepositoryBase, IAgendaRepositories
    {

        public Agenda ObterPorId(int id)
        {
            var sql = new StringBuilder().Append(GetSelectBasic());
            sql.Append(" Where nro = @sid");

            sql.Append(" Order By Data_Age, Hora_Age");

            using (IDbConnection cn = Connection)
            {
                cn.Open();

                var agendamento = cn.Query<Agenda>(sql.ToString(), new { sid = id }).FirstOrDefault();

                cn.Close();

                return agendamento;
            }
        }

        public IEnumerable<Agenda> ObterPorDataAgendamento(int usuarioId, DateTime data)
        {
            var sql = new StringBuilder().Append(GetSelectBasic());

            sql.Append(" Where Usuario_Age = @susuarioid");
            sql.Append(" AndData_Age = @sdata");

            sql.Append(" Order By Data_Age, Hora_Age");

            using (IDbConnection cn = Connection)
            {
                cn.Open();

                var agendamento = cn.Query<Agenda>(sql.ToString(), new {sdata = data.Date, susuarioid = usuarioId});

                cn.Close();

                return agendamento;
            }
        }

        public IEnumerable<Agenda> ObterPorDatainicioFim(int usuarioId, DateTime dataInicio, DateTime dataFim)
        {
            var sql = new StringBuilder().Append(GetSelectBasic());

            sql.Append(" Where Usuario_Age = @susuarioid");
            sql.Append(" And Data_Age between @sdataInicio And @sdataFim");

            sql.Append(" Order By Data_Age, Hora_Age");

            using (IDbConnection cn = Connection)
            {
                cn.Open();

                var agendamento = cn.Query<Agenda>(sql.ToString(), new { sdataInicio = dataInicio.Date, sdataFim = dataFim.Date, susuarioid = usuarioId });

                cn.Close();

                return agendamento;
            }
        }

        public IEnumerable<Agenda> ObterPendentes(int usuarioId)
        {
            var sql = new StringBuilder().Append(GetSelectBasic());

            sql.Append(" Where Usuario_Age = @susuarioid");
            sql.Append(" And Data_OK is null");

            sql.Append(" Order By Data_Age, Hora_Age");

            using (IDbConnection cn = Connection)
            {
                cn.Open();

                var agendamento = cn.Query<Agenda>(sql.ToString(), new { susuarioid = usuarioId });

                cn.Close();

                return agendamento;
            }

        }

        public IEnumerable<Agenda> ObterPorPrioridade(int usuarioId, int prioridade)
        {
            var sql = new StringBuilder().Append(GetSelectBasic());
            sql.Append(" Where Usuario_Age = @susuarioid");
            sql.Append(" And Prioridade = @sprioridade");
            sql.Append(" And Data_OK is null");

            sql.Append(" Order By Data_Age, Hora_Age");

            using (IDbConnection cn = Connection)
            {
                cn.Open();

                var agendamento = cn.Query<Agenda>(sql.ToString(), new { sprioridade = prioridade, susuarioid = usuarioId });

                cn.Close();

                return agendamento;
            }

        }

        public ValidationResult Conclusao(int id, int usuarioId, string conclusao, DateTime dataHora)
        {
            var param = new DynamicParameters();
            param.Add("@sid", id);
            param.Add("@susuarioid", usuarioId);
            param.Add("@sconclusao", conclusao);
            param.Add("@sdata", dataHora.Date);
            param.Add("@shora", dataHora.ToShortTimeString());

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                cn.Execute(GetUpdateBasic(), param, commandType: CommandType.Text);
                cn.Close();

                return new ValidationResult();
            }

        }

        public ValidationResult CriarAgendamento(Agenda model)
        {
            var param = new DynamicParameters();
            param.Add("@sData", model.DataHoraAgedado.Date);
            param.Add("@sHora", model.DataHoraAgedado.ToShortTimeString());
            param.Add("@sUsuarioId", model.UsuarioId);
            param.Add("@sClienteId", model.ClienteId);
            param.Add("@sAcao", model.Acao);
            param.Add("@sAcaoDescricao", model.AcaoDescricao);
            param.Add("@sCadUduarioId", model.UsuarioId);
            param.Add("@sPrioridade", model.Prioridade);

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                cn.Execute(GetInsertBasic(), param, commandType: CommandType.Text);
                cn.Close();

                return new ValidationResult();
            }
        }

        public override string GetSelectBasic()
        {
            return @"select 
	                    nro as AgendaId,
	                    Usuario_Age as UsuarioId,
	                    Cliente as ClienteId,
	                    Clientes.Nome as ClienteNome,
	                    Cast(CONVERT(VARCHAR(10),Data_Age,121) + ' '+ Hora_Age as DateTime) as DataHoraAgedado,
	                    Cast(CONVERT(VARCHAR(10),Data_OK,121) + ' '+ Hora_Ok as DateTime) as DataHoraConclusao,
	                    Acao as Acao,
	                    Obs_Age as AcaoDescricao,
                        Obs_Ok as Conclusao,
	                    Prioridade
                    from Tele
                    Inner Join CLientes On Tele.Cliente = Clientes.Codigo";
        }

        public override string GetUpdateBasic()
        {
            return @"Update Tele Set 
	                    Usuario_Ok = @susuarioid,
	                    Obs_Ok = @sconclusao,
	                    Data_OK = @sdata,
	                    Hora_Ok = @shora
                    Where nro = @sid";
        }

        public override string GetInsertBasic()
        {
            return
                @"Insert Into Tele (Data_Age, Hora_Age, Usuario_Age, Cliente, Acao, Obs_Age, Usuario_Cadastro, Prioridade)" +
                "Values (@sData, @sHora, @sUsuarioId, @sClienteId, @sAcao, @sAcaoDescricao, @sCadUduarioId, @sPrioridade)";
        }
    }
}