using System;
using System.Collections.Generic;
using VipCRM.Core.Domain.Entites;

namespace VipCRM.Data.Interface.Repositories
{
    public interface IOrcamentoDashBoardRepository
    {
        IEnumerable<OrcamentoRanking> OrcamentoRankingPorData(DateTime dataInicio, DateTime dataFinal);
        IEnumerable<OrcamentoEquipe> RankingEquipePorData(DateTime dataInicio, DateTime dataFinal);
        IEnumerable<Orcamento> UltimosOrcamentos(DateTime dataInicio, DateTime dataFinal, int page, int limit);
        OrcamentoTotalizar TotalizarPorData(DateTime dataInicio, DateTime dataFinal);
    }
}