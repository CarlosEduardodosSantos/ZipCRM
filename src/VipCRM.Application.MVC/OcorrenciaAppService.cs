using System;
using System.Collections.Generic;
using AutoMapper;
using VipCRM.Application.MVC.Interface;
using VipCRM.Application.MVC.Validation;
using VipCRM.Application.MVC.ViewModels;
using VipCRM.Core.Domain.Entites;
using VipCRM.Core.Domain.ValueObjects;
using VipCRM.Data.Interface.Repositories;

namespace VipCRM.Application.MVC
{
    public class OcorrenciaAppService : IOcorrenciaAppService
    {
        private readonly IOcorrenciaRepositories _ocorrenciaServices;
        public OcorrenciaAppService(IOcorrenciaRepositories ocorrenciaServices)
        {
            _ocorrenciaServices = ocorrenciaServices;
        }
        public OcorrenciaViewModel ObterOcorrenciaId(int id)
        {
            var ocorrencia = _ocorrenciaServices.ObterOcorrenciaId(id);
            return Mapper.Map<Ocorrencia, OcorrenciaViewModel>(ocorrencia);
        }

        public IEnumerable<OcorrenciaViewModel> ObterOcorrenciasPorUsuario(int usuarioId)
        {
            return
                Mapper.Map<IEnumerable<Ocorrencia>, IEnumerable<OcorrenciaViewModel>>(
                    _ocorrenciaServices.ObterOcorrenciasPorUsuario(usuarioId));
        }

        public IEnumerable<OcorrenciaViewModel> ObterOcorrenciasPorUsuarioRoteiro(int usuarioId)
        {
            return
                Mapper.Map<IEnumerable<Ocorrencia>, IEnumerable<OcorrenciaViewModel>>(
                    _ocorrenciaServices.ObterOcorrenciasPorUsuarioRoteiro(usuarioId));
        }

        public IEnumerable<OcorrenciaViewModel> ObterOcorrenciasPorUsuarioIniciada(int usuarioId)
        {
            return
                Mapper.Map<IEnumerable<Ocorrencia>, IEnumerable<OcorrenciaViewModel>>(
                    _ocorrenciaServices.ObterOcorrenciasPorUsuarioIniciada(usuarioId));
        }
                
        public bool ExisteOcorrenciasIniciadaNaoFinalizadas(int usuarioId)
        {
            return _ocorrenciaServices.ExisteOcorrenciasIniciadaNaoFinalizadas(usuarioId);
        }

        public ValidationAppResult IniciarOcorrencia(IniciaOcorrenciaViewModel iniciaOcorrenciaView)
        {
            var iniciaOcorrencia = Mapper.Map<IniciaOcorrenciaViewModel, IniciaOcorrencia>(iniciaOcorrenciaView);
            return Mapper.Map<ValidationResult, ValidationAppResult>(_ocorrenciaServices.IniciarOcorrencia(iniciaOcorrencia));
        }

        public ValidationAppResult IncluirOcorrencia(IncluirOcorrenciaViewModels incluirOcorrenciaView)
        {
            var incluirOcorrencia = Mapper.Map<IncluirOcorrenciaViewModels, IncluirOcorrencia>(incluirOcorrenciaView);
            return Mapper.Map<ValidationResult, ValidationAppResult>(_ocorrenciaServices.IncluirOcorrencia(incluirOcorrencia));
        }

        public ValidationAppResult FinalizaOcorrencia(FinalizaOcorrenciaViewModel finalizaOcorrenciaViewModel)
        {
            var finalizaOcorrencia = Mapper.Map<FinalizaOcorrenciaViewModel, FinalizaOcorrencia>(finalizaOcorrenciaViewModel);

            return Mapper.Map<ValidationResult, ValidationAppResult>(_ocorrenciaServices.FinalizaOcorrencia(finalizaOcorrencia));
        }

        public ValidationAppResult IncluirRoteiro(IncluirRoteiroViewModel incluirRoteiroView)
        {
            var incluirRoteiro = Mapper.Map<IncluirRoteiroViewModel, IncluirRoteiro>(incluirRoteiroView);
            return Mapper.Map<ValidationResult, ValidationAppResult>(_ocorrenciaServices.IncluirRoteiro(incluirRoteiro));
        }

        public IEnumerable<OcorrenciaViewModel> ObterOcorrenciasPorPesquisa(int usuarioId, string pesquisa)
        {
            return
                Mapper.Map<IEnumerable<Ocorrencia>, IEnumerable<OcorrenciaViewModel>>(
                    _ocorrenciaServices.ObterOcorrenciasPorPesquisa(usuarioId, pesquisa));
        }


        public IEnumerable<OcorrenciaViewModel> ObterOcorrenciasPorUsuarioFinalizadas(int usuarioId, int dias)
        {
            return
                Mapper.Map<IEnumerable<Ocorrencia>, IEnumerable<OcorrenciaViewModel>>(
                    _ocorrenciaServices.ObterOcorrenciasPorUsuarioFinalizadas(usuarioId, dias));
        }

        public IEnumerable<OcorrenciaViewModel> ObterOcorrenciasPorOcorrenciaIDFinalizadas(int usuarioId, int ocorrenciaId)
        {
            return
                Mapper.Map<IEnumerable<Ocorrencia>, IEnumerable<OcorrenciaViewModel>>(
                    _ocorrenciaServices.ObterOcorrenciasPorOcorrenciaIDFinalizadas(usuarioId, ocorrenciaId));
        }

        public IEnumerable<RequisicaoViewModel> ObterResqRequisicoesPorCliente(int usuarioId, int clienteId)
        {
            return
                Mapper.Map<IEnumerable<Requisicao>, IEnumerable<RequisicaoViewModel>>(
                    _ocorrenciaServices.ObterResqRequisicoesPorCliente(usuarioId, clienteId));
        }

        public string ObterFilePdf(int ocorrenciaId)
        {
            return _ocorrenciaServices.ObterFilePdf(ocorrenciaId);
        }

        public IEnumerable<EscalaViewModel> ObterEscalasPorData()
        {
            return Mapper.Map<IEnumerable<Escalas>, IEnumerable<EscalaViewModel>>(
                _ocorrenciaServices.ObterEscalasPorData());
        }

        public ValidationAppResult IncluirEscala(EscalaViewModel incluirEscalaView)
        {
            var incluirEscala = Mapper.Map<EscalaViewModel, Escalas>(incluirEscalaView);
            return Mapper.Map<ValidationResult, ValidationAppResult>(_ocorrenciaServices.IncluirEscala(incluirEscala));
        }
    }
}