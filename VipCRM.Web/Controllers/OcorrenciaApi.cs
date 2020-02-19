using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using VipCRM.Application.MVC.Interface;
using VipCRM.Application.MVC.ViewModels;
using VipCRM.Web.Models;

namespace VipCRM.Web.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/ocorrencia")]
    public class OcorrenciaApiController : ApiController
    {
        private readonly IOcorrenciaAppService _appServer;
        private readonly IOcorrenciaImagemAppService _ocorrenciaImagemAppService;

        public OcorrenciaApiController(IOcorrenciaAppService appService, IOcorrenciaImagemAppService ocorrenciaImagemAppService)
        {
            _appServer = appService;
            _ocorrenciaImagemAppService = ocorrenciaImagemAppService;
        }

        [HttpGet]
        [Route("roteiro/{usuarioId}")]
        public List<OcorrenciaViewModel> ObterPorRoteiro(int usuarioId)
        {
            var response = _appServer.ObterOcorrenciasPorUsuarioRoteiro(usuarioId);
            return response.ToList();
        }
        [HttpGet]
        [Route("minhas/{usuarioId}")]
        public List<OcorrenciaViewModel> ObterOcorrencias(int usuarioId)
        {
            var response = _appServer.ObterOcorrenciasPorUsuario(usuarioId);
            return response.ToList();
        }
        [HttpGet]
        [Route("requisicoes/{usuarioId}/{clienteId}")]
        public List<RequisicaoViewModel> ObterRequisicoes(int usuarioId, int clienteId)
        {
            var response = _appServer.ObterResqRequisicoesPorCliente(usuarioId, clienteId);
            return response.ToList();
        }

        [HttpGet]
        [Route("finalizadas/{usuarioId}")]
        public List<OcorrenciaViewModel> ObterFinalizadas(int usuarioId)
        {
            var response = _appServer.ObterOcorrenciasPorUsuarioIniciada(usuarioId);
            return response.ToList();
        }
        [HttpGet]
        [Route("filePdf/{ocorrenciaId}")]
        public string ObterFilePdf(int ocorrenciaId)
        {
            var response = _appServer.ObterFilePdf(ocorrenciaId);
            return response;
        }
        [HttpPost]
        [Route("inicia")]
        public IHttpActionResult IniciaOcorrencia(IniciaOcorrenciaViewModel iniciaOcorrenciaView)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _appServer.IniciarOcorrencia(iniciaOcorrenciaView);

                return StatusCode(HttpStatusCode.OK);
            }
            catch
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }
        [HttpPost]
        [Route("finaliza")]
        public IHttpActionResult FinalizaOcorrencia(FinalizaOcorrenciaViewModel finalizaOcorrencia)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _appServer.FinalizaOcorrencia(finalizaOcorrencia);

                return StatusCode(HttpStatusCode.OK);
            }
            catch
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public IEnumerable<OcorrenciaViewModel> Get(int id)
        {
            return _appServer.ObterOcorrenciasPorUsuarioRoteiro(id);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}