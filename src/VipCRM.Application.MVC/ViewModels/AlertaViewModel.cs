using System;
using VipCRM.Core.Domain.Entites;

namespace VipCRM.Application.MVC.ViewModels
{
    public class AlertaViewModel
    {
        public int AlertaId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataHora { get; set; }
        public DateTime DhConfirm { get; set; }
        public string Subject { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}