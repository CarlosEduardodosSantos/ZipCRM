using System;

namespace VipCRM.Core.Domain.Entites
{
    public class EmprestimoSolicitacao
    {
        public int EmprestimoId { get; set; }
        public int OcorrenciaId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataHora { get; set; }
        public int StatusId { get; set; }
        public int EquipamentoId { get; set; }
        public int EquipamentoTipo { get; set; }
 
    }
}