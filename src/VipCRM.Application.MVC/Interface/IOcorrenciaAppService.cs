using System;
using System.Collections.Generic;
using VipCRM.Application.MVC.Validation;
using VipCRM.Application.MVC.ViewModels;

namespace VipCRM.Application.MVC.Interface
{
    public interface IOcorrenciaAppService
    {
        OcorrenciaViewModel ObterOcorrenciaId(int id);
        IEnumerable<OcorrenciaViewModel> ObterOcorrenciasPorUsuario(int usuarioId);
        IEnumerable<OcorrenciaViewModel> ObterOcorrenciasPorUsuarioRoteiro(int usuarioId);
        IEnumerable<OcorrenciaViewModel> ObterOcorrenciasPorUsuarioIniciada(int usuarioId);
        IEnumerable<OcorrenciaViewModel> ObterOcorrenciasPorPesquisa(int usuarioId, string pesquisa);        
        bool ExisteOcorrenciasIniciadaNaoFinalizadas(int usuarioId);
        ValidationAppResult IniciarOcorrencia(IniciaOcorrenciaViewModel iniciaOcorrenciaView);
        ValidationAppResult IncluirOcorrencia(IncluirOcorrenciaViewModels incluirOcorrenciaView);
        ValidationAppResult FinalizaOcorrencia(FinalizaOcorrenciaViewModel finalizaOcorrenciaViewModel);
        ValidationAppResult IncluirRoteiro(IncluirRoteiroViewModel incluirRoteiroView);
        IEnumerable<OcorrenciaViewModel> ObterOcorrenciasPorUsuarioFinalizadas(int usuarioId, int dias);
        IEnumerable<OcorrenciaViewModel> ObterOcorrenciasPorOcorrenciaIDFinalizadas(int usuarioId, int ocorrenciaId);
        IEnumerable<RequisicaoViewModel> ObterResqRequisicoesPorCliente(int usuarioId, int clienteId);
        string ObterFilePdf(int ocorrenciaId);
        IEnumerable<EscalaViewModel> ObterEscalasPorData();
        ValidationAppResult IncluirEscala(EscalaViewModel incluirEscalaView);
    }
}