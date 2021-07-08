using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace VipCRM.Application.MVC.ViewModels
{
    public class OcorrenciaViewModel
    {
        public OcorrenciaViewModel()
        {
            DataInicioVip = DateTime.Now;
            DataFimVip = DateTime.Now;
        }

        [DisplayName("Nº Do Ocorrência")]
        public string OcorrenciaId { get; set; }

        public int ClienteId { get; set; }
        public int UsuarioId { get; set; }
        public int RoteiroId { get; set; }
        public int Sequencia { get; set; }
        public DateTime DhOcorrencia { get; set; }
        public DateTime DhRoteiro { get; set; }
        public int QuantidadeDias { get; set; }
        public int StatusId { get; set; }
        public int Prioridade { get; set; }
        public string Contato { get; set; }
        public string Comentario { get; set; }
        private string _problema;
        public string Problema
        {
            get => RemoveRTFFormatting(_problema);
            set => _problema = value;
        }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Solução obrigatória")]
        [StringLength(8000, ErrorMessage = "O {0} deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DisplayName("Solução")]
        public string Solucao { get; set; }

        public string ImageRat { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        [DisplayName("Data Inicio")]
        public DateTime DataInicioVip { get; set; }


        [DisplayName("Data Final")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime DataFimVip { get; set; }

        [DisplayName("Pendência")]
        public bool IsPendente { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "O {0} deve ter pelo menos {2} caracteres.", MinimumLength = 1)]
        [DisplayName("Nº Do RAT")]
        public string Rat { get; set; }

        public ClienteViewModel Cliente { get; set; }
        public UsuarioViewModel Usuario { get; set; }
        public int Avaliacao { get; set; }
        public float Distancia { get; set; }
        public bool Deslocamento => VerificaDeslocamento();
        public decimal ValorQuilometro => VerificaValorQuilometro();
        public string Veiculo { get; set; }
        public bool IsInterno => VerificaOcorrenciaInterna();
        private string RemoveRTFFormatting(string rtfContent)
        {
            if (string.IsNullOrEmpty(rtfContent)) return "";

            rtfContent = rtfContent.Trim();


            Regex rtfRegEx = new Regex("({\\\\)(.+?)(})|(\\\\)(.+?)(\\b)",
                RegexOptions.IgnoreCase
                | RegexOptions.Multiline
                | RegexOptions.Singleline
                | RegexOptions.ExplicitCapture
                | RegexOptions.IgnorePatternWhitespace
                | RegexOptions.Compiled
            );
            string output = rtfRegEx.Replace(rtfContent, string.Empty);
            output = Regex.Replace(output, @"\}", string.Empty); //replacing the remaining braces


            return output.Remove(output.Length - 1); //to trim last char (line end)
        }

        private bool VerificaDeslocamento()
        {
            if (VerificaOcorrenciaInterna()) return false;

            if (Cliente == null) return false;

            if (Cliente?.Cep?.Length < 5) return false;

            var faixaCep = int.Parse(Cliente.Cep.Substring(0, 5));
            return !(faixaCep >= 14000 && faixaCep <= 14114);
        }

        private decimal VerificaValorQuilometro()
        {
            //Verificar o veiculo se é moto ou carro

            return decimal.Parse("1,90");
        }

        private bool VerificaOcorrenciaInterna()
        {
            return Veiculo == "ADM";
        }
    }


}