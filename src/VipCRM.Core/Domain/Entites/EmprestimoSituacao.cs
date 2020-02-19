using System;

namespace VipCRM.Core.Domain.Entites
{
    public class EmprestimoSituacao
    {
        public int EmprestimoSituacaoId { get; set; }
        public int EmprestimoId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataHora { get; set; }
        public int StatusId { get; set; }
        public string Comentario { get; set; }
    }
}