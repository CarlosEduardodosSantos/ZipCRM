using System;
using System.Collections.Generic;

namespace VipCRM.Core.Domain.Entites
{
    public class Ocorrencia
    {
        public string OcorrenciaId { get; set; }
        public int ClienteId { get; set; }
        public int UsuarioId { get; set; }
        public int RoreitoId { get; set; }
        public DateTime DhOcorrencia { get; set; }
        public DateTime DhRoteiro { get; set; }
        public int QuantidadeDias { get; set; }
        public int StatusId { get; set; }
        public int Prioridade { get; set; }
        public string Contato { get; set; }
        public string Comentario { get; set; }
        public string Problema { get; set; }
        public string Solucao { get; set; }
        public string ImageRat { get; set; }
        public string Rat { get; set; }
        public Cliente Cliente { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime DataInicioVIP { get; set; }
        public DateTime DataFimVIP { get; set; }
        public IEnumerable<OcorrenciaHistorico> OcorrenciaHistoricos { get; set; }
        public string Veiculo { get; set; }
    }
}