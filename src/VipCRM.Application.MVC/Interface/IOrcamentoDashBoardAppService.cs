using System;
using System.Collections.Generic;
using VipCRM.Application.MVC.ViewModels;

namespace VipCRM.Application.MVC.Interface
{
    public interface IOrcamentoDashBoardAppService
    {
        IEnumerable<OrcamentoRankingViewModel> OrcamentoRankingPorData(DateTime dataInicio, DateTime dataFinal);
        IEnumerable<OrcamentoEquipeViewModel> RankingEquipePorData(DateTime dataInicio, DateTime dataFinal);
        IEnumerable<OrcamentoViewModel> UltimosOrcamentos(DateTime dataInicio, DateTime dataFinal, int page, int limit);
        OrcamentoTotalizarViewModel TotalizarPorData(DateTime dataInicio, DateTime dataFinal);
    }
}