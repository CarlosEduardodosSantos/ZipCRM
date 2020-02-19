using System;
using VipCRM.Core.Domain.Interfaces.Validation;
using VipCRM.Core.Domain.ValueObjects;

namespace VipCRM.Core.Domain.Entites
{
    public class Alerta : ISelfValidator
    {
        public Alerta()
        {
            AlertaId = Guid.NewGuid();
        }
        public Guid AlertaId { get; set; }
        public int UsuarioId { get; set; }
        public int Situacao { get; set; }
        public DateTime DataHora { get; set; }
        public DateTime DhConfirm { get; set; }
        public string Subject { get; set; }
        public virtual Usuario Usuario { get; set; }
        public ValidationResult ResultadoValidacao { get; private set; }
        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}