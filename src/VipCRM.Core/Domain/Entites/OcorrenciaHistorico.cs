using System;

namespace VipCRM.Core.Domain.Entites
{
    public class OcorrenciaHistorico
    {
        public int OcorrenciaHistoricoId { get; set; }
        public int OcorrenciaId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFinal { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }
    }
}