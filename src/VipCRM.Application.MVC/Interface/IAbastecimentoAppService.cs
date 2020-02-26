using System.Collections.Generic;
using VipCRM.Application.MVC.ViewModels;

namespace VipCRM.Application.MVC.Interface
{
    public interface IAbastecimentoAppService
    {
        AbastecimentoViewModel ObterPorId(int abastecimentoId);
        IEnumerable<AbastecimentoViewModel> ObterPorUsuarioId(int usuarioId);
        void Alterar(AbastecimentoViewModel abastecimento);
    }
}