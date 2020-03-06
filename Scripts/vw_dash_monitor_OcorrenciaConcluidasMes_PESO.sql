
/****** Object:  View [dbo].[vw_dash_monitor_OcorrenciaConcluidasMes]    Script Date: 06/03/2020 14:04:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





ALTER View [dbo].[vw_dash_monitor_OcorrenciaConcluidasMes]
as

	Select 
		UsuarioId, UsuarioNome,
		Sum(QtdeConcluidaInt30) as QtdeConcluidaInt30,
		Sum(QtdeConcluidaExt30) as QtdeConcluidaExt30,
		Sum(QtdeConcluidaInt30)+(Sum(QtdeConcluidaExt30)*2) as Qtdetotal,
		Max(DataUltAtualizacao) as DataUltAtualizacao
		From (

				--Total de ocorrencias ABERTAS
				select 
					Usuarios.Codigo as UsuarioId, 
					Usuarios.Nome as UsuarioNome, 
					Count(1) as QtdeConcluidaInt30,
					0 as QtdeConcluidaExt30,
					Max(data) as DataUltAtualizacao
				from Ocorrencia_vip
				Inner Join Usuarios On Ocorrencia_vip.OK_TECNICO = Usuarios.Nome 
				where AGE_data between DATEADD(D,1-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112)) and DATEADD(D,(SELECT DBO.FN_DIASDOMES (GETDATE()))-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112)) and
				OK_data between DATEADD(D,1-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112)) and DATEADD(D,(SELECT DBO.FN_DIASDOMES (GETDATE()))-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112)) AND 
				OK_TECNICO IS NOT NULL  and
				(veiculo is null or Veiculo = 'ADM')

				Group By Usuarios.Codigo, Usuarios.Nome,Ocorrencia_vip.Veiculo
				

				union

				select 
					Usuarios.Codigo as UsuarioId, 
					Usuarios.Nome as UsuarioNome, 
					0 as QtdeConcluida30,
					Count(1) as QtdeConcluidaExt30,
					Max(data) as DataUltAtualizacao
				from Ocorrencia_vip
				Inner Join Usuarios On Ocorrencia_vip.OK_TECNICO = Usuarios.Nome 
				where AGE_data	between DATEADD(D,1-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112)) and DATEADD(D,(SELECT DBO.FN_DIASDOMES (GETDATE()))-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112)) and
				OK_data			between DATEADD(D,1-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112)) and DATEADD(D,(SELECT DBO.FN_DIASDOMES (GETDATE()))-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112)) AND 
				OK_TECNICO is not null and
				veiculo is not null and
				veiculo <> 'ADM'
				Group By Usuarios.Codigo, Usuarios.Nome,Ocorrencia_vip.Veiculo
				
	) as tmp
	Group By UsuarioId, UsuarioNome

GO


