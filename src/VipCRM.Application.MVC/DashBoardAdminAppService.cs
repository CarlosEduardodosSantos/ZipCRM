using System.Collections.Generic;
using AutoMapper;
using VipCRM.Application.MVC.Interface;
using VipCRM.Application.MVC.ViewModels;
using VipCRM.Core.Domain.Entites;
using VipCRM.Data.Interface.Repositories;

namespace VipCRM.Application.MVC
{
    public class DashBoardAdminAppService : IDashBoardAdminAppService
    {
        private readonly IOcorrenciaAdmRepository _admRepository;
        private readonly IOcTecnicoClienteRepository _ocTecnicoClienteRepository;
        private readonly IDemandaOcorrenciaRepository _demandaOcorrenciaRepository;

        public DashBoardAdminAppService(IOcorrenciaAdmRepository admRepository, 
            IOcTecnicoClienteRepository ocTecnicoClienteRepository, 
            IDemandaOcorrenciaRepository demandaOcorrenciaRepository)
        {
            _admRepository = admRepository;
            _ocTecnicoClienteRepository = ocTecnicoClienteRepository;
            _demandaOcorrenciaRepository = demandaOcorrenciaRepository;
        }

        public IEnumerable<OcorrenciaMonitorViewModel> RoteiroDia()
        {
            return
                Mapper.Map<IEnumerable<OcorrenciaMonitor>, IEnumerable<OcorrenciaMonitorViewModel>>(
                    _admRepository.ObterOcorrenciaMonitor());
        }

        public IEnumerable<OcorrenciaMonitorViewModel> RoteiroPendentes()
        {
            return
                Mapper.Map<IEnumerable<OcorrenciaMonitor>, IEnumerable<OcorrenciaMonitorViewModel>>(
                    _admRepository.ObterOcorrenciaMonitorPendentes());
        }

        public IEnumerable<OcorrenciaMonitorViewModel> RoteiroPendentes30()
        {
            return
                Mapper.Map<IEnumerable<OcorrenciaMonitor>, IEnumerable<OcorrenciaMonitorViewModel>>(
                    _admRepository.ObterOcorrenciaMonitorPendentes30());
        }

        public IEnumerable<OcorrenciaMonitorViewModel> RoteiroPendentes30Anterior()
        {
            return
                Mapper.Map<IEnumerable<OcorrenciaMonitor>, IEnumerable<OcorrenciaMonitorViewModel>>(
                    _admRepository.ObterOcorrenciaMonitorPendentes30Anterior());
        }

        public IEnumerable<OcorrenciaMonitorViewModel> RoteiroConcluido()
        {
            return
                Mapper.Map<IEnumerable<OcorrenciaMonitor>, IEnumerable<OcorrenciaMonitorViewModel>>(
                    _admRepository.ObterOcorrenciaMonitorConcluido());
        }

        public IEnumerable<OcorrenciaMonitorViewModel> RoteiroConcluido30()
        {
            return
                Mapper.Map<IEnumerable<OcorrenciaMonitor>, IEnumerable<OcorrenciaMonitorViewModel>>(
                    _admRepository.ObterOcorrenciaMonitorConcluido30());
        }

        public IEnumerable<OcorrenciaMonitorViewModel> RoteiroConcluido30Anterior()
        {
            return
                Mapper.Map<IEnumerable<OcorrenciaMonitor>, IEnumerable<OcorrenciaMonitorViewModel>>(
                    _admRepository.ObterOcorrenciaMonitorConcluido30Anterior());
        }

        public IEnumerable<OcorrenciaMonitorViewModel> ObterOcorrenciasDias()
        {
            return
                Mapper.Map<IEnumerable<OcorrenciaMonitor>, IEnumerable<OcorrenciaMonitorViewModel>>(
                    _admRepository.ObterOcorrenciasDias());
        }

        public IEnumerable<SabadosMonitorViewModel> EscalaSabados()
        {
            return
                Mapper.Map<IEnumerable<SabadosMonitor>, IEnumerable<SabadosMonitorViewModel>>(
                    _admRepository.ObterEscalaSabados());
        }

        public OcorrenciaTotalizadorViewModel Totalizador()
        {
            return
                Mapper.Map<OcorrenciaTotalizador, OcorrenciaTotalizadorViewModel>(_admRepository.OcorrenciaTotalizador());
        }

        public IEnumerable<OcTecnicoClienteViewModel> OcorrenciasPorTecnico()
        {
            return
                Mapper.Map<IEnumerable<OcTecnicoCliente>, IEnumerable<OcTecnicoClienteViewModel>>(
                    _ocTecnicoClienteRepository.ObterOcottenciaTecnicoClientes());
        }

        public IEnumerable<DemandaOcorrenciaViewModel> DemandaOcorrencia()
        {
            return Mapper.Map<IEnumerable<DemandaOcorrencia>, IEnumerable<DemandaOcorrenciaViewModel>>(
                _demandaOcorrenciaRepository.ObterTodos());
        }

        public OcTecnicoClienteViewModel PendenciaPorTecnico(int usuarioId, string situacao)
        {
            return
                Mapper.Map<OcTecnicoCliente, OcTecnicoClienteViewModel>(
                    _ocTecnicoClienteRepository.PendenciaPorTecnico(usuarioId, situacao));
        }

        public IEnumerable<LocalizacaoViewModel> ObterLocalizacaoDiaria()
        {
            return
                Mapper.Map<IEnumerable<Localizacao>, IEnumerable<LocalizacaoViewModel>>(
                    _admRepository.ObterLocalizacaoDiaria());
        }

        public IEnumerable<LocalizacaoViewModel> ObterLocalizacaoUsuarioId(string usuarioId)
        {
            return
                Mapper.Map<IEnumerable<Localizacao>, IEnumerable<LocalizacaoViewModel>>(
                    _admRepository.ObterLocalizacaoUsuarioId(usuarioId));
        }
    }
}