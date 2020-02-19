using System;
using System.Collections.Generic;
using AutoMapper;
using VipCRM.Application.MVC.Interface;
using VipCRM.Application.MVC.ViewModels;
using VipCRM.Core.Domain.Entites;
using VipCRM.Data.Repositories;

namespace VipCRM.Application.MVC
{
    public class OrcamentoDashBoardAppService: IOrcamentoDashBoardAppService
    {
        private readonly OrcamentoDashBoardRepository _boardRepository;

        public OrcamentoDashBoardAppService(OrcamentoDashBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        public IEnumerable<OrcamentoRankingViewModel> OrcamentoRankingPorData(DateTime dataInicio, DateTime dataFinal)
        {
            return Mapper.Map< IEnumerable < OrcamentoRanking >, IEnumerable <OrcamentoRankingViewModel>>(
                _boardRepository.OrcamentoRankingPorData(dataInicio, dataFinal));
        }

        public IEnumerable<OrcamentoEquipeViewModel> RankingEquipePorData(DateTime dataInicio, DateTime dataFinal)
        {
            return Mapper.Map<IEnumerable<OrcamentoEquipe>, IEnumerable<OrcamentoEquipeViewModel>>(
                _boardRepository.RankingEquipePorData(dataInicio, dataFinal));
        }

        public IEnumerable<OrcamentoViewModel> UltimosOrcamentos(DateTime dataInicio, DateTime dataFinal, int page, int limit)
        {
            return Mapper.Map<IEnumerable<Orcamento>, IEnumerable<OrcamentoViewModel>>(
                _boardRepository.UltimosOrcamentos(dataInicio, dataFinal, page, limit));
        }

        public OrcamentoTotalizarViewModel TotalizarPorData(DateTime dataInicio, DateTime dataFinal)
        {
            return Mapper.Map<OrcamentoTotalizar, OrcamentoTotalizarViewModel> (
                _boardRepository.TotalizarPorData(dataInicio, dataFinal));
        }
    }
}