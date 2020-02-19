using Ninject.Modules;
using VipCRM.Application.MVC;
using VipCRM.Application.MVC.Interface;
using VipCRM.Data.Interface.Repositories;
using VipCRM.Data.Repositories;

namespace Vip.CrossCutting.IoC
{
    public class NinjectModulo : NinjectModule
    {

        public override void Load()
        {
            //app
            Bind<IOcorrenciaAppService>().To<OcorrenciaAppService>();
            Bind<IUsuarioAppService>().To<UsuarioAppService>();
            Bind<IRoleAppService>().To<RoleAppService>();
            Bind<IAgendaAppService>().To<AgendaAppService>();
            Bind<IClienteAppService>().To<ClienteAppService>();
            Bind<IOcorrenciaImagemAppService>().To<OcorrenciaImagemAppService>();
            Bind<IOcorrenciaAdmAppService>().To<OcorrenciaAdmAppService>();
            Bind<IDashBoardAdminAppService>().To<DashBoardAdminAppService>();
            Bind<IOcorrenciaProgramacaoAppService>().To<OcorrenciaProgramacaoAppService>();
            Bind<IOrcamentoDashBoardAppService>().To<OrcamentoDashBoardAppService>();
            Bind<ILocationAppService>().To<LocationAppService>();
            //data
            Bind<IOcorrenciaRepositories>().To<OcorrenciaRepositories>();
            Bind<IUsuarioRepositories>().To<UsuarioRepositories>();
            Bind<IRoleRepositories>().To<RoleRepositories>();
            Bind<IAgendaRepositories>().To<AgendaRepositories>();
            Bind<IClienteRepositories>().To<ClienteRepositories>();
            Bind<IOcorrenciaImagemRepositories>().To<OcorrenciaImagemRepositories>();
            Bind<IOcorrenciaAdmRepository>().To<OcorrenciaAdmRepository>();
            Bind<IOcTecnicoClienteRepository>().To<OcTecnicoClienteRepository>();
            Bind<IDemandaOcorrenciaRepository>().To<DemandaOcorrenciaRepository>();
            Bind<IOcorrenciaProgramacaoRepository>().To<OcorrenciaProgramacaoRepository>();
            Bind<IOrcamentoDashBoardRepository>().To<OrcamentoDashBoardRepository>();

        }
    }
}