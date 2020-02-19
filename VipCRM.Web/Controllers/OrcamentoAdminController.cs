using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VipCRM.Application.MVC.Interface;

namespace VipCRM.Web.Controllers
{
    public class OrcamentoAdminController : Controller
    {
        private readonly IOrcamentoDashBoardAppService _appService;
        private readonly ILocationAppService _locationApp;

        public OrcamentoAdminController(IOrcamentoDashBoardAppService appService, ILocationAppService locationApp)
        {
            _appService = appService;
            _locationApp = locationApp;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RankingEquipe()
        {
            var data = _appService.RankingEquipePorData(DateTime.Now.AddDays(-30), DateTime.Now);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RankingOrcamento()
        {
            var data = _appService.OrcamentoRankingPorData(DateTime.Now.AddDays(-30), DateTime.Now);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RankingVendedor()
        {
            var data = _appService.OrcamentoRankingPorData(DateTime.Now.AddDays(-30), DateTime.Now);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UltimosOrcamentos(int page, int limit)
        {
            var data = _appService.UltimosOrcamentos(DateTime.Now.AddDays(-30), DateTime.Now, page, limit);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Totalizar()
        {
            var data = _appService.TotalizarPorData(DateTime.Now.AddDays(-30), DateTime.Now);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLocation(double latitude, double longitude)
        {
            var data = _locationApp.ReverseGeocode(latitude, longitude);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}