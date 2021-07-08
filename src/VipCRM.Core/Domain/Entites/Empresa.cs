namespace VipCRM.Core.Domain.Entites
{
    public class Empresa
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
    }
}