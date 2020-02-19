using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using VipCRM.Core.Domain.Entites;
using VipCRM.Core.Domain.ValueObjects;
using VipCRM.Data.Interface.Repositories;

namespace VipCRM.Data.Repositories
{
    public class OcorrenciaRepositories : RepositoryBase, IOcorrenciaRepositories
    {

        public Ocorrencia ObterOcorrenciaId(int id)
        {
            var sql = new StringBuilder().Append(GetSelectBasic());
            sql.Append(" Where Ocorrencia.ocorrencia= @sid");
            sql.Append(Environment.NewLine);
            sql.Append(GetSelectBasic());
            sql.Append(" Where Ocorrencia.ocorrencia= @sid");

            using (IDbConnection cn = Connection)
            {
                cn.Open();

                using (var mult = cn.QueryMultiple(sql.ToString(), new { sid = id }))
                {
                    var ocorrencia = mult.Read<Ocorrencia>().SingleOrDefault();
                    var cliente = mult.Read<Cliente>().SingleOrDefault();
                    cn.Close();

                    if (ocorrencia != null)
                    {
                        ocorrencia.Cliente = cliente;

                        return ocorrencia;
                    }
                }

            }
            return new Ocorrencia();

        }

        public IEnumerable<Ocorrencia> ObterOcorrenciasPorUsuario(int usuarioId)
        {
            var sql = new StringBuilder().Append(GetSelectBasic());
            sql.Append(" Where Ocorrencia.Ok_Data is null And Ocorrencia.enc_tecnico = @sid");
            using (IDbConnection cn = Connection)
            {
                cn.Open();

                //var multi = cn.QueryMultiple(sql.ToString(), new { sid = usuarioId });
                //var oc = multi.Read<Ocorrencia>();
                var ocorrencias = cn.Query<Ocorrencia>(sql.ToString(), new { sid = usuarioId });

                sql = new StringBuilder().Append(GetSelectBasic());
                sql.Append(" Where Ocorrencia.ocorrencia= @sid");
                foreach (var ocorrencia in ocorrencias)
                {
                    var cliente =
                        cn.Query<Cliente>(sql.ToString(), new { sid = ocorrencia.OcorrenciaId }).FirstOrDefault();
                    ocorrencia.Cliente = cliente;
                }

                cn.Close();

                return ocorrencias;
            }
        }

        public IEnumerable<Ocorrencia> ObterOcorrenciasPorUsuarioRoteiro(int usuarioId)
        {
            var sql = new StringBuilder().Append(GetSelectBasic());
            sql.Append(" Where Ocorrencia.data_roteiro is not null And Ocorrencia.Ok_Data is null");
            sql.Append(" And Ocorrencia.Age_Tecnico = @sid");
            sql.Append(" Order By Roteiro_1.Nro");

            using (IDbConnection cn = Connection)
            {
                cn.Open();

                //var multi = cn.QueryMultiple(sql.ToString(), new { sid = usuarioId });
                //var oc = multi.Read<Ocorrencia>();
                var ocorrencias = cn.Query<Ocorrencia>(sql.ToString(), new { sid = usuarioId });

                sql = new StringBuilder().Append(GetSelectBasic());
                sql.Append(" Where Ocorrencia.ocorrencia= @sid");
                foreach (var ocorrencia in ocorrencias)
                {
                    var cliente =
                        cn.Query<Cliente>(sql.ToString(), new { sid = ocorrencia.OcorrenciaId }).FirstOrDefault();
                    ocorrencia.Cliente = cliente;
                }

                cn.Close();

                return ocorrencias;
            }
        }

        public IEnumerable<Ocorrencia> ObterOcorrenciasPorUsuarioIniciada(int usuarioId)
        {
            var sql = new StringBuilder().Append(GetSelectBasic());
            sql.Append(" Where Ocorrencia.Ok_Data is null");
            sql.Append(" And Ocorrencia.DataInicioVip is not null");
            sql.Append(" And (Ocorrencia.Age_Tecnico = @sid Or Ocorrencia.enc_tecnico = @sid)");
            using (IDbConnection cn = Connection)
            {
                cn.Open();

                var ocorrencias = cn.Query<Ocorrencia>(sql.ToString(), new { sid = usuarioId });

                sql = new StringBuilder().Append(GetSelectBasic());
                sql.Append(" Where Ocorrencia.ocorrencia= @sid");
                foreach (var ocorrencia in ocorrencias)
                {
                    var cliente =
                        cn.Query<Cliente>(sql.ToString(), new { sid = ocorrencia.OcorrenciaId }).FirstOrDefault();
                    ocorrencia.Cliente = cliente;
                }

                cn.Close();

                return ocorrencias;
            }
        }

