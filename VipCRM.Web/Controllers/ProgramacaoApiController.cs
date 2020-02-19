using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using VipCRM.Application.MVC.Interface;
using VipCRM.Application.MVC.ViewModels;

namespace VipCRM.Web.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/programacao")]
    public class ProgramacaoApiController : ApiController
    {
        private readonly IOcorrenciaProgramacaoAppService _ocorrenciaProgramacaoApp;

        public ProgramacaoApiController(IOcorrenciaProgramacaoAppService ocorrenciaProgramacaoApp)
        {
            _ocorrenciaProgramacaoApp = ocorrenciaProgramacaoApp;
        }

        [HttpGet]
        [Route("homologacao/{usuarioId}/{limit}/{page}")]
        public RootObjeto ObterBloqueados(string usuarioId, int limit, int page)
        {
            var data = _ocorrenciaProgramacaoApp.ObterPorUsuario(usuarioId).ToList();
            var result = new RootObjeto()
            {
                Page = page,
                TotalPage = (data.Count/limit),
                Results = data.Skip((page - 1) * limit).Take(limit).ToList<object>()
            };
            
            return result;
        }
    }
}
