namespace VipCRM.Application.MVC.ViewModels
{
    public class EmpresaViewModel
    {
        public int EmpresaId { get; set; }
        public string NomeFantasia { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Fone { get; set; }
        public string Logo { get; set; }
        public string Endereco => $"{Logradouro}, {Numero} - {Bairro}";
    }
}