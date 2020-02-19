using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using VipCRM.Core.Domain.Entites;
using VipCRM.Data.Interface.Repositories;

namespace VipCRM.Data.Repositories
{
    public class UsuarioRepositories : RepositoryBase, IUsuarioRepositories
    {

        public Usuario ObterUsuarioPorId(int id)
        {
            var sql = new StringBuilder().Append(GetSelectBasic());
            sql.Append(" Where Codigo = @sid");
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var usuario = cn.Query<Usuario>(sql.ToString(), new { sid = id }).FirstOrDefault();
                cn.Close();
                return usuario;

            }
        }

        public Usuario AutenticaUsuario(string nome, string senha)
        {
            var sql = new StringBuilder().Append(GetSelectBasic());
            sql.Append(" Where nome = @snome And Senha = @ssenha");
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var usuario = cn.Query<Usuario>(sql.ToString(), new { snome = nome, ssenha = senha }).FirstOrDefault();
                cn.Close();

                return usuario;

            }
        }

        public IEnumerable<Usuario> ObterPesquisaUsuarios(string pesquisa)
        {
            var sql = new StringBuilder().Append(GetSelectBasic());
            sql.Append(" Where nome like @snome ");
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var usuarios = cn.Query<Usuario>(sql.ToString(), new { snome = pesquisa });
                cn.Close();

                return usuarios;

            }
        }

        public void ModificaUsuario(UsuarioImagem usuarioImagem)
        {
            var sql = new StringBuilder();
            sql.AppendLine("If exists (Select 1 From usuarios_imagens Where UsuarioId = @UsuarioId) Begin");
            sql.AppendLine("Update usuarios_imagens Set");
            sql.AppendLine("Imagem = @Imagem,");
            sql.AppendLine("Assinatura = @Assinatura");
            sql.AppendLine("Where UsuarioId = @UsuarioId End");
            sql.AppendLine("Else Begin");
            sql.AppendLine("Insert Into usuarios_imagens(UsuarioId, Imagem, Assinatura) Values");
            sql.AppendLine("(@UsuarioId, @Imagem, @Assinatura) End");

            var parms = new DynamicParameters();
            parms.Add("@UsuarioId", usuarioImagem.UsuarioId);
            parms.Add("@Imagem", usuarioImagem.Imagem);
            parms.Add("@Assinatura", usuarioImagem.Assinatura);

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                cn.Query(sql.ToString(), parms);
                cn.Close();

            }

        }

        public UsuarioImagem ObterImagemUduarioId(string usuarioId)
        {
            var sql = "Select * from usuarios_imagens Where UsuarioId = @usuarioId";
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var imagem = cn.Query<UsuarioImagem>(sql, new {usuarioId}).FirstOrDefault();
                cn.Close();

                return imagem;
            }
        }

        public IEnumerable<Usuario> ObterUsuarios()
        {
            var sql = "select * from usuarios Where Ativo = 0 And Perfil > 0";
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var usuarios = cn.Query<Usuario>(sql);
                cn.Close();

                return usuarios;
            }
        }

        public override string GetSelectBasic()
        {
            return @"select 
	                Codigo as UsuarioId,
	                Nome_Completo as Nome,
	                Nome as [User],
	                Senha as [Password],
                    perfil as PerfilId
                    from Usuarios ";
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