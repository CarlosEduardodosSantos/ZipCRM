using CommonServiceLocator.SimpleInjectorAdapter;
using Microsoft.Practices.ServiceLocation;
using SimpleInjector;
using VipCRM.Application.MVC;
using VipCRM.Application.MVC.Interface;
using VipCRM.Data.Interface.Repositories;
using VipCRM.Data.Repositories;

namespace Vip.CrossCutting.IoC
{
    public class ContainerIoc
    {
        public ContainerIoc()
        {
            var adapter = new SimpleInjectorServiceLocatorAdapter(GetModule());
            ServiceLocator.SetLocatorProvider(() => adapter);
        }

        public SimpleInjector.Container GetModule()
        {
            var container = new SimpleInjector.Container();



            container.Register<IOcorrenciaAppService, OcorrenciaAppService>();
            container.Register<IUsuarioAppService, UsuarioAppService>();
            container.Register<IRoleAppService,RoleAppService>();
            container.Register<IAgendaAppService,AgendaAppService>();
            container.Register<IClienteAppService, ClienteAppService>();
            container.Register<IOcorrenciaImagemAppService, OcorrenciaImagemAppService>();
            container.Register<IOcorrenciaAdmAppService, OcorrenciaAdmAppService>();
            container.Register<IDashBoardAdminAppService, DashBoardAdminAppService>();
            container.Register<IOcorrenciaProgramacaoAppService, OcorrenciaProgramacaoAppService>();
            container.Register<IOrcamentoDashBoardAppService, OrcamentoDashBoardAppService>();
            container.Register<IAbastecimentoAppService, AbastecimentoAppService>();
            container.Register<IEmpresaAppService, EmpresaAppService>();
            //data
            container.Register<IOcorrenciaRepositories, OcorrenciaRepositories>();
            container.Register<IUsuarioRepositories, UsuarioRepositories>();
            container.Register<IRoleRepositories, RoleRepositories>();
            container.Register<IAgendaRepositories, AgendaRepositories>();
            container.Register<IClienteRepositories, ClienteRepositories>();
            container.Register<IOcorrenciaImagemRepositories, OcorrenciaImagemRepositories>();
            container.Register<IOcorrenciaAdmRepository, OcorrenciaAdmRepository>();
            container.Register<IOcTecnicoClienteRepository, OcTecnicoClienteRepository>();
            container.Register<IDemandaOcorrenciaRepository, DemandaOcorrenciaRepository>();
            container.Register<IOcorrenciaProgramacaoRepository, OcorrenciaProgramacaoRepository>();
            container.Register<IOrcamentoDashBoardRepository, OrcamentoDashBoardRepository>();
            container.Register<IAbastecimentoRepository, AbastecimentoRepository>();
            container.Register<IEmpresaRepository, EmpresaRepository>();

            return container;
        }
    }
}