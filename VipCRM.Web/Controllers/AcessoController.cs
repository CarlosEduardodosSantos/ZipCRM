using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using VipCRM.Application.MVC.Interface;
using VipCRM.Application.MVC.ViewModels;

namespace VipCRM.Web.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/acesso")]
    public class AcessoController : ApiController
    {
        private readonly IUsuarioAppService _usuarioService;

        public AcessoController(IUsuarioAppService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("login/{usuario}/{senha}")]
        public UsuarioViewModel Login(string usuario, string senha)
        {
            var user = _usuarioService.AutenticaUsuario(usuario, senha);

            return user;
        }
        [HttpGet]
        [Route("getImagem/{usuarioId}")]
        public UsuarioImagemViewModel GetImagem(string usuarioId)
        {
            var user = _usuarioService.ObterImagemUduarioId(usuarioId);

            return user;
        }

        [HttpPost]
        [Route("modificarImagem")]
        public IHttpActionResult ModificarImagem(UsuarioImagemViewModel usuarioImagemView)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _usuarioService.ModificaUsuario(usuarioImagemView);

                return StatusCode(HttpStatusCode.OK);
            }
            catch
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }
        [HttpGet]
        [Route("usuarios/{pageSize}/{page}")]
        public List<UsuarioViewModel> GetUsuarios(int pageSize, int page)
        {
            var users = _usuarioService.ObterUsuarios().Skip(page * pageSize).Take(pageSize).ToList(); 

            return users;
        }
        [HttpGet]
        [Route("usuarios")]
        public List<UsuarioViewModel> ObterUsuarios()
        {
            var users = _usuarioService.ObterUsuarios().ToList();
            return users.ToList();

        }
        [HttpGet]
        [Route("veiculos")]
        public List<VeiculoViewModel> ObterVeiculos()
        {
            var veiculos = _usuarioService.ObterVeiculos().ToList();
            return veiculos.ToList();

        }
        [HttpGet]
        [Route("folha/{usuario}/{mes}/{ano}")]
        public FolhaPgtoViewModel ObterFolhaPgto(int usuario, string mes, string ano)
        {
            var folha = _usuarioService.ObterFolhaPgto(usuario, mes, ano);
            return folha;
        }
    }
}