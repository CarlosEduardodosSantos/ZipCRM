using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using VipCRM.Core.Domain.Entites;
using VipCRM.Data.Interface.Repositories;

namespace VipCRM.Data.Repositories
{
    public class OrcamentoDashBoardRepository : RepositoryBase, IOrcamentoDashBoardRepository
    {
        public override string GetSelectBasic()
        {
            throw new NotImplementedException();
        }

        public override string GetUpdateBasic()
        {
            throw new NotImplementedException();
        }

        public override string GetInsertBasic()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrcamentoRanking> OrcamentoRankingPorData(DateTime dataInicio, DateTime dataFinal)
        {

            var sql = @"Select 
	                        Nome_Completo as Vendedor,
	                        ue_descricao as Equipe,
	                        Sum(TotalVenda) as Valor,
	                        Sum(Quantidade) as Quantidade
                        From (
	                        Select 
		                        Usuarios.Nome_Completo,
		                        usuario_equipe.ue_descricao,
		                        TotalVenda = (select Sum(total) From orc_2 Where Nro = orc_1.Nro),
		                        Count(orc_1.Nro) as Quantidade
	                        from Orc_1
	                        Left Join Usuarios On orc_1.vendedor = Usuarios.Codigo
	                        Left Join USUARIO_CAD On orc_1.vendedor = USUARIO_CAD.ID_USUARIO
	                        Left Join usuario_equipe On USUARIO_CAD.ue_codigo = usuario_equipe.ue_codigo
	                        Where ORC_MOBILE = 1 And Data Between @dataInicio And @dataFinal
	                        Group By
		                        Usuarios.Nome_Completo,
		                        usuario_equipe.ue_descricao,
		                        Orc_1.nro
                        )tmp
                        Group By 
	                        Nome_Completo,
	                        ue_descricao
                        Order By 
	                        Valor desc";

            using (var conn = Connection)
            {
                conn.Open();
                var rankins = conn.Query<OrcamentoRanking>(sql, new {dataInicio, dataFinal});
                conn.Close();
                return rankins;
            }
        }

        public IEnumerable<OrcamentoEquipe> RankingEquipePorData(DateTime dataInicio, DateTime dataFinal)
        {
            var sqlTotal = @"select Sum(total) From orc_2 Inner Join orc_1 On orc_1.nro = orc_2.nro
                                Where ORC_MOBILE = 1 And Data Between @dataInicio And @dataFinal";

            var sql = @"Select 
	                        ue_descricao as Equipe,
	                        Sum(Valor) as Valor,
	                        Sum(Quantidade) as Quantidade,
	                        percentual = Cast((Sum(Valor) * 100) / @valorTotal as decimal(18,2))
                        From (
	                        Select 
		                        usuario_equipe.ue_descricao,
		                        Valor = (select Sum(total) From orc_2 Where Nro = orc_1.Nro),
		                        Count(orc_1.Nro) as Quantidade
	                        from Orc_1
	                        Left Join USUARIO_CAD On orc_1.vendedor = USUARIO_CAD.ID_USUARIO
	                        Left Join usuario_equipe On USUARIO_CAD.ue_codigo = usuario_equipe.ue_codigo
	                        Where ORC_MOBILE = 1 And Data Between @dataInicio And @dataFinal
	                        Group By
		                        usuario_equipe.ue_descricao,
		                        Orc_1.nro
                        )tmp
                        Group By 
	                        ue_descricao
                        Order By 
	                        Valor desc
                        ";

            using (var conn = Connection)
            {
                conn.Open();
                var valorTotal = conn.Query<decimal>(sqlTotal, new { dataInicio, dataFinal }).FirstOrDefault();

                var rankins = conn.Query<OrcamentoEquipe>(sql, new { dataInicio, dataFinal, valorTotal });
                conn.Close();

                return rankins;
            }
        }

        public IEnumerable<Orcamento> UltimosOrcamentos(DateTime dataInicio, DateTime dataFinal, int page, int limit)
        {
            var sql = @"Select 
	                        Usuarios.Nome_Completo as Vendedor,
	                        usuario_equipe.ue_descricao as Equipe,
	                        Orc_1.Nome as Cliente,
	                        Orc_1.Data,
	                        Valor = (select Sum(total) From orc_2 Where Nro = orc_1.Nro),
	                        ORC_HIST.Latitude,
	                        ORC_HIST.Longitude
                        from Orc_1
                        Left Join ORC_HIST On orc_1.nro = ORC_HIST.orchist_nro_orc
                        Left Join Usuarios On orc_1.vendedor = Usuarios.Codigo
                        Left Join USUARIO_CAD On orc_1.vendedor = USUARIO_CAD.ID_USUARIO
                        Left Join usuario_equipe On USUARIO_CAD.ue_codigo = usuario_equipe.ue_codigo
                        Where ORC_MOBILE = 1 And Data Between @dataInicio And @dataFinal
                        Order By nro desc, data desc
                        OFFSET @limit * (@page - 1) ROWS
                          FETCH NEXT @limit ROWS ONLY;";

            using (var conn = Connection)
            {
                conn.Open();
                var orcamentos = conn.Query<Orcamento>(sql, new { dataInicio, dataFinal, page, limit });
                conn.Close();
                return orcamentos;
            }
        }

        public OrcamentoTotalizar TotalizarPorData(DateTime dataInicio, DateTime dataFinal)
        {
            var sql = @"Select 
	                        TotalVenda =  Isnull((select Sum(total) From orc_2 
					                        Inner Join orc_1 On orc_1.nro = orc_2.nro 
					                        Where ORC_MOBILE = 1 And Data Between @dataInicio And @dataFinal),0),
	                        TotalCliente =Isnull((select count(*) From orc_2 
					                        Inner Join orc_1 On orc_1.nro = orc_2.nro
					                        Where ORC_MOBILE = 1 And Data Between @dataInicio And @dataFinal),0),
	                        TotalPagar = Isnull((select Sum(valor) From Contas
					                        Where tipo = 'P' and pgtodata is null And vencimento Between @dataInicio And @dataFinal),0),
	                        TotalReceber = Isnull((select Sum(valor) From Contas
					                        Where tipo = 'R' and pgtodata is null And vencimento Between @dataInicio And @dataFinal),0)";

            using (var conn = Connection)
            {
                conn.Open();
                var total = conn.Query<OrcamentoTotalizar>(sql, new { dataInicio, dataFinal }).FirstOrDefault();
                conn.Close();
                return total;
            }
        }
    }
}