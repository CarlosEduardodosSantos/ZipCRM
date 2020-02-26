using System.Collections.Generic;
using System.Linq;
using Dapper;
using VipCRM.Core.Domain.Entites;
using VipCRM.Data.Interface.Repositories;

namespace VipCRM.Data.Repositories
{
    public class AbastecimentoRepository : RepositoryBase, IAbastecimentoRepository
    {
        public override string GetSelectBasic()
        {
            return @"select 
	                    Nro as AbastecimentoId,
	                    Usuario as UsuarioId,
	                    FROTA_MOVIMENTACAO.Data,
	                    FROTA_MOVIMENTACAO.Valor,
	                    Frota_1.Codigo as VeiculoId,
	                    Frota_1.Nome as Descricao,
	                    Frota_1.kmatual as Quilometragem
	                    from FROTA_MOVIMENTACAO
                    Left Join Frota_1 On FROTA_MOVIMENTACAO.VEICULO = Frota_1.Codigo";
        }

        public override string GetUpdateBasic()
        {
            throw new System.NotImplementedException();
        }

        public override string GetInsertBasic()
        {
            throw new System.NotImplementedException();
        }

        public Abastecimento ObterPorId(int abastecimentoId)
        {
            var sql = $"{GetSelectBasic()}  Where Nro = @abastecimentoId";

            using (var conn = Connection)
            {
                conn.Open();
                var abastecimento = conn.Query<Abastecimento, Veiculo, Abastecimento>(sql,
                    (a, v) =>
                    {
                        a.Veiculo = v;
                        return a;
                    }, new { abastecimentoId }, splitOn: "AbastecimentoId, VeiculoId").FirstOrDefault();

       
                conn.Close();

                return abastecimento;
            }
        }

        public IEnumerable<Abastecimento> ObterPorUsuarioId(int usuarioId)
        {
            var sql = $"{GetSelectBasic()}  Where REQUISITANTE = @usuarioId And Isnull(kmabastecido,0) = 0";

            using (var conn = Connection)
            {
                conn.Open();
                var abastecimentos = conn.Query<Abastecimento, Veiculo, Abastecimento>(sql,
                    (a, v) =>
                    {
                        a.Veiculo = v;
                        return a;
                    }, new { usuarioId } , splitOn: "AbastecimentoId, VeiculoId");

                conn.Close();

                return abastecimentos;
            }
        }

        public void Alterar(Abastecimento abastecimento)
        {
            var sql = @"Update FROTA_MOVIMENTACAO Set
                            kmabastecido = @kmabastecido,
                            vlabastecido = @vlabastecido
                        Where Nro = @abastecimentoId";

            var sql2 = @"Update Frota_1 Set kmatual = @kmatual Where Codigo = @VeiculoId";

            using (var conn = Connection)
            {
                conn.Open();
                conn.Query(sql,
                    new
                    {
                        kmabastecido = abastecimento.AbastecimentoKm,
                        vlabastecido = abastecimento.AbastecimetnoValor,
                        abastecimento.AbastecimentoId
                    });

                conn.Query(sql2, new { kmatual = abastecimento.AbastecimentoKm, abastecimento.Veiculo.VeiculoId });

                conn.Close();
            }
        }
    }
}