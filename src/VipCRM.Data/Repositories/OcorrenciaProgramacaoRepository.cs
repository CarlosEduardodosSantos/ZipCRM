using System.Collections.Generic;
using Dapper;
using VipCRM.Core.Domain.Entites;
using VipCRM.Data.Interface.Repositories;

namespace VipCRM.Data.Repositories
{
    public class OcorrenciaProgramacaoRepository : RepositoryBase, IOcorrenciaProgramacaoRepository
    {
        public IEnumerable<ResultOcorrenciaProgramacao> OcorrenciasPorSoftware()
        {
            var sql = @"Select
                            Software.Codigo, 
	                        Software.Soft as Nome,
	                        Count(1) as Ocorrencias
                        From Ocorrencia_Software
                        Left Join Software On Software.Codigo = Ocorrencia_Software.Cod_Soft
                        Where Data_Fim is null
                        And Data >= '2016-01-01'
                        Group By
                            Software.Codigo,
	                        Software.Soft
                        Order By Software.Soft";

            using (var conn = Connection)
            {
                conn.Open();
                var result = conn.Query<ResultOcorrenciaProgramacao>(sql);
                conn.Close();

                return result;
            }
        }

        public IEnumerable<ResultOcorrenciaProgramacao> OcorrenciasPorCliente()
        {
            var sql = @"Select
	                        uEnc.Codigo,
	                        uEnc.Nome as Nome,
	                        Count(1) as Ocorrencias
                        From Ocorrencia_Software
                        Left Join Usuarios uEnc On uEnc.Codigo = Ocorrencia_Software.Enc_Tecnico
                        Where Enc_Data is not null and Data_Fim is null
                        And Data >= '2016-01-01'
                        Group By 
	                        uEnc.Codigo,
	                        uEnc.Nome
                        Order By 
	                        uEnc.Nome";

            using (var conn = Connection)
            {
                conn.Open();
                var result = conn.Query<ResultOcorrenciaProgramacao>(sql);
                conn.Close();

                return result;
            }
        }

        public IEnumerable<ResultOcorrenciaProgramacao> OcorrenciasPorProgramador()
        {
            var sql = @"Select
	                        Clientes.Codigo,
	                        Clientes.Nome,
	                        Count(1) as Ocorrencias
                        From Ocorrencia_Software
                        Inner Join Clientes On Clientes.Codigo = Ocorrencia_Software.Cliente
                        Where Data_Fim is null
                        And Data >= '2016-01-01'
                        Group By
	                        Clientes.Codigo,
	                        Clientes.Nome
                        Order By
	                        Clientes.Nome";

            using (var conn = Connection)
            {
                conn.Open();
                var result = conn.Query<ResultOcorrenciaProgramacao>(sql);
                conn.Close();

                return result;
            }
        }

        public IEnumerable<OcorrenciaProgramacao> OcorrenciaProgramadores(int softwareId, int clienteId, int programadorId)
        {
            var sql = @"Select
	                        Ocorrencia_Software.nro as OcorrenciaId, 
	                        Software.Soft, 
	                        Topico,
	                        Desc_Topico as Descricao,
	                        Isnull(uAbert.Nome,'') as Tecnico,
	                        Isnull(uEnc.Nome,'') as Ecaminhado,
	                        Isnull(uExec.Nome,'') as Execultando,  
	                        Data as DataAbertura,
	                        convert(datetime, Data_Ini + ' ' +convert(char(5), Hora_Ini, 108), 103) as DataAbertura,
	                        convert(datetime, Data_Fim + ' ' +convert(char(5), Hora_Fim, 108), 103) as DataFinalizado,
	                        Status as Situacao,
	                        Prioridade,
	                        Isnull(ObsDev,'') as ObsDev
                        From Ocorrencia_Software
                        Left Join Software On Software.Codigo = Ocorrencia_Software.Cod_Soft
                        Left Join Clientes On Clientes.Codigo = Ocorrencia_Software.Cliente
                        Left Join Usuarios uExec On uExec.Codigo = Ocorrencia_Software.Tecnico_Exe
                        Left Join Usuarios uAbert On uAbert.Codigo = Ocorrencia_Software.Tecnico
                        Left Join Usuarios uEnc On uEnc.Codigo = Ocorrencia_Software.Enc_Tecnico
                        Where Data_Fim is null
                        And Data >= '2016-01-01'
                        And (@softwareId = 0 Or Software.Codigo = @softwareId)
                        And (@clienteId = 0 Or Clientes.Codigo = @clienteId)
                        And (@programadorId = 0 Or uEnc.Codigo = @programadorId)
                        Order By Data
                        ";

            using (var conn = Connection)
            {
                conn.Open();
                var result = conn.Query<OcorrenciaProgramacao>(sql, new {softwareId, clienteId, programadorId});
                conn.Close();

                return result;
            }
        }

        public IEnumerable<OcorrenciaProgramacao> ObterPorUsuario(string usuarioId)
        {
            var sql = @"Select 
	                        Oc.Nro as OcorrenciaId,
	                        Software.Soft as Software,
	                        Oc.Cliente as ClienteId,
	                        Cli.Nome as Cliente,
	                        TA.codigo as TecnicoId,
	                        TA.Nome as Tecnico, 
	                        Topico, 
	                        Desc_Topico as Descricao, 
	                        ObsDev, 
	                        StatusDev, 
	                        TE.Nome as Programador,
	                        Oc.Enc_Data as DataEncaminhamento,
	                        convert(datetime, Oc.Data_Ini + ' ' +convert(char(5), Oc.Hora_Ini, 108), 103) as DataAbertura,
	                        convert(datetime, Oc.Data_Fim + ' ' +convert(char(5), Oc.Hora_Fim, 108), 103) as DataFinalizado,
	                        Oc.Prioridade
                        From Ocorrencia_Software Oc
                        Inner Join Clientes Cli On Oc.Cliente = Cli.Codigo
                        Inner Join Software On Oc.Cod_Soft = Software.Codigo
                        Inner Join Usuarios TA On Oc.Tecnico = TA.codigo
                        Left Join Usuarios TE On Oc.Tecnico_Exe = TE.Codigo
                         Where [Status] = 'Ag.Homologar' And Oc.Data >= '2018-01-01' And Oc.Tecnico = @usuarioId
                         Order By TA.Nome, Software.Soft
                        ";

            using (var conn = Connection)
            {
                conn.Open();
                var result = conn.Query<OcorrenciaProgramacao>(sql, new { usuarioId });
                conn.Close();

                return result;
            }
        }

        public override string GetSelectBasic()
        {
            throw new System.NotImplementedException();
        }

        public override string GetUpdateBasic()
        {
            throw new System.NotImplementedException();
        }

        public override string GetInsertBasic()
        {
            throw new System.NotImplementedException();
        }
    }
}