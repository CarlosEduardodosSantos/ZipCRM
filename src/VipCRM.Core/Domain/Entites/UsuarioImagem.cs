namespace VipCRM.Core.Domain.Entites
{
    public class UsuarioImagem
    {
        public int ImagemId { get; set; }
        public int UsuarioId { get; set; }
        public string Imagem { get; set; }
        public string Assinatura { get; set; }
    }
}