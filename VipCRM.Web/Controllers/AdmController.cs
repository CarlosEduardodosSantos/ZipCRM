using System.Linq;
using System.Web.Mvc;
using VipCRM.Application.MVC.Interface;

namespace VipCRM.Web.Controllers
{
    public class AdmController : Controller
    {
        private readonly IOcorrenciaAdmAppService _admAppService;

        public AdmController(IOcorrenciaAdmAppService admAppService)
        {
            _admAppService = admAppService;
        }

        // GET: Adm
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult BarChart()
        {
            var dataViewModel = _admAppService.ObterDemandaOcorrencia();

            var data = (from d in dataViewModel
                        select new
                        {
                            y = d.DataOcorrencia.Day + "/" + d.DataOcorrencia.Month,
                            a = d.Ocorrencias,
                            b = d.Finalizadas
                        }).ToList();



            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetOcorrenciaTecnico()
        {
            return Json(_admAppService.ObterTecnicoCliente(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Totalizaddores()
        {

            return Json(new
            {
                Totalizador = _admAppService.ObterOcorrenciaTotalizador(),
                Monitores = _admAppService.ObterOcorrenciaMonitor().OrderByDescending(o=>o.QtdeOcorrenciaConcluida),
                TecnicoClientes = _admAppService.ObterTecnicoCliente()
            }, JsonRequestBehavior.AllowGet);
        }
    }
}