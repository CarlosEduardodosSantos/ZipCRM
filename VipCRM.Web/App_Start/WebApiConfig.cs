using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using SimpleInjector.Integration.WebApi;
using Vip.CrossCutting.IoC;

namespace VipCRM.Web
{
    class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            // New code
            config.EnableCors();

            config.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("text/html"));

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );            

            /*
            configuration.Routes.MapHttpRoute(
                name: "ApiByAction",
                routeTemplate: "api/ocorrencia/roteiro/{usuarioId}",
                defaults: new { controller = "OcorrenciaApi", action = "ObterPorRoteiro" }
            );
            configuration.Routes.MapHttpRoute(
                name: "ApiByMinhas",
                routeTemplate: "api/ocorrencia/minhas/{usuarioId}",
                defaults: new { controller = "OcorrenciaApi", action = "ObterOcorrencias" }
            );
            configuration.Routes.MapHttpRoute(
                name: "ApiByFinalizadas",
                routeTemplate: "api/ocorrencia/finalizadas/{usuarioId}",
                defaults: new { controller = "OcorrenciaApi", action = "ObterFinalizadas" }
            );

            configuration.Routes.MapHttpRoute(
                name: "ApiByInicia",
                routeTemplate: "api/ocorrencia/inicia/{iniciaOcorrenciaView}",
                defaults: new { controller = "OcorrenciaApi", action = "IniciaOcorrencia" }
            );

            configuration.Routes.MapHttpRoute(
                name: "ApiByFinaliza",
                routeTemplate: "api/ocorrencia/finaliza/{finalizaOcorrencia}",
                defaults: new { controller = "OcorrenciaApi", action = "FinalizaOcorrencia" }
            );
            */
            var container = new ContainerIoc().GetModule();
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

        }
    }
}