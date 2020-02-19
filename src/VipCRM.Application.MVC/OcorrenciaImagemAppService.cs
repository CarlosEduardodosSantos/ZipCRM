using System.Collections.Generic;
using AutoMapper;
using VipCRM.Application.MVC.Interface;
using VipCRM.Application.MVC.ViewModels;
using VipCRM.Core.Domain.Entites;
using VipCRM.Data.Interface.Repositories;

namespace VipCRM.Application.MVC
{
    public class OcorrenciaImagemAppService : IOcorrenciaImagemAppService
    {
        private readonly IOcorrenciaImagemRepositories _ocorrenciaImagemRepositories;

        public OcorrenciaImagemAppService(IOcorrenciaImagemRepositories ocorrenciaImagemRepositories)
        {
            _ocorrenciaImagemRepositories = ocorrenciaImagemRepositories;
        }

        public OcorrenciaImagemViewModel ObterPorId(int id)
        {
            return Mapper.Map<OcorrenciaImagem, OcorrenciaImagemViewModel>(_ocorrenciaImagemRepositories.ObterPorId(id));
        }

        public IEnumerable<OcorrenciaImagemViewModel> ObterPorOcorrencia(int ocorrenciaId)
        {
            return Mapper.Map<IEnumerable<OcorrenciaImagem>, IEnumerable<OcorrenciaImagemViewModel>>(
                _ocorrenciaImagemRepositories.ObterPorOcorrencia(ocorrenciaId));
        }
    }
}