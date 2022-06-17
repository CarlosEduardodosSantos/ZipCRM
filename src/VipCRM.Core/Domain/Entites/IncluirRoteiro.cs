using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VipCRM.Core.Domain.Entites
{
    public class IncluirRoteiro
    {
        public int TecnicoId { get; set; }
        public int VeiculoId { get; set; }
        public string OcorrenciaGUID { get; set; }
    }
}
