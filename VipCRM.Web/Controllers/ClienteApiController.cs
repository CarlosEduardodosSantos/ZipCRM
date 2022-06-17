using System.Collections.Generic;
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

        [HttpGet]
        [Route("clientes/{nome}")]
        public List<ClienteViewModel> ObterCliente(string nome)
        {
            var response = _clienteAppService.ObterPorNome(nome).ToList();
            return response.ToList();
           
        }

        [HttpGet]
        [Route("ranking/{dias}")]
        public List<RankingClientesViewModel> ObterRankingClientes(int dias)
        {
            var response = _clienteAppService.ObterRankingClientes(dias).ToList();
            return response.ToList();
        }

        [HttpGet]
        [Route("carencia/{dias}")]
        public List<RankingClientesViewModel> ObterCarenciaClientes(int dias)
        {
            var response = _clienteAppService.ObterCarenciaClientes(dias).ToList();
            return response.ToList();
        }
    }
}


