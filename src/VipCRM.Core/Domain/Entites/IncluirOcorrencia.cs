using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VipCRM.Core.Domain.Entites
{
    public class IncluirOcorrencia
    {
        public int UsuarioID { get; set; }
        public int ClienteID { get; set; }
        public string Problema { get; set; }
        public string OcorrenciaGUID { get; set; }
    }
}
