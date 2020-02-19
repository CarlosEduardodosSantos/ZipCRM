using System;
using System.Linq;
using System.Web.Mvc;
using VipCRM.Application.MVC.Interface;

namespace VipCRM.Web.Controllers
{
    public class DashBoardProgramacaoController : Controller
    {
        private readonly IOcorrenciaProgramacaoAppService _appService;

        public DashBoardProgramacaoController(IOcorrenciaProgramacaoAppService appService)
        {
            _appService = appService;
        }

        // GET: DashBoardProgramacao
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CarregaControles()
        {
            try
            {
                return Json(new
                {
                    OcorrenciaSoft = _appService.OcorrenciasPorSoftware().OrderByDescending(o => o.Ocorrencias),
                    OcorrenciaClientes = _appService.OcorrenciasPorCliente().OrderByDescending(o => o.Ocorrencias),
                    OcorrenciaProgramadores = _appService.OcorrenciasPorProgramador()
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    error = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult CarregaOcorrencias()
        {
            try
            {
                return Json(new
                {
                    Ocorrencias = _appService.OcorrenciaProgramadores(0,0,0)
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    error = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}