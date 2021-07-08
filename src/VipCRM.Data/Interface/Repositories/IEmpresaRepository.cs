using VipCRM.Core.Domain.Entites;

namespace VipCRM.Data.Interface.Repositories
{
    public interface IEmpresaRepository
    {
        Empresa GetById(string id);
    }
}