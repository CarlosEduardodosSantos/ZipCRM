using System.Collections.Generic;
using AutoMapper;
using VipCRM.Application.MVC.Interface;
using VipCRM.Application.MVC.ViewModels;
using VipCRM.Core.Domain.Entites;
using VipCRM.Data.Interface.Repositories;

namespace VipCRM.Application.MVC
{
    public class OcorrenciaAdmAppService : IOcorrenciaAdmAppService
    {
        private readonly IOcorrenciaAdmRepository _admRepository;
        private readonly IOcTecnicoClienteRepository _ocTecnicoClienteRepository;
        private readonly IDemandaOcorrenciaRepository _demandaOcorrenciaRepository;

        public OcorrenciaAdmAppService(IOcorrenciaAdmRepository admRepository, IOcTecnicoClienteRepository ocTecnicoClienteRepository, 
            IDemandaOcorrenciaRepository demandaOcorrenciaRepository)
        {
            _admRepository = admRepository;
            _ocTecnicoClienteRepository = ocTecnicoClienteRepository;
            _demandaOcorrenciaRepository = demandaOcorrenciaRepository;
        }

        public IEnumerable<OcorrenciaMonitorViewModel> ObterOcorrenciaMonitor()
        {
            return
                Mapper.Map<IEnumerable<OcorrenciaMonitor>, IEnumerable<OcorrenciaMonitorViewModel>>(
                    _admRepository.ObterOcorrenciaMonitor());
        }

        public IEnumerable<OcorrenciaMonitorViewModel> ObterOcorrenciaMonitorPendentes()
        {
            return
                Mapper.Map<IEnumerable<OcorrenciaMonitor>, IEnumerable<OcorrenciaMonitorViewModel>>(
                    _admRepository.ObterOcorrenciaMonitorPendentes());
        }

        public IEnumerable<OcorrenciaMonitorViewModel> ObterOcorrenciaMonitorPendentes30()
        {
            return
                Mapper.Map<IEnumerable<OcorrenciaMonitor>, IEnumerable<OcorrenciaMonitorViewModel>>(
                    _admRepository.ObterOcorrenciaMonitorPendentes30());
        }

        public IEnumerable<OcorrenciaMonitorViewModel> ObterOcorrenciaMonitorPendentes30Anterior()
        {
            return
                Mapper.Map<IEnumerable<OcorrenciaMonitor>, IEnumerable<OcorrenciaMonitorViewModel>>(
                    _admRepository.ObterOcorrenciaMonitorPendentes30Anterior());
        }

        
        public OcorrenciaTotalizadorViewModel ObterOcorrenciaTotalizador()
        {
            return
                Mapper.Map<OcorrenciaTotalizador, OcorrenciaTotalizadorViewModel>(_admRepository.OcorrenciaTotalizador());
        }

        public IEnumerable<OcTecnicoClienteViewModel> ObterTecnicoCliente()
        {
            return
                Mapper.Map<IEnumerable<OcTecnicoCliente>, IEnumerable<OcTecnicoClienteViewModel>>(
                    _ocTecnicoClienteRepository.ObterOcottenciaTecnicoClientes());
        }

        public IEnumerable<DemandaOcorrenciaViewModel> ObterDemandaOcorrencia()
        {
            return Mapper.Map<IEnumerable<DemandaOcorrencia>, IEnumerable<DemandaOcorrenciaViewModel>>(
                _demandaOcorrenciaRepository.ObterTodos());
        }

        public DashboardAdminViewModel ObterDashBoardAdm()
        {
            var dashBoardAdmin = new DashboardAdminViewModel();
            dashBoardAdmin.OcorrenciaMonitorViewModel = ObterOcorrenciaMonitor();
            dashBoardAdmin.OcTecnicoClienteViewModel = ObterTecnicoCliente();
            dashBoardAdmin.DemandaOcorrencia = ObterDemandaOcorrencia();
            return dashBoardAdmin;
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
        public void Dispose()
        {
            
        }
    }
}