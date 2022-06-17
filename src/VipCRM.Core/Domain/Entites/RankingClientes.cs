using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VipCRM.Core.Domain.Entites
{
    public class RankingClientes
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public int Qtde { get; set; }
        public decimal Percentual { get; set; }
        public int MediaMensal { get; set; }
        public string Data { get; set; }
    }
}
