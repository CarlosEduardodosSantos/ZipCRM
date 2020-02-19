using System.Collections.Generic;
using AutoMapper;
using VipCRM.Application.MVC.Interface;
using VipCRM.Application.MVC.ViewModels;
using VipCRM.Core.Domain.Entites;
using VipCRM.Core.Domain.Validation.Documentos;
using VipCRM.Data.Interface.Repositories;

namespace VipCRM.Application.MVC
{
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly IUsuarioRepositories _usuarioRepositories;

        public UsuarioAppService(IUsuarioRepositories usuarioRepositories)
        {
            _usuarioRepositories = usuarioRepositories;
        }
        public ViewModels.UsuarioViewModel ObterUsuarioPorId(int id)
        {

            return Mapper.Map<Usuario, UsuarioViewModel>(_usuarioRepositories.ObterUsuarioPorId(id));
        }

        public ViewModels.UsuarioViewModel AutenticaUsuario(string nome, string senha)
        {
            return Mapper.Map<Usuario, UsuarioViewModel>(_usuarioRepositories.AutenticaUsuario(nome, ConvertMd5.GetMd5Hash(senha)));
        }

        public IEnumerable<UsuarioViewModel> ObterPesquisaUsuarios(string pesquisa)
        {

            return Mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioViewModel>>(
                _usuarioRepositories.ObterPesquisaUsuarios(pesquisa));
        }

        public void ModificaUsuario(UsuarioImagemViewModel usuarioImagemView)
        {
            var usuario = Mapper.Map<UsuarioImagemViewModel, UsuarioImagem>(usuarioImagemView);
            _usuarioRepositories.ModificaUsuario(usuario);
        }

        public UsuarioImagemViewModel ObterImagemUduarioId(string usuarioId)
        {
            return Mapper.Map<UsuarioImagem, UsuarioImagemViewModel>(_usuarioRepositories.ObterImagemUduarioId(usuarioId));
        }

        public IEnumerable<UsuarioViewModel> ObterUsuarios()
        {
            return Mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioViewModel>>(
                _usuarioRepositories.ObterUsuarios());
        }
    }
}