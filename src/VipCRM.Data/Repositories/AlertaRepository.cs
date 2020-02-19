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
    public class AlertaRepository : RepositoryBase, IAlertaRepository
    {
        private readonly IUsuarioRepositories _usuarioRepositories;

        public AlertaRepository(IUsuarioRepositories usuarioRepositories)
        {
            _usuarioRepositories = usuarioRepositories;
        }

        public ValidationResult Adicionar(Alerta alerta)
        {
            var param = new DynamicParameters();
            param.Add("@UsuarioId", alerta.UsuarioId);
            param.Add("@Situacao", alerta.Situacao);
            param.Add("@DataHora", alerta.DataHora);
            param.Add("@DhConfirm", DateTime.MinValue);
            param.Add("@Subject", alerta.Subject);

            using (IDbConnection cn = Connection)
            {
                if (!alerta.IsValid())
                    return alerta.ResultadoValidacao;

                cn.Open();
                cn.Query(GetInsertBasic(), param);
                cn.Close();

                return alerta.ResultadoValidacao;
            }

        }

        public ValidationResult ConfirmarLeitura(Alerta alerta)
        {
            var sql = new StringBuilder().Append("Update Alertas Set DhConfirm = @DhConfirm");
            using (IDbConnection cn = Connection)
            {
                if (!alerta.IsValid())
                    return alerta.ResultadoValidacao;
                
                cn.Open();
                cn.Query(sql.ToString());
                cn.Close();

                return alerta.ResultadoValidacao;
            }
        }

        public Alerta ObterPorId(Guid id)
        {
            var sql = new StringBuilder().Append(GetSelectBasic());
            sql.Append(" Where AlertaId = @sid");

            sql.Append(" Order By DataHora desc");

            using (IDbConnection cn = Connection)
            {
                cn.Open();

                var alerta = cn.Query<Alerta>(sql.ToString(), new { sid = id }).FirstOrDefault();

                cn.Close();

                if (alerta != null)
                    alerta.Usuario = _usuarioRepositories.ObterUsuarioPorId(alerta.UsuarioId);

                return alerta;
            }
        }

        public IEnumerable<Alerta> ObterPendentes()
        {
            var sql = new StringBuilder().Append(GetSelectBasic());
            sql.Append(" Where Situacao = 0");

            sql.Append(" Order By DataHora desc");

            using (IDbConnection cn = Connection)
            {
                cn.Open();

                var alertas = cn.Query<Alerta>(sql.ToString());

                cn.Close();

                foreach (var alerta in alertas)
                {
                    alerta.Usuario = _usuarioRepositories.ObterUsuarioPorId(alerta.UsuarioId);
                }   

                return alertas;
            }
        }

        public IEnumerable<Alerta> ObterPorUsuarioId(int usuarioId)
        {
            var sql = new StringBuilder().Append(GetSelectBasic());
            sql.Append(" Where UsuarioId = @susuarioId");

            sql.Append(" Order By DataHora desc");

            using (IDbConnection cn = Connection)
            {
                cn.Open();

                var alertas = cn.Query<Alerta>(sql.ToString(), new { susuarioId = usuarioId });

                cn.Close();

                foreach (var alerta in alertas)
                {
                    alerta.Usuario = _usuarioRepositories.ObterUsuarioPorId(alerta.UsuarioId);
                }

                return alertas;
            }
        }

        public override string GetSelectBasic()
        {
            return "Select * From Alertas";
        }

        public override string GetUpdateBasic()
        {
            throw new System.NotImplementedException();
        }

        public override string GetInsertBasic()
        {
            return @"Insert Into Alertas(UsuarioId, Situacao, DataHora, DhConfirm, Subject) 
                        Values (@UsuarioId, @Situacao, @DataHora, @DhConfirm, @Subject)";
        }
    }
}