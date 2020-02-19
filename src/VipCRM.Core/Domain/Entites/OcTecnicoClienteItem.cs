using System;

namespace VipCRM.Core.Domain.Entites
{
    public class OcTecnicoClienteItem
    {
        public int ClienteId { get; set; }
        public string ClienteNome { get; set; }
        public DateTime DataAgendamento { get; set; }
        public string Situacao { get; set; }
        public int UsuarioId { get; set; }
        public DateTime? DataInicioVip { get; set; }
        public DateTime? DataFimVip { get; set; }
        public DateTime DataRoteiro { get; set; }
        public DateTime HoraRoteiro { get; set; }
        public string Veiculo { get; set; }
    }
}