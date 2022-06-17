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
        ValidationResult IncluirOcorrencia(IncluirOcorrencia incluirOcorrencia);
        ValidationResult IncluirRoteiro(IncluirRoteiro incluirRoteiro);
        IEnumerable<Ocorrencia> ObterOcorrenciasPorUsuarioFinalizadas(int usuarioId, int dias);
        IEnumerable<Ocorrencia> ObterOcorrenciasPorOcorrenciaIDFinalizadas(int usuarioId, int ocorrenciaId);
        IEnumerable<Requisicao> ObterResqRequisicoesPorCliente(int usuarioId, int clienteId);
        string ObterFilePdf(int ocorrenciaId);
        IEnumerable<Escalas> ObterEscalasPorData();
        ValidationResult IncluirEscala(Escalas incluirEscala);
    }
}