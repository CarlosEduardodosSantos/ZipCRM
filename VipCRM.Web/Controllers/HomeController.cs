using System.Linq;
using System.Web.Mvc;
using VipCRM.Application.MVC.Interface;
using VipCRM.Application.MVC.ViewModels;
using VipCRM.Web.Framework.Infrastructure;
using VipCRM.Web.Infrastructure;
using VipCRM.Web.Models;

namespace VipCRM.Web.Controllers
{
    [Authorize]
    public class HomeController : MessageControllerBase
    {
        private readonly IOcorrenciaAppService _ocorrenciaAppService;

        public HomeController(IOcorrenciaAppService ocorrenciaAppService)
        {
            _ocorrenciaAppService = ocorrenciaAppService;
        }

        public ActionResult Index()
        {
            int usuarioId = int.Parse(((UserIdentity)User.Identity).UsuarioId);
            var ocorrencias = _ocorrenciaAppService.ObterOcorrenciasPorUsuario(usuarioId);
            var iniciadas = _ocorrenciaAppService.ObterOcorrenciasPorUsuarioIniciada(usuarioId);
            var roteiro = _ocorrenciaAppService.ObterOcorrenciasPorUsuarioRoteiro(usuarioId);
            var finalizadas = _ocorrenciaAppService.ObterOcorrenciasPorUsuarioFinalizadas(usuarioId, 0);

            var dasboard = new DashboardViewModel()
            {
                Ocorrencias = ocorrencias.Count(),
                OcorrenciaIniciada = iniciadas.Count(),
                OcorrenciaFinalizada = finalizadas.Count(),
                OcorrenciaRoteiro = roteiro.Count()
            };

            return View(dasboard);
        }

        public JsonResult DemoResult()
        {
            var data = new { FistName = "Alexandre", LastName = "Amorim" };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ViewResult PedidoFacil()
        {
            var produtos = new Grupo();
            return View(produtos.GetGrupos());
        }
    }
}