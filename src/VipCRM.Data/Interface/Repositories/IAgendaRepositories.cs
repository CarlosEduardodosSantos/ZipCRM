
using System;
using System.Collections.Generic;
using VipCRM.Core.Domain.Entites;
using VipCRM.Core.Domain.ValueObjects;

namespace VipCRM.Data.Interface.Repositories
{
    public interface IAgendaRepositories
    {
        Agenda ObterPorId(int id);
        IEnumerable<Agenda> ObterPorDataAgendamento(int usuarioId, DateTime data);
        IEnumerable<Agenda> ObterPorDatainicioFim(int usuarioId, DateTime dataInicio, DateTime dataFim);
        IEnumerable<Agenda> ObterPendentes(int usuarioId);
        IEnumerable<Agenda> ObterPorPrioridade(int usuarioId, int prioridade);
        ValidationResult Conclusao(int id, int usuarioId, string conclusao, DateTime dataHora);
        ValidationResult CriarAgendamento(Agenda model);
    }
}