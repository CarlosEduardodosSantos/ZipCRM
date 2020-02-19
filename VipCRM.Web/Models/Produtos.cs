using System.Collections.Generic;
using System.Linq;

namespace VipCRM.Web.Models
{
    public class Produtos
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public int GrupoId { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public Grupo Grupo { get; set; }
        public IEnumerable<Produtos> GetProdutoByGrupo(int grupoId)
        {
            IEnumerable<Produtos> produtos = new List<Produtos>()
            {
                new Produtos()
                {
                    ProdutoId = 1,
                    Nome = "Estação Cheddar",
                    Descricao = "Pão de Hamburguer + Incomparável Hamburger Gourmet de Fiet Mingnon e Picanha + Queijo + Cheddar(Muito Chaddar) + Cebola ao Barbecue.",
                    Valor = 33,
                    GrupoId = 1
                },
                new Produtos()
                {
                    ProdutoId = 2,
                    Nome = "Estação Cream Cheese",
                    Descricao = "Com molho de tomate, mussarela e tomates",
                    Valor = 32,
                    GrupoId = 1,
                    Grupo = new Grupo(){Nome = "Tradicionais"}
                },
                new Produtos()
                {
                    ProdutoId = 3,
                    Nome = "Estação México",
                    Descricao = "Com molho de tomate, mussarela, linguiça calabresa, cebola e azeitonas",
                    Valor = 34,
                    GrupoId = 1,
                    Grupo = new Grupo(){Nome = "Tradicionais"}
                },
                new Produtos()
                {
                    ProdutoId = 4,
                    Nome = "Pizza Escarola",
                    Descricao = "Com molho de tomate, mussarela, escarola, tomate e azeitonas",
                    Valor = 33,
                    GrupoId = 1,
                    Grupo = new Grupo(){Nome = "Tradicionais"}
                },
                new Produtos()
                {
                    ProdutoId = 5,
                    Nome = "Estação Chichen ",
                    Descricao = "Com molho de tomate, mussarela, escarola, tomate e azeitonas",
                    Valor = 33,
                    GrupoId = 1,
                    Grupo = new Grupo(){Nome = "Tradicionais"}
                },
                new Produtos()
                {
                    ProdutoId = 6,
                    Nome = "Cheese Egg",
                    Descricao = "Com molho de tomate, mussarela, quijo e goiabada",
                    Valor = 22,
                    GrupoId = 2
                },
                new Produtos()
                {
                    ProdutoId = 7,
                    Nome = "Cheese Burguer",
                    Descricao = "Com molho de tomate, banana e chocolate",
                    Valor = 26,
                    GrupoId = 2
                }
             

            };

            return produtos.Where(w=> w.GrupoId == grupoId);
        }
    }

    public class Grupo
    {
        public int GrupoId { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }
        public virtual IEnumerable<Produtos> Produtos { get; set; }
        public virtual IEnumerable<Complemento> Complemento { get; set; }

        public IEnumerable<Grupo> GetGrupos()
        {
            var produtos = new Produtos();
            var complementos = new Complemento();
            var grupos = new List<Grupo>()
            {
               new Grupo()
               {
                   GrupoId = 1,
                   Nome = "Estação Gourmet",
                   Imagem = "chamadagourme1.jpg",
                   Produtos = produtos.GetProdutoByGrupo(1),
                   Complemento = complementos.GetComplemento(1)
               },
               new Grupo()
               {
                   GrupoId = 2,
                   Nome = "Estação Tradicional",
                   Imagem = "chamadagourme2.jpg",
                   Produtos = produtos.GetProdutoByGrupo(2),
                   Complemento = complementos.GetComplemento(1)
               }
            };

            return grupos;
        }
    }

    public class Complemento
    {
        public int ComplementoId { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public int GrupoId { get; set; }

        public IEnumerable<Complemento> GetComplemento(int grupoId)
        {
            var complementos = new List<Complemento>()
            {
                new Complemento()
                {
                    ComplementoId = 1,
                    Nome = "Queijo",
                    Valor = (decimal)1.50
                },
                new Complemento()
                {
                    ComplementoId = 2,
                    Nome = "Bacon",
                    Valor = (decimal)1.50
                },
                new Complemento()
                {
                    ComplementoId = 1,
                    Nome = "Ovo",
                    Valor = (decimal)1.50
                },
                new Complemento()
                {
                    ComplementoId = 1,
                    Nome = "Chaddar",
                    Valor = (decimal)1.50
                }
            };

            return complementos;
        }
    }
}