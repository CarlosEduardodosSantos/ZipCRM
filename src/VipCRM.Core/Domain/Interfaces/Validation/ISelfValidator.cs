using VipCRM.Core.Domain.ValueObjects;

namespace VipCRM.Core.Domain.Interfaces.Validation
{
    public interface ISelfValidator
    {
        ValidationResult ResultadoValidacao { get; }
        bool IsValid(); 
    }
}