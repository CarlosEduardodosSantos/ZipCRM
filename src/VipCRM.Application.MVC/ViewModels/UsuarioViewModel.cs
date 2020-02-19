using System.ComponentModel.DataAnnotations;

namespace VipCRM.Application.MVC.ViewModels
{
    public class UsuarioViewModel
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Usuário")]
        public string User { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Senha")]
        public string Password { get; set; }
        [Display(Name = "Lembrar")]
        public bool RememberMe { get; set; }
        public int PerfilId { get; set; }
        public string Assinatura { get; set; }
        public string Imagem { get; set; }
    }
}