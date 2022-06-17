using VipCRM.Core.Domain.Validation.Documentos;

namespace VipCRM.Core.Domain.Entites
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string User { get; set; }
        public string Password { get; set ; }
        public int PerfilId { get; set; }
        public string Imagem { get; set; }
        public string Assinatura { get; set; }
        public int isAdmin { get; set; }

    }
    
}