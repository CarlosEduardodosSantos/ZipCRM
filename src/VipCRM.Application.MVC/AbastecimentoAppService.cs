using System.Collections.Generic;
using AutoMapper;
using VipCRM.Application.MVC.Interface;
using VipCRM.Application.MVC.ViewModels;
using VipCRM.Core.Domain.Entites;
using VipCRM.Data.Interface.Repositories;

namespace VipCRM.Application.MVC
{
    public class AbastecimentoAppService : IAbastecimentoAppService
    {
        private readonly IAbastecimentoRepository _abastecimentoRepository;

        public AbastecimentoAppService(IAbastecimentoRepository abastecimentoRepository)
        {
            _abastecimentoRepository = abastecimentoRepository;
        }
        public AbastecimentoViewModel ObterPorId(int abastecimentoId)
        {
            return Mapper.Map<Abastecimento, AbastecimentoViewModel>(_abastecimentoRepository.ObterPorId(abastecimentoId));
        }

        public IEnumerable<AbastecimentoViewModel> ObterPorUsuarioId(int usuarioId)
        {
            return Mapper.Map<IEnumerable<Abastecimento>, IEnumerable<AbastecimentoViewModel>>(_abastecimentoRepository.ObterPorUsuarioId(usuarioId));
        }

        public void Alterar(AbastecimentoViewModel abastecimentoView)
        {
            var abastecimento = Mapper.Map<AbastecimentoViewModel, Abastecimento>(abastecimentoView);
            _abastecimentoRepository.Alterar(abastecimento);
        }
    }
}