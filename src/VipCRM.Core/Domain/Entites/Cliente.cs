namespace VipCRM.Core.Domain.Entites
{
    public class Cliente
    {
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
        public string Fone { get; set; }
    }
}