        public bool ExisteOcorrenciasIniciadaNaoFinalizadas(int usuarioId)
        {
            var sql = new StringBuilder().Append(GetSelectBasic());
            sql.Append(" Where Ocorrencia.DataInicioVIP is not null And Ocorrencia.Ok_Data is null");
            sql.Append(" And (Ocorrencia.Age_Tecnico = @sid Or Ocorrencia.enc_tecnico = @sid)");

            using (IDbConnection cn = Connection)
            {
                cn.Open();

                var ocorrencias = cn.Query<Ocorrencia>(sql.ToString(), new { sid = usuarioId }).Any();

                cn.Close();

                return false;
            }
        }


        public ValidationResult IniciarOcorrencia(IniciaOcorrencia iniciaOcorrencia)
        {
            var param = new DynamicParameters();

            param.Add("@ocorrenciaId", iniciaOcorrencia.OcorrenciaId);
            param.Add("@DataFinal", DateTime.Now);
            param.Add("@Latitude", iniciaOcorrencia.Latitude);
            param.Add("@Longitude", iniciaOcorrencia.Longitude);
            param.Add("@Localizacao", iniciaOcorrencia.Localizacao);
            param.Add("@UsuarioId", iniciaOcorrencia.UsuarioId);


            using (IDbConnection cn = Connection)
            {
                cn.Open();
                cn.Execute("spIniciarOcorrencia", param, commandType: CommandType.StoredProcedure);
                cn.Close();

                return new ValidationResult();
            }

        }

        public ValidationResult FinalizaOcorrencia(FinalizaOcorrencia finalizaOcorrencia)
        {
            var param = new DynamicParameters();

            param.Add("@ocorrenciaId", finalizaOcorrencia.OcorrenciaId);
            param.Add("@UsuarioId", finalizaOcorrencia.UsuarioId);
            param.Add("@Rat", finalizaOcorrencia.NumeroRat);
            param.Add("@DataFinal", DateTime.Now);
            param.Add("@Solucao", finalizaOcorrencia.Solucao);
            param.Add("@ImagemRat", finalizaOcorrencia.ImagemRat);
            param.Add("@IsPendente", finalizaOcorrencia.Pendencia);
            param.Add("@PdfFile", finalizaOcorrencia.Arquivo);
            param.Add("@Latitude", finalizaOcorrencia.Latitude);
            param.Add("@Longitude", finalizaOcorrencia.Longitude);
            param.Add("@Localizacao", finalizaOcorrencia.Localizacao);
            param.Add("@Avaliacao", finalizaOcorrencia.Avaliacao);


            using (IDbConnection cn = Connection)
            {
                cn.Open();
                cn.Execute("spFinalizaOcorrencia", param, commandType: CommandType.StoredProcedure);
                cn.Close();


            }

            if (finalizaOcorrencia.Requisicoes != null)
            {
                foreach (var requisicoe in finalizaOcorrencia.Requisicoes)

                {
                    if (requisicoe.Utilizado == 0) continue;

                    var sql = new StringBuilder();
                    sql.AppendLine("Insert Into OcorrenciaRequisicoes(OcorrenciaId, DataHora, UsuarioId, ");
                    sql.AppendLine("ClienteId, ProdutoId, Quantidade) Values(");
                    sql.AppendLine("@OcorrenciaId, @DataHora, @UsuarioId, @ClienteId, @ProdutoId, @Quantidade)");

                    var paramReq = new DynamicParameters();
                    paramReq.Add("@ocorrenciaId", finalizaOcorrencia.OcorrenciaId);
                    paramReq.Add("@DataHora", DateTime.Now);
                    paramReq.Add("@UsuarioId", finalizaOcorrencia.UsuarioId);
                    paramReq.Add("@ClienteId", requisicoe.ClienteId);
                    paramReq.Add("@ProdutoId", requisicoe.ProdutoId);
                    paramReq.Add("@Quantidade", requisicoe.Utilizado);

                    using (IDbConnection cn = Connection)
                    {
                        cn.Open();
                        cn.Execute(sql.ToString(), paramReq);
                        cn.Close();
                    }

                }
            }

            if (finalizaOcorrencia.Deslocamento != null)
            {
                if (finalizaOcorrencia.Deslocamento.Quilometragem > 0)
                {
                    var sqlDesl = new StringBuilder();
                    sqlDesl.AppendLine("Insert Into OcorrenciaDeslocamentos(OcorrenciaId, DataHora, UsuarioId, ");
                    sqlDesl.AppendLine("ValorDeslocamento, ValorPedagio, ValorAlimentacao, ValorHospedagem, ValorOutros) Values(");
                    sqlDesl.AppendLine("@OcorrenciaId, @DataHora, @UsuarioId, @ValorDeslocamento, @ValorPedagio, @ValorAlimentacao, @ValorHospedagem, @ValorOutros)");

                    var paramDesl = new DynamicParameters();
                    paramDesl.Add("@ocorrenciaId", finalizaOcorrencia.OcorrenciaId);
                    paramDesl.Add("@DataHora", DateTime.Now);
                    paramDesl.Add("@UsuarioId", finalizaOcorrencia.UsuarioId);
                    paramDesl.Add("@ValorDeslocamento", finalizaOcorrencia.Deslocamento.Quilometragem);
                    paramDesl.Add("@ValorPedagio", finalizaOcorrencia.Deslocamento.ValorPedagio);
                    paramDesl.Add("@ValorAlimentacao", finalizaOcorrencia.Deslocamento.ValorAlimentacao);
                    paramDesl.Add("@ValorHospedagem", finalizaOcorrencia.Deslocamento.ValorHospedagem);
                    paramDesl.Add("@ValorOutros", finalizaOcorrencia.Deslocamento.ValorOutros);

                    using (IDbConnection cn = Connection)
                    {
                        cn.Open();
                        cn.Execute(sqlDesl.ToString(), paramDesl);
                        cn.Close();
                    }
                }

            }




            return new ValidationResult();
        }

        public IEnumerable<Ocorrencia> ObterOcorrenciasPorPesquisa(int usuarioId, string pesquisa)
        {
            var sql = new StringBuilder().Append(GetSelectBasic());
            sql.Append(" Where Ocorrencia.Ok_Data is null And Ocorrencia.Age_Tecnico = @sid");
            sql.Append(" And clientes.nome like @spesquisa");


            using (IDbConnection cn = Connection)
            {
                cn.Open();

                //var multi = cn.QueryMultiple(sql.ToString(), new { sid = usuarioId });
                //var oc = multi.Read<Ocorrencia>();
                var ocorrencias = cn.Query<Ocorrencia>(sql.ToString(),
                    new { sid = usuarioId, spesquisa = "%" + pesquisa + "%" });

                sql = new StringBuilder().Append(GetSelectBasic());
                sql.Append(" Where Ocorrencia.ocorrencia= @sid");
                foreach (var ocorrencia in ocorrencias)
                {
                    var cliente =
                        cn.Query<Cliente>(sql.ToString(), new { sid = ocorrencia.OcorrenciaId }).FirstOrDefault();
                    ocorrencia.Cliente = cliente;
                }

                cn.Close();

                return ocorrencias;
            }
        }

        public IEnumerable<Ocorrencia> ObterOcorrenciasPorUsuarioFinalizadas(int usuarioId, int dias)
        {
            var sql = new StringBuilder().Append(GetSelectBasic());
            sql.Append(" Where datediff(day, Ocorrencia.Ok_Data, getdate()) <= @sdias And DataFimVip is not null And  Ocorrencia.Depto <> 4 And  Ocorrencia.Veiculo <> 'ADM' ");
            sql.Append(" And (@sid = 0 Or Ocorrencia.Age_Tecnico = @sid Or Ocorrencia.enc_tecnico = @sid)");
            sql.Append(" Order By Ocorrencia.Ok_Data desc");


            using (IDbConnection cn = Connection)
            {
                cn.Open();

                //var multi = cn.QueryMultiple(sql.ToString(), new { sid = usuarioId });
                //var oc = multi.Read<Ocorrencia>();
                var ocorrencias = cn.Query<Ocorrencia>(sql.ToString(),
                    new { sid = usuarioId, sdias = dias });

                sql = new StringBuilder().Append(GetSelectBasic());
                sql.Append(" Where Ocorrencia.ocorrencia= @sid");
                foreach (var ocorrencia in ocorrencias)
                {
                    var cliente =
                        cn.Query<Cliente>(sql.ToString(), new { sid = ocorrencia.OcorrenciaId }).FirstOrDefault();
                    ocorrencia.Cliente = cliente;
                }

                cn.Close();

                return ocorrencias;
            }
        }

        public IEnumerable<Requisicao> ObterResqRequisicoesPorCliente(int usuarioId, int clienteId)
        {
            var sql = new StringBuilder().AppendLine("Select");
            sql.AppendLine("REQ1.REQ1_NRO as RequisicaoId,");
            sql.AppendLine("REQ2.REQ2_ID as RequisicaoItemId,");
            sql.AppendLine("REQ1.REQ1_CLIENTE as ClienteId,");
            sql.AppendLine("Produtos.codigo as ProdutoId,");
            sql.AppendLine("produtos.Descricao as Produto,");
            sql.AppendLine("REQ2.REQ2_QTDE as Quantidade,");
            sql.AppendLine("Cast(PRODUTOS.vlVenda as Decimal(18,2)) as ValorVenda");
            sql.AppendLine("From REQ1");
            sql.AppendLine("Inner Join REQ2 On REQ1.REQ1_NRO = REQ2.REQ1_NRO");
            sql.AppendLine("LEFT JOIN PRODUTOS ON REQ2.REQ2_PROD = PRODUTOS.CODIGO");
            sql.AppendLine("Where REQ1.REQ1_STATUS = 'P'");
            sql.AppendLine("And REQ1.REQ1_REQ = @usuarioId And REQ1.REQ1_CLIENTE = @clienteId");

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var requisicoes = cn.Query<Requisicao>(sql.ToString(), new { usuarioId, clienteId });
                cn.Close();

                return requisicoes;
            }
        }

        public string ObterFilePdf(int ocorrenciaId)
        {
            var sql = "select Imagem from ocorrenciaImagens where ocorrenciaId = @ocorrenciaId And Tipo = 1";
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var file = cn.Query<string>(sql, new { ocorrenciaId }).FirstOrDefault();
                cn.Close();

                return file;
            }

        }

        public override string GetSelectBasic()
        {
            return @"select
	                Ocorrencia.ocorrencia as OcorrenciaId,
                    --Roteiro_2.nro as RoreitoId,
	                Ocorrencia.cliente as ClienteId,
	                Ocorrencia.Age_Tecnico as UsuarioId,
	                Ocorrencia.data as DataOcorrencia,
                    Ocorrencia.Ok_Nro as Rat,
	                datediff(day, Ocorrencia.data, getdate()) as QuantidadeDias,
	                Ocorrencia.data as DhOcorrencia,
	                Ocorrencia.data_roteiro as DhRoteiro,
                    Ocorrencia.Contato as Contato,
	                case
		                when (isnull(Ocorrencia.ok_data, 0) > 0)		then 3  
		                when (isnull(Ocorrencia.age_tecnico, 0) > 0)	and (isnull(ok_data, 0) = 0) then 2 
		                else 0
	                end as StatusId,
	                case
		                when Ocorrencia.prioridade = 'alta'			then 0
		                when Ocorrencia.prioridade = 'media'		then 1
		                else 2
	                end as Prioridade,
	                Ocorrencia.problema as Comentario,
	                Ocorrencia.problema_desc as Problema,
                    Ocorrencia.Solucao_desc as Solucao,
                    Ocorrencia.DataInicioVIP,
	                Ocorrencia.DataFimVIP,

	                --clientes
	                clientes.cnpj as Cnpj,
	                clientes.nome as Nome,
	                clientes.razão as RazaoSocial,
	                clientes.cep as Cep,
	                clientes.endereço as Endereco,
	                clientes.end_num as Numero,
	                clientes.bairro as Bairro,
	                clientes.cidade as Cidade,
	                clientes.estado as Uf,
                    Isnull(clientes.DDD,0) + Isnull(clientes.Fone1,0) as Fone,
	                --usuario

	                excutando.nome	as NomeUsuario,
	                excutando.nome as [User],
	                excutando.senha as [Password],

                    frota_1.Nome as Veiculo
                from ocorrencia_vip Ocorrencia
                left join clientes on clientes.codigo = Ocorrencia.cliente
                left join usuarios emcaminhado	on Ocorrencia.enc_tecnico = emcaminhado.codigo
                left join usuarios excutando	on Ocorrencia.age_tecnico = excutando.codigo
                left join Roteiro_1 On Ocorrencia.Age_nro = Roteiro_1.Nro
                --left join Roteiro_2 On Ocorrencia.ocorrencia = Roteiro_2.Ocorrencia
	            Left Join frota_1 On Roteiro_1.Veiculo = frota_1.Codigo ";
        }

        public override string GetUpdateBasic()
        {
            throw new NotImplementedException();
        }

        public override string GetInsertBasic()
        {
            throw new NotImplementedException();
        }
    }
}