using System;
using System.Linq;
using System.Web.Mvc;
using VipCRM.Application.MVC.Interface;

namespace VipCRM.Web.Controllers
{
    public class DashboardAdminController : Controller
    {
        private readonly IDashBoardAdminAppService _boardAdminAppService;

        public DashboardAdminController(IDashBoardAdminAppService boardAdminAppService)
        {
            _boardAdminAppService = boardAdminAppService;
        }

        // GET: DashboardAdmin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BarChart()
        {
            var dataViewModel = _boardAdminAppService.DemandaOcorrencia();

            var data = (from d in dataViewModel
                        select new
                        {
                            y = d.DataOcorrencia.Day + "/" + d.DataOcorrencia.Month,
                            a = d.Ocorrencias,
                            b = d.Finalizadas
                        }).ToList();



            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CarregaControles()
        {
            try
            {
                return Json(new
                {
                    Totalizador = _boardAdminAppService.Totalizador(),
                    Monitores = _boardAdminAppService.RoteiroDia().OrderByDescending(o => o.QtdeOcorrenciaConcluida),
                    MonitoresPendentes = _boardAdminAppService.RoteiroPendentes().OrderByDescending(o => o.QtdeOcorrenciaPendente),
                    MonitoresPendentes30 = _boardAdminAppService.RoteiroPendentes30().OrderByDescending(o => o.QtdeOcorrenciaPendente30),
                    MonitoresPendentes30Anterior = _boardAdminAppService.RoteiroPendentes30Anterior().OrderByDescending(o => o.QtdeOcorrenciaPendente30Anterior),
                    MonitoresConcluido = _boardAdminAppService.RoteiroConcluido().OrderByDescending(o => o.QtdeConcluida),
                    MonitoresConcluido30 = _boardAdminAppService.RoteiroConcluido30().OrderByDescending(o => o.QtdeConcluida30),
                    MonitoresConcluido30Anterior = _boardAdminAppService.RoteiroConcluido30Anterior().OrderByDescending(o => o.QtdeConcluida30Anterior),
                    MonitoresOcorrenciasDias = _boardAdminAppService.ObterOcorrenciasDias().OrderBy(o => o.hora),
                    MonitoresEscalaSabado = _boardAdminAppService.EscalaSabados().OrderBy(o => o.DATA),
                    TecnicoClientes = _boardAdminAppService.OcorrenciasPorTecnico()
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
        public ActionResult PendenciaPorTecnico(int usuarioId, string situacao)
        {
            try
            {
                var model = _boardAdminAppService.PendenciaPorTecnico(usuarioId, situacao);

                return Json(new
                {
                    tecnico = model
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
        public ActionResult LocalizacaoDiaria()
        {
            try
            {
                var model = _boardAdminAppService.ObterLocalizacaoDiaria().Where(t=> t.Latitude != string.Empty).OrderBy(t => t.Nome);

                return Json(new
                {
                    localizacoes = model
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

        public ActionResult ObterArquivoPdf()
        {
            try
            {
                var model = _boardAdminAppService.ObterLocalizacaoDiaria().Where(t => t.Latitude != string.Empty).OrderBy(t => t.Nome);

                return Json(new
                {
                    localizacoes = model
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