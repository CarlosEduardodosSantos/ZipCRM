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
            sql.Append(" Where OcorrenciaId = @sid");
            sql.Append(ReturnGroupby());
            sql.Append(Environment.NewLine);
            sql.Append(GetSelectBasic());
            sql.Append(" Where OcorrenciaId = @sid");
            sql.Append(ReturnGroupby());

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
            sql.Append(" Where Ok_Data is null And enc_tecnico = @sid");
            using (IDbConnection cn = Connection)
            {
                cn.Open();

                //var multi = cn.QueryMultiple(sql.ToString(), new { sid = usuarioId });
                //var oc = multi.Read<Ocorrencia>();
                var ocorrencias = cn.Query<Ocorrencia>(sql.ToString(), new { sid = usuarioId });

                sql = new StringBuilder().Append(GetSelectBasic());
                sql.Append(" Where OcorrenciaId = @sid");
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
            sql.Append(" Where DhRoteiro is not null And DataFimVip is null");
            sql.Append(" And UsuarioId = @sid");
            sql.Append(ReturnGroupby());
            sql.Append(" Order By DhRoteiro");

            using (IDbConnection cn = Connection)
            {
                cn.Open();

                //var multi = cn.QueryMultiple(sql.ToString(), new { sid = usuarioId });
                //var oc = multi.Read<Ocorrencia>();
                var ocorrencias = cn.Query<Ocorrencia>(sql.ToString(), new { sid = usuarioId });

                sql = new StringBuilder().Append(GetSelectBasic());
                sql.Append(" Where OcorrenciaId = @sid");
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
            sql.Append(" Where Ok_Data is null");
            sql.Append(" And DataInicioVip is not null");
            sql.Append(" And (UsuarioId = @sid Or enc_tecnico = @sid)");
            using (IDbConnection cn = Connection)
            {
                cn.Open();

                var ocorrencias = cn.Query<Ocorrencia>(sql.ToString(), new { sid = usuarioId });

                sql = new StringBuilder().Append(GetSelectBasic());
                sql.Append(" Where OcorrenciaId = @sid");
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
            sql.Append(" Where DataInicioVIP is not null And Ok_Data is null");
            sql.Append(" And (UsuarioId = @sid Or enc_tecnico = @sid)");

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
                cn.Query("spIniciarOcorrencia", param, commandType: CommandType.StoredProcedure);
                cn.Close();

                return new ValidationResult();
            }

        }

        public ValidationResult IncluirOcorrencia(IncluirOcorrencia incluirOcorrencia)
        {
            var param = new DynamicParameters();

            param.Add("@Tecnico", incluirOcorrencia.UsuarioID);
            param.Add("@Cliente", incluirOcorrencia.ClienteID);
            param.Add("@Problema", incluirOcorrencia.Problema);
            param.Add("@OcorrenciaGUID", incluirOcorrencia.OcorrenciaGUID);


            using (IDbConnection cn = Connection)
            {
                cn.Open();
                cn.Query("spIncluirOcorrencia_eRat", param, commandType: CommandType.StoredProcedure);
                cn.Close();

                return new ValidationResult();
            }

        }

        public ValidationResult IncluirRoteiro(IncluirRoteiro incluirRoteiro)
        {
            var param = new DynamicParameters();

            param.Add("@Tecnico_Rot", incluirRoteiro.TecnicoId);
            param.Add("@Veiculo", incluirRoteiro.VeiculoId);
            param.Add("@OcorrenciaGUID", incluirRoteiro.OcorrenciaGUID);


            using (IDbConnection cn = Connection)
            {
                cn.Open();
                cn.Query("spIncluirRoteiro_eRat", param, commandType: CommandType.StoredProcedure);
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
            sql.Append(" Where Ok_Data is null And enc_tecnico = @sid");
            sql.Append(" And Nome like @spesquisa");


            using (IDbConnection cn = Connection)
            {
                cn.Open();

                //var multi = cn.QueryMultiple(sql.ToString(), new { sid = usuarioId });
                //var oc = multi.Read<Ocorrencia>();
                var ocorrencias = cn.Query<Ocorrencia>(sql.ToString(),
                    new { sid = usuarioId, spesquisa = "%" + pesquisa + "%" });

                sql = new StringBuilder().Append(GetSelectBasic());
                sql.Append(" Where OcorrenciaId= @sid");
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
            sql.Append(" Where datediff(day, Ok_Data, getdate()) <= @sdias And DataFimVip is not null And  Depto <> 4 And  Veiculo <> 'ADM' ");
            sql.Append(" And (@sid = 0 Or UsuarioId = @sid Or enc_tecnico = @sid)");
            sql.Append(ReturnGroupby());
            sql.Append(" Order By Ok_Data desc");
            

            using (IDbConnection cn = Connection)
            {
                cn.Open();

                //var multi = cn.QueryMultiple(sql.ToString(), new { sid = usuarioId });
                //var oc = multi.Read<Ocorrencia>();
                var ocorrencias = cn.Query<Ocorrencia>(sql.ToString(),
                    new { sid = usuarioId, sdias = dias });

                sql = new StringBuilder().Append(GetSelectBasic());
                sql.Append(" Where OcorrenciaId= @sid");
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

        public IEnumerable<Ocorrencia> ObterOcorrenciasPorOcorrenciaIDFinalizadas(int usuarioId, int ocorrenciaId)
        {
            var sql = new StringBuilder().Append(GetSelectBasic());
            sql.Append(" where OcorrenciaID = @socorrenciaId And DataFimVip is not null And  Depto <> 4 And  Veiculo <> 'ADM' ");
            sql.Append(" And (@sid = 0 Or UsuarioId = @sid Or enc_tecnico = @sid)");
            sql.Append(ReturnGroupby());
            sql.Append(" Order By Ok_Data desc");


            using (IDbConnection cn = Connection)
            {
                cn.Open();

                //var multi = cn.QueryMultiple(sql.ToString(), new { sid = usuarioId });
                //var oc = multi.Read<Ocorrencia>();
                var ocorrencias = cn.Query<Ocorrencia>(sql.ToString(),
                    new { sid = usuarioId, socorrenciaId = ocorrenciaId });

                sql = new StringBuilder().Append(GetSelectBasic());
                sql.Append(" Where OcorrenciaId= @sid");
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

        public IEnumerable<Escalas> ObterEscalasPorData()
        {
            DateTime dt = DateTime.Today;
            var _min = new DateTime(dt.Year, dt.Month, 1);
            var _max = _min.AddMonths(1).AddDays(-1);
            var min = _min.ToString("yyyy-MM-dd");
            var max = _max.ToString("yyyy-MM-dd");
            var sql = "select id,cast(data as date) as dataEscala,func1,func2,func3,func4,func5 from VW_SABADOS_MES where data between @smin and @smax";
            using (IDbConnection cn = Connection)
            {
                cn.Open();

                var escalas = cn.Query<Escalas>(sql.ToString(), new { smin = min, smax = max });

                cn.Close();

                return escalas;
            }
        }

        public ValidationResult IncluirEscala(Escalas incluirEscala)
        {
            var param = new DynamicParameters();

            param.Add("@Data", incluirEscala.DataEscala);
            param.Add("@Func1", incluirEscala.Func1);
            param.Add("@Func2", incluirEscala.Func2);
            param.Add("@Func3", incluirEscala.Func3);
            param.Add("@Func4", incluirEscala.Func4);
            param.Add("@Func5", incluirEscala.Func5);

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                cn.Query("spIncluirEscala_eRat", param, commandType: CommandType.StoredProcedure);
                cn.Close();

                return new ValidationResult();
            }
        }

        public override string GetSelectBasic()
        {
            return "Select * from Vw_OcorrenciaRat";            
        }

        public string ReturnGroupby()
        {
            return " group by Ocorrenciaid,Roteiroid,sequencia,clienteid,usuarioid,dataocorrencia,rat," +
                "quantidadedias,dhocorrencia,dhroteiro,contato,statusid,prioridade,comentario,problema," +
                "solucao,DataInicioVIP,datafimvip,ok_data,enc_tecnico,depto,cnpj,nome,razaosocial,cep," +
                "endereco,numero,bairro,cidade,uf,fone,nomeusuario,[user],[password],veiculo";
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