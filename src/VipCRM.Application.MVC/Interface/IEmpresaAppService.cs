using VipCRM.Application.MVC.ViewModels;

namespace VipCRM.Application.MVC.Interface
{
    public interface IEmpresaAppService
    {
        EmpresaViewModel ObterPorId(string empresaId);
    }
}