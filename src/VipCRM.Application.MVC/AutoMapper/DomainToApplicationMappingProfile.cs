using AutoMapper;
using VipCRM.Application.MVC.Validation;
using VipCRM.Core.Domain.ValueObjects;

namespace VipCRM.Application.MVC.AutoMapper
{
    public class DomainToApplicationMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToApplicationMapping"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<ValidationError, ValidationAppError>();
            Mapper.CreateMap<ValidationResult, ValidationAppResult>();
        }
    }
}