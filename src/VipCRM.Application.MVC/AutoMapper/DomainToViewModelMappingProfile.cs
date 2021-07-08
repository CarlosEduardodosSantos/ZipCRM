using AutoMapper;
using VipCRM.Application.MVC.ViewModels;
using VipCRM.Core.Domain.Entites;

namespace VipCRM.Application.MVC.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Ocorrencia, OcorrenciaViewModel>();
            Mapper.CreateMap<Cliente, ClienteViewModel>();
            Mapper.CreateMap<Usuario, UsuarioViewModel>();
            Mapper.CreateMap<Role, RoleViewModel>();
            Mapper.CreateMap<Agenda, AgendaViewModel>();
            Mapper.CreateMap<OcorrenciaImagem, OcorrenciaImagemViewModel>();
            Mapper.CreateMap<OcorrenciaMonitor, OcorrenciaMonitorViewModel>();

            Mapper.CreateMap<OcTecnicoCliente, OcTecnicoClienteViewModel>()
                .ForMember(dest => dest.TecnicoClienteItem, opt => opt.MapFrom(src => src.TecnicoClienteItem));
            Mapper.CreateMap<OcTecnicoClienteItem, OcTecnicoClienteItemViewModel>();
            Mapper.CreateMap<DemandaOcorrencia, DemandaOcorrenciaViewModel>();
            Mapper.CreateMap<OcorrenciaTotalizador, OcorrenciaTotalizadorViewModel>();
            Mapper.CreateMap<ResultOcorrenciaProgramacao, ResultOcorrenciaProgramacaoViewModel>();
            Mapper.CreateMap<OcorrenciaProgramacao, OcorrenciaProgramacaoViewModel>();
            Mapper.CreateMap<IniciaOcorrencia, IniciaOcorrenciaViewModel>();
            Mapper.CreateMap<FinalizaOcorrencia, FinalizaOcorrenciaViewModel>();
            Mapper.CreateMap<Localizacao, LocalizacaoViewModel>();
            Mapper.CreateMap<UsuarioImagem, UsuarioImagemViewModel>();
            Mapper.CreateMap<Requisicao, RequisicaoViewModel>();
            Mapper.CreateMap<Deslocamento, DeslocamentoViewModel>();
            Mapper.CreateMap<OrcamentoRanking, OrcamentoRankingViewModel>();
            Mapper.CreateMap<OrcamentoEquipe, OrcamentoEquipeViewModel>();
            Mapper.CreateMap<Orcamento, OrcamentoViewModel>();
            Mapper.CreateMap<OrcamentoTotalizar, OrcamentoTotalizarViewModel>();
            Mapper.CreateMap<SabadosMonitor, SabadosMonitorViewModel>();
            Mapper.CreateMap<Abastecimento, AbastecimentoViewModel>();
            Mapper.CreateMap<Veiculo, VeiculoViewModel>();
            Mapper.CreateMap<Empresa, EmpresaViewModel>();

        }
    }
}