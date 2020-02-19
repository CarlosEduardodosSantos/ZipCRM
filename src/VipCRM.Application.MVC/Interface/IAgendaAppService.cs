using System;
using System.Collections.Generic;
using VipCRM.Application.MVC.Validation;
using VipCRM.Application.MVC.ViewModels;

namespace VipCRM.Application.MVC.Interface
{
    public interface IAgendaAppService
    {
        AgendaViewModel ObterPorId(int id);
        IEnumerable<AgendaViewModel> ObterPorDataAgendamento(int usuarioId, DateTime data);
        IEnumerable<AgendaViewModel> ObterPorDatainicioFim(int usuarioId, DateTime dataInicio, DateTime dataFim);
        IEnumerable<AgendaViewModel> ObterPendentes(int usuarioId);
        IEnumerable<AgendaViewModel> ObterPorPrioridade(int usuarioId, int prioridade);
        ValidationAppResult Conclusao(int id, int usuarioId, string conclusao, DateTime dataHora);
        ValidationAppResult CriarAgendamento(AgendaViewModel modelView);
    }
}