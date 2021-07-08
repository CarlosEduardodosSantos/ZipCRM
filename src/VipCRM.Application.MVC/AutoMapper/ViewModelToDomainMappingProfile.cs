using AutoMapper;
using VipCRM.Application.MVC.ViewModels;
using VipCRM.Core.Domain.Entites;

namespace VipCRM.Application.MVC.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<OcorrenciaViewModel, Ocorrencia>();
            Mapper.CreateMap<ClienteViewModel, Cliente>();
            Mapper.CreateMap<UsuarioViewModel, Usuario>();
            Mapper.CreateMap<RoleViewModel, Role>();
            Mapper.CreateMap<AgendaViewModel, Agenda>();
            Mapper.CreateMap<OcorrenciaImagemViewModel, OcorrenciaImagem>();
            Mapper.CreateMap<OcorrenciaMonitorViewModel, OcorrenciaMonitor>();
            Mapper.CreateMap<DemandaOcorrenciaViewModel, DemandaOcorrencia>();
            Mapper.CreateMap<OcorrenciaTotalizadorViewModel, OcorrenciaTotalizador>();
            Mapper.CreateMap<IniciaOcorrenciaViewModel, IniciaOcorrencia>();
            Mapper.CreateMap<FinalizaOcorrenciaViewModel, FinalizaOcorrencia>();
            Mapper.CreateMap<LocalizacaoViewModel, Localizacao>();
            Mapper.CreateMap<UsuarioImagemViewModel, UsuarioImagem>();
            Mapper.CreateMap<RequisicaoViewModel, Requisicao>();
            Mapper.CreateMap<DeslocamentoViewModel, Deslocamento>();
            Mapper.CreateMap<OrcamentoRankingViewModel, OrcamentoRanking>();
            Mapper.CreateMap<OrcamentoEquipeViewModel, OrcamentoEquipe>();
            Mapper.CreateMap<OrcamentoViewModel, Orcamento>();
            Mapper.CreateMap<OrcamentoTotalizarViewModel, OrcamentoTotalizar>();
            Mapper.CreateMap<SabadosMonitorViewModel, SabadosMonitor>();
            Mapper.CreateMap<AbastecimentoViewModel, Abastecimento>();
            Mapper.CreateMap<VeiculoViewModel, Veiculo>();
            Mapper.CreateMap<EmpresaViewModel, Empresa>();
        }
    }
}