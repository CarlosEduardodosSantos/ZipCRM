

create VIEW [DBO].[VW_Dash_Monitor_Ocorrencias_Abertura] AS

		SELECT ocorrencia,CONVERT(VARCHAR(11),hora,114) as hora,C.NOME as nomecliente,PRIORIDADE as prioridade, isnull(problema,'') as problema,
		dbo.fn_ConvertMinutosHoras((CAST(LTRIM(DATEDIFF(MINUTE, 0, CONVERT(VARCHAR(11),getdate(),114))) AS INT))-(CAST(LTRIM(DATEDIFF(MINUTE, 0, CONVERT(VARCHAR(11),hora,114))) AS INT))) as qtdehoras
		FROM OCORRENCIA_VIP OC
				LEFT JOIN CLIENTES C ON C.CODIGO = OC.CLIENTE
		WHERE DATA = DATEADD(DD, 0, DATEDIFF(DD, 0, GETDATE())) AND OK_DATA IS NULL and Age_Data = DATEADD(DD, 0, DATEDIFF(DD, 0, GETDATE()))
		 
GO


