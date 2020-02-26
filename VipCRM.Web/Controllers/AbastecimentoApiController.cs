using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using VipCRM.Application.MVC.Interface;
using VipCRM.Application.MVC.ViewModels;
using VipCRM.Web.Models;

namespace VipCRM.Web.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/abastecimento")]
    public class AbastecimentoApiController : ApiController
    {
        private readonly IAbastecimentoAppService _abastecimentoAppService;

        public AbastecimentoApiController(IAbastecimentoAppService abastecimentoAppService)
        {
            _abastecimentoAppService = abastecimentoAppService;
        }

        [HttpGet]
        [Route("requisicao/{usuarioId}")]
        public List<AbastecimentoViewModel> ObterRequisicoesPorUsuario(int usuarioId)
        {

            return _abastecimentoAppService.ObterPorUsuarioId(usuarioId).ToList();
        }

        [HttpPost]
        [Route("abastecer")]
        public object Abastercer(AbastecimentoNotificacaoModel abastecimento)
        {
            var abastecimentoView = _abastecimentoAppService.ObterPorId(abastecimento.AbastecimentoId);
            var kmAbastecido = 0;
            var validaKm = int.TryParse(abastecimento.Kilometragem, out kmAbastecido);
            if (!validaKm)
                return new
                {
                    errors = true,
                    message = "Quilometragem informada é inválida."
                };


            if (kmAbastecido <= abastecimentoView.Veiculo.Quilometragem)
                return new
                {
                    errors = true,
                    message = "Quilometragem informada é menor que o ultimo abastecimento."
                };

            abastecimentoView.AbastecimentoKm = kmAbastecido.ToString();
            abastecimentoView.AbastecimetnoValor = abastecimento.ValorAbastecimento;
            _abastecimentoAppService.Alterar(abastecimentoView);

            return new
            {
                errors = false,
                message = "Operaçao concluida con sucesso."
            };
        }
    }
}