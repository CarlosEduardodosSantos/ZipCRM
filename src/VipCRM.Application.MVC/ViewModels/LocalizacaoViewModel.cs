using System;

namespace VipCRM.Application.MVC.ViewModels
{
    public class LocalizacaoViewModel
    {
        public int OcorrenciaId { get; set; }
        public DateTime DataHora { get; set; }
        public string Nome { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Endereco { get; set; }
        public string DataToString => DataHora.ToString("dd-MM-yyyy HH:mm");
        public LocalizarTipoOperacaoEnum TipoOperacao { get; set; }
        public string DescricaoOperacao => TipoOperacao.ToString();
    }
}