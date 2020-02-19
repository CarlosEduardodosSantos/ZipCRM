using System.Collections.Generic;
using VipCRM.Application.MVC.ViewModels;

namespace VipCRM.Application.MVC.Interface
{
    public interface IUsuarioAppService
    {
        UsuarioViewModel ObterUsuarioPorId(int id);
        UsuarioViewModel AutenticaUsuario(string nome, string senha);
        IEnumerable<UsuarioViewModel> ObterPesquisaUsuarios(string pesquisa);
        void ModificaUsuario(UsuarioImagemViewModel usuarioImagemView);
        UsuarioImagemViewModel ObterImagemUduarioId(string usuarioId);
        IEnumerable<UsuarioViewModel> ObterUsuarios();
    }
}