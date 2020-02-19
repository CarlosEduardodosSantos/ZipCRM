
using System.Collections.Generic;

namespace VipCRM.Core.Domain.Entites
{
    public class FinalizaOcorrencia
    {
        public int UsuarioId { get; set; }
        public string OcorrenciaId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Localizacao { get; set; }
        public string Solucao { get; set; }
        public bool Pendencia { get; set; }
        public string Arquivo { get; set; }
        public string NumeroRat { get; set; }
        public string ImagemRat { get; set; }
        public int Avaliacao { get; set; }
        public float Distancia { get; set; }
        public ICollection<Requisicao> Requisicoes { get; set; }
        public Deslocamento Deslocamento { get; set; }
    }
}