using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VipCRM.Application.MVC.ViewModels
{
    public class FinalizaOcorrenciaViewModel
    {
        public int UsuarioId { get; set; }
        public string OcorrenciaId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Localizacao { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Solução obrigatória")]
        [StringLength(8000, ErrorMessage = "O {0} deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DisplayName("Solução")]
        public string Solucao { get; set; }
        public bool Pendencia { get; set; }
        public string Arquivo { get; set; }
        public string NumeroRat { get; set; }
        public string ImagemRat { get; set; }
        public int Avaliacao { get; set; }
        public float Distancia { get; set; }
        public ICollection<RequisicaoViewModel> Requisicoes { get; set; }
        public DeslocamentoViewModel Deslocamento { get; set; }
    }
}