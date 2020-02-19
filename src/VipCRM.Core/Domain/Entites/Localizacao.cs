using System;

namespace VipCRM.Core.Domain.Entites
{
    public class Localizacao
    {
        public int OcorrenciaId { get; set; }
        public DateTime DataHora { get; set; }
        public string Nome { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int TipoOperacao { get; set; }
        public string Endereco { get; set; }
    }
}