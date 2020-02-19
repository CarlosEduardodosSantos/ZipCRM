namespace VipCRM.Application.MVC.ViewModels
{
    public class RequisicaoViewModel
    {
        public int RequisicaoId { get; set; }
        public int RequisicaoItemId { get; set; }
        public int ClienteId { get; set; }
        public int ProdutoId { get; set; }
        public string Produto { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Utilizado { get; set; }
        public decimal ValorVenda { get; set; }
    }
}