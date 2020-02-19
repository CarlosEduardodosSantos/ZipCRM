using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using VipCRM.Application.MVC.Interface;
using VipCRM.Application.MVC.ViewModels;

namespace VipCRM.Web.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/cliente")]
    public class ClienteApiController : ApiController
    {
        private readonly IClienteAppService _clienteAppService;

        public ClienteApiController(IClienteAppService clienteAppService)
        {
            _clienteAppService = clienteAppService;
        }

        [HttpGet]
        [Route("bloqueados/{limit}/{page}")]
        public RootObjeto ObterBloqueados(int limit, int page)
        {
            var response = _clienteAppService.ObterBoqueados().ToList();

            var result = new RootObjeto()
            {
                Page = page,
                TotalPage = (response.Count / limit),
                Results = response.Skip((page - 1) * limit).Take(limit).ToList<object>()
            };

            return result;
        }
    }
}
