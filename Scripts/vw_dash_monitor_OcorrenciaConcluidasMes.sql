
/****** Object:  View [dbo].[vw_dash_monitor_OcorrenciaConcluidasMes]    Script Date: 15/12/2021 15:39:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE View [dbo].[vw_dash_monitor_OcorrenciaConcluidasMes]
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
					Max(OK_data) as DataUltAtualizacao
				from Ocorrencia_vip
				Inner Join Usuarios On Ocorrencia_vip.OK_TECNICO = Usuarios.Nome 
				where AGE_data between DATEADD(D,1-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112)) and DATEADD(D,(SELECT DBO.FN_DIASDOMES (GETDATE()))-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112)) and
				OK_data between DATEADD(D,1-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112)) and DATEADD(D,(SELECT DBO.FN_DIASDOMES (GETDATE()))-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112)) AND 
				OK_TECNICO IS NOT NULL  and
				(veiculo is null or Veiculo = 'ADM')

				Group By Usuarios.Codigo, Usuarios.Nome
				

				union

				select 
					Usuarios.Codigo as UsuarioId, 
					Usuarios.Nome as UsuarioNome, 
					0 as QtdeConcluida30,
					Count(1) as QtdeConcluidaExt30,
					Max(OK_data) as DataUltAtualizacao
				from Ocorrencia_vip
				Inner Join Usuarios On Ocorrencia_vip.OK_TECNICO = Usuarios.Nome 
				where AGE_data	>= DATEADD(D,1-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112)) and AGE_data <= DATEADD(D,(SELECT DBO.FN_DIASDOMES (GETDATE()))-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112)) and
				OK_data			>= DATEADD(D,1-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112)) and CONVERT(DATETIME, FLOOR(CONVERT(FLOAT(24), ok_data))) <= DATEADD(D,(SELECT DBO.FN_DIASDOMES (GETDATE()))-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112)) AND 
				OK_TECNICO is not null and
				veiculo is not null and
				veiculo <> 'ADM'
				Group By Usuarios.Codigo, Usuarios.Nome
				
	) as tmp
	Group By UsuarioId, UsuarioNome


GO


