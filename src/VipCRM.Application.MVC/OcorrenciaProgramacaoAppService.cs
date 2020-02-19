using System.Collections.Generic;
using AutoMapper;
using VipCRM.Application.MVC.Interface;
using VipCRM.Application.MVC.ViewModels;
using VipCRM.Core.Domain.Entites;
using VipCRM.Data.Interface.Repositories;

namespace VipCRM.Application.MVC
{
    public class OcorrenciaProgramacaoAppService : IOcorrenciaProgramacaoAppService
    {
        private readonly IOcorrenciaProgramacaoRepository _programacaoRepository;

        public OcorrenciaProgramacaoAppService(IOcorrenciaProgramacaoRepository programacaoRepository)
        {
            _programacaoRepository = programacaoRepository;
        }

        public IEnumerable<ResultOcorrenciaProgramacaoViewModel> OcorrenciasPorSoftware()
        {
            return
                Mapper.Map<IEnumerable<ResultOcorrenciaProgramacao>, IEnumerable<ResultOcorrenciaProgramacaoViewModel>>(
                    _programacaoRepository.OcorrenciasPorSoftware());
        }

        public IEnumerable<ResultOcorrenciaProgramacaoViewModel> OcorrenciasPorCliente()
        {
            return
                Mapper.Map<IEnumerable<ResultOcorrenciaProgramacao>, IEnumerable<ResultOcorrenciaProgramacaoViewModel>>(
                    _programacaoRepository.OcorrenciasPorCliente());
        }

        public IEnumerable<ResultOcorrenciaProgramacaoViewModel> OcorrenciasPorProgramador()
        {
            return
                Mapper.Map<IEnumerable<ResultOcorrenciaProgramacao>, IEnumerable<ResultOcorrenciaProgramacaoViewModel>>(
                    _programacaoRepository.OcorrenciasPorProgramador());
        }

        public IEnumerable<OcorrenciaProgramacaoViewModel> OcorrenciaProgramadores(int softwareId, int clienteId, int programadorId)
        {
            return
                Mapper.Map<IEnumerable<OcorrenciaProgramacao>, IEnumerable<OcorrenciaProgramacaoViewModel>>(
                    _programacaoRepository.OcorrenciaProgramadores(softwareId, clienteId, programadorId));
        }

        public IEnumerable<OcorrenciaProgramacaoViewModel> ObterPorUsuario(string usuarioId)
        {
            return
                Mapper.Map<IEnumerable<OcorrenciaProgramacao>, IEnumerable<OcorrenciaProgramacaoViewModel>>(
                    _programacaoRepository.ObterPorUsuario(usuarioId));
        }
    }
}