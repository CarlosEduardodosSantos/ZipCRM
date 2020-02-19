using System.Web.Security;
using VipCRM.Web.Infrastructure;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(VipCRM.Web.App_Start.NinjectWebCommon), "Start")]
//[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(VipCRM.Web.App_Start.NinjectWebCommon), "RegisterMembership")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(VipCRM.Web.App_Start.NinjectWebCommon), "Stop")]

namespace VipCRM.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Vip.CrossCutting.IoC;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }


        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = RegisterServices();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                //RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        public static StandardKernel RegisterServices()
        {
            var ioc = new Container().GetModule();

            
            return ioc;
        }        
    }
}
