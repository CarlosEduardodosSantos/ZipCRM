using System;
using System.Collections.Generic;
using VipCRM.Core.Domain.Entites;
using VipCRM.Core.Domain.ValueObjects;

namespace VipCRM.Data.Interface.Repositories
{
    public interface IOcorrenciaRepositories
    {
        Ocorrencia ObterOcorrenciaId(int id);
        IEnumerable<Ocorrencia> ObterOcorrenciasPorUsuario(int usuarioId);
        IEnumerable<Ocorrencia> ObterOcorrenciasPorUsuarioRoteiro(int usuarioId);
        IEnumerable<Ocorrencia> ObterOcorrenciasPorPesquisa(int usuarioId, string pesquisa);
        IEnumerable<Ocorrencia> ObterOcorrenciasPorUsuarioIniciada(int usuarioId);
        bool ExisteOcorrenciasIniciadaNaoFinalizadas(int usuarioId);
        ValidationResult IniciarOcorrencia(IniciaOcorrencia iniciaOcorrencia);
        ValidationResult FinalizaOcorrencia(FinalizaOcorrencia finalizaOcorrencia);

        IEnumerable<Ocorrencia> ObterOcorrenciasPorUsuarioFinalizadas(int usuarioId, int dias);
        IEnumerable<Requisicao> ObterResqRequisicoesPorCliente(int usuarioId, int clienteId);
        string ObterFilePdf(int ocorrenciaId);

    }
}