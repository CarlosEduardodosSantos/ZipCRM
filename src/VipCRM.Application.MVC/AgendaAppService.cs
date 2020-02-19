using System;
using System.Collections.Generic;
using AutoMapper;
using VipCRM.Application.MVC.Interface;
using VipCRM.Application.MVC.Validation;
using VipCRM.Application.MVC.ViewModels;
using VipCRM.Core.Domain.Entites;
using VipCRM.Data.Interface.Repositories;
using VipCRM.Core.Domain.ValueObjects;

namespace VipCRM.Application.MVC
{
    public class AgendaAppService : IAgendaAppService
    {
        private readonly IAgendaRepositories _agendaRepositories;

        public AgendaAppService(IAgendaRepositories agendaRepositories)
        {
            _agendaRepositories = agendaRepositories;
        }

        public AgendaViewModel ObterPorId(int id)
        {
            return Mapper.Map<Agenda, AgendaViewModel>(_agendaRepositories.ObterPorId(id));
        }

        public IEnumerable<AgendaViewModel> ObterPorDataAgendamento(int usuarioId, DateTime data)
        {
            return Mapper.Map<IEnumerable<Agenda>, IEnumerable<AgendaViewModel>>(
                _agendaRepositories.ObterPorDataAgendamento(usuarioId, data));
        }

        public IEnumerable<AgendaViewModel> ObterPorDatainicioFim(int usuarioId, DateTime dataInicio, DateTime dataFim)
        {
            return Mapper.Map<IEnumerable<Agenda>, IEnumerable<AgendaViewModel>>(
                _agendaRepositories.ObterPorDatainicioFim(usuarioId, dataInicio, dataFim));
        }

        public IEnumerable<AgendaViewModel> ObterPendentes(int usuarioId)
        {
            return Mapper.Map<IEnumerable<Agenda>, IEnumerable<AgendaViewModel>>(
                _agendaRepositories.ObterPendentes(usuarioId));
        }

        public IEnumerable<AgendaViewModel> ObterPorPrioridade(int usuarioId, int prioridade)
        {
            return Mapper.Map<IEnumerable<Agenda>, IEnumerable<AgendaViewModel>>(
                _agendaRepositories.ObterPorPrioridade(usuarioId, prioridade));
        }


        public ValidationAppResult Conclusao(int id, int usuarioId, string conclusao, DateTime dataHora)
        {
            return Mapper.Map<ValidationResult, ValidationAppResult>(_agendaRepositories.Conclusao(id, usuarioId, conclusao, dataHora));
        }

        public ValidationAppResult CriarAgendamento(AgendaViewModel modelView)
        {
            var model = Mapper.Map<AgendaViewModel, Agenda>(modelView);
            return Mapper.Map<ValidationResult, ValidationAppResult>(_agendaRepositories.CriarAgendamento(model));
        }
    }
}