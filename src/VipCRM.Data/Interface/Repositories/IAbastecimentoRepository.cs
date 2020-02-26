using System.Collections.Generic;
using VipCRM.Core.Domain.Entites;

namespace VipCRM.Data.Interface.Repositories
{
    public interface IAbastecimentoRepository
    {
        Abastecimento ObterPorId(int abastecimentoId);
        IEnumerable<Abastecimento> ObterPorUsuarioId(int usuarioId);
        void Alterar(Abastecimento abastecimento);
    }
}