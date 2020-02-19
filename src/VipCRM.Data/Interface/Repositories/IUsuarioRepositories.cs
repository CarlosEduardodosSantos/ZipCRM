using System.Collections.Generic;
using VipCRM.Core.Domain.Entites;

namespace VipCRM.Data.Interface.Repositories
{
    public interface IUsuarioRepositories
    {
        Usuario ObterUsuarioPorId(int id);
        Usuario AutenticaUsuario(string nome, string senha);
        IEnumerable<Usuario> ObterPesquisaUsuarios(string pesquisa);
        void ModificaUsuario(UsuarioImagem usuarioImagem);
        UsuarioImagem ObterImagemUduarioId(string usuarioId);
        IEnumerable<Usuario> ObterUsuarios();
    }
}