using System;

namespace VipCRM.Application.MVC.ViewModels
{
    public class OcorrenciaImagemViewModel
    {
        public int OcorrenciaImagemId { get; set; }
        public int OcorrenciaId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataHora { get; set; }
        public string ImagemTipo { get; set; }
        public string Imagem { get; set; }
        public string Tipo { get; set; } 
    }
}