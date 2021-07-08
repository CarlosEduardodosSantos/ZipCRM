using AutoMapper;
using VipCRM.Application.MVC.Interface;
using VipCRM.Application.MVC.ViewModels;
using VipCRM.Core.Domain.Entites;
using VipCRM.Data.Interface.Repositories;

namespace VipCRM.Application.MVC
{
    public class EmpresaAppService: IEmpresaAppService
    {
        private readonly IEmpresaRepository _empresaRepository;

        public EmpresaAppService(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }

        public EmpresaViewModel ObterPorId(string empresaId)
        {
            return Mapper.Map<Empresa, EmpresaViewModel>(_empresaRepository.GetById(empresaId));
        }
    }
}