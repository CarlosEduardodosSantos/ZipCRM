namespace VipCRM.Core.Domain.Entites
{
    public class IniciaOcorrencia
    {
        public int UsuarioId { get; set; }
        public string OcorrenciaId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Localizacao { get; set; }
    }
}