using System;
using System.Linq;
using System.Web.Mvc;
using VipCRM.Application.MVC.Interface;
using VipCRM.Application.MVC.ViewModels;
using VipCRM.Web.Framework.Infrastructure;
using VipCRM.Web.Framework.UI.toastr;
using VipCRM.Web.Infrastructure;

namespace VipCRM.Web.Controllers
{
    [Authorize(Roles = "menu_OP_TeleAtendimento")]
    public class AgendaController : MessageControllerBase
    {
        private readonly IAgendaAppService _agendaAppService;
        private readonly IClienteAppService _clienteAppService;

        public AgendaController(IAgendaAppService agendaAppService, IClienteAppService clienteAppService)
        {
            _agendaAppService = agendaAppService;
            _clienteAppService = clienteAppService;
        }

        // GET: Agenda
        public ActionResult Index()
        {
            int usuarioId = int.Parse(((UserIdentity)User.Identity).UsuarioId);
            return View(_agendaAppService.ObterPendentes(usuarioId));
        }

        public ActionResult Detalhe(int id)
        {
            return View(_agendaAppService.ObterPorId(id));
        }

        [HttpPost]
        public ActionResult Finalizar(AgendaViewModel model)
        {
            if (ModelState.IsValid)
            {
                int usuarioId = int.Parse(((UserIdentity)User.Identity).UsuarioId);

                _agendaAppService.Conclusao(model.AgendaId, usuarioId, model.Conclusao, DateTime.Now);

                AddToastMessage("Informação", "Agendamento concluido com sucesso!", ToastType.Success);
                return RedirectToAction("Index");
            }
            AddToastMessage("Informação", "Não foi possivel concluir o agendamento!", ToastType.Error);
            return View("Detalhe",model);
        }

        public ActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Criar(AgendaViewModel model)
        {
            if (ModelState.IsValid)
            {
                int usuarioId = int.Parse(((UserIdentity) User.Identity).UsuarioId);
                model.UsuarioId = usuarioId;

                var messaValid = _agendaAppService.CriarAgendamento(model);

                if (!messaValid.IsValid)
                {
                    AddToastMessage("Informação", "Não foi possivel concluir o agendamento!", ToastType.Error);
                    return View("Criar", model);
                }

                AddToastMessage("Informação", "Agendamento concluido com sucesso!", ToastType.Success);
                return RedirectToAction("Index");
            }
            AddToastMessage("Informação", "Não foi possivel concluir o agendamento!", ToastType.Error);
            return View("Criar", model);

        }

        public JsonResult BuscarCliente(string nome)
        {
            var cliente = _clienteAppService.ObterPorNome(nome);

            var data = (from p in cliente
                select new
                {
                    id  = p.ClienteId,
                    value = p.Nome
                });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}