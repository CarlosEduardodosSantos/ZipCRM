using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VipCRM.Application.MVC.ViewModels
{
    public class OcorrenciaClienteViewModel
    {
        public OcorrenciaClienteViewModel()
        {
            DataInicioVip = DateTime.Now;
            DataFimVip = DateTime.Now;
        }
        public string OcorrenciaId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DhOcorrencia { get; set; }
        public DateTime DhRoteiro { get; set; }
        public int QuantidadeDias { get; set; }
        public int StatusId { get; set; }
        public int Prioridade { get; set; }
        public string Comentario { get; set; }
        public string Problema { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Solução obrigatória")]
        [StringLength(100, ErrorMessage = "O {0} deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
        public string Solucao { get; set; }
        public string ImageRat { get; set; }
        
        [DisplayName("Data Inicio")]
        [Required(ErrorMessage = "Data inicial é obrigatório")]
        [DataType(DataType.Date, ErrorMessage = "Não é um formato de data e hora !!!!")]
        public DateTime DataInicioVip { get; set; }

        [Required(ErrorMessage = "Data final é obrigatório")]
        [DataType(DataType.Date, ErrorMessage = "Não é um formato de data e hora !!!!")]
        [DisplayName("Data Final")]
        public DateTime DataFimVip { get; set; }

        [DisplayName("Pendencia")]
        public bool IsPendente { get; set; }
        [Required]
        public string Rat { get; set; }
 

        //Clientes

        public int ClienteId { get; set; }
        public string Cnpj { get; set; }
        public string Nome { get; set; }
        public string RazaoSocial { get; set; }
        public string Cep { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
    }
}