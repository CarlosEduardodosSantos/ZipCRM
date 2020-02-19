using VipCRM.Core.Domain.ValueObjects;

namespace VipCRM.Core.Domain.Interfaces.Validation
{
    public interface IFiscal<in TEntity>
    {
        ValidationResult Validar(TEntity entity);
    }
}
