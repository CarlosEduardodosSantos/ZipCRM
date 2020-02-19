using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VipCRM.Application.MVC.ViewModels
{
    public class AgendaViewModel
    {
        public int AgendaId { get; set; }
        public int UsuarioId { get; set; }
        public int ClienteId { get; set; }


        [DisplayName("Cliente")]
        [Required(ErrorMessage = "Cliente obrigatório")]
        public string ClienteNome { get; set; }

        [DisplayName("Data Agendamento")]
        [DataType(DataType.DateTime, ErrorMessage = "Não é um formato de data e hora !!!!")]
        [Required(ErrorMessage = "Data Agendamento é obrigatório")]
        public DateTime DataHoraAgedado { get; set; }
        

        [DataType(DataType.MultilineText)]
        [DisplayName("Ação")]
        [Required(ErrorMessage = "É necessario saber qual ação")]
        [StringLength(100, ErrorMessage = "O {0} deve ter pelo menos {2} caracteres.", MinimumLength = 4)]
        public string Acao { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("Descrição Ação")]
        [Required(ErrorMessage = "É necessario saber a descrição da ação")]
        [StringLength(100, ErrorMessage = "O {0} deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
        public string AcaoDescricao { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("Conclusão")]
        [Required(ErrorMessage = "É necessario saber qual a conclusão")]
        [StringLength(100, ErrorMessage = "O {0} deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
        public string Conclusao { get; set; }

        [DisplayName("Data Conclusão")]
        [Required(ErrorMessage = "Data Conclusão é obrigatório")]
        [DataType(DataType.DateTime, ErrorMessage = "Não é um formato de data e hora !!!!")]

        public DateTime DataHoraConclusao { get; set; }
        public int Prioridade { get; set; } 
    }
}