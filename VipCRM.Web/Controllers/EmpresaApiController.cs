using System.Web.Http;
using System.Web.Http.Cors;
using VipCRM.Application.MVC.Interface;
using VipCRM.Application.MVC.ViewModels;

namespace VipCRM.Web.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/empresa")]
    public class EmpresaApiController : ApiController
    {
        private readonly IEmpresaAppService _empresaAppService;

        public EmpresaApiController(IEmpresaAppService empresaAppService)
        {
            _empresaAppService = empresaAppService;
        }

        [HttpGet]
        [Route("obter/{empresaId}")]
        public EmpresaViewModel GetById(string empresaId)
        {
            return _empresaAppService.ObterPorId(empresaId);
        }

    }
}