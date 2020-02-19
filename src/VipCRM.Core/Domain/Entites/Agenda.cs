using System;

namespace VipCRM.Core.Domain.Entites
{
    public class Agenda
    {
        public int AgendaId { get; set; }
        public int UsuarioId { get; set; }
        public int ClienteId { get; set; }
        public string ClienteNome { get; set; }
        public DateTime DataHoraAgedado { get; set; }
        public string Acao { get; set; }
        public string AcaoDescricao { get; set; }
        public string Conclusao { get; set; }
        public DateTime DataHoraConclusao { get; set; }
        public int Prioridade { get; set; }
        
    }
}