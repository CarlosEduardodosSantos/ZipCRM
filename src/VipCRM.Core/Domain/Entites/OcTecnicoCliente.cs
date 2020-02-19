using System.Collections.Generic;

namespace VipCRM.Core.Domain.Entites
{
    public class OcTecnicoCliente
    {
        public int UsuarioId { get; set; }
        public string UsuarioNome { get; set; }
        public string Imagem { get; set; }
        public IEnumerable<OcTecnicoClienteItem> TecnicoClienteItem { get; set; }  
    }
}