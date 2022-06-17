using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VipCRM.Application.MVC.ViewModels
{
    public class IncluirOcorrenciaViewModels
    {
        public int UsuarioID { get; set; }
        public int ClienteID { get; set; }
        public string Problema { get; set; }
        public string OcorrenciaGUID { get; set; }
    }
}
