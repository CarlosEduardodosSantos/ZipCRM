using System.Collections.Generic;

namespace VipCRM.Application.MVC.ViewModels
{
    public class OcTecnicoClienteViewModel
    {
        public int UsuarioId { get; set; }
        public string UsuarioNome { get; set; }
        public string Imagem { get; set; }
        public IEnumerable<OcTecnicoClienteItemViewModel> TecnicoClienteItem { get; set; }
        
    }
}