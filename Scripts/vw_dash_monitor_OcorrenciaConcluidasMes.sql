USE [Vip_Order]
GO

/****** Object:  View [dbo].[vw_dash_monitor_OcorrenciaConcluidasMes]    Script Date: 06/03/2020 14:03:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE View [dbo].[vw_dash_monitor_OcorrenciaConcluidasMes]
as

	Select 
		UsuarioId, UsuarioNome,
		Sum(QtdeConcluida30) as QtdeConcluida30,
		--CASE WHEN USUARIOID = 63 THEN 0 Else Sum(QtdeConcluida30) end as QtdeConcluida30,
		Max(DataUltAtualizacao) as DataUltAtualizacao
		From (

				--Total de ocorrencias ABERTAS
				select 
					Usuarios.Codigo as UsuarioId, 
					Usuarios.Nome as UsuarioNome, 
					Count(1) as QtdeConcluida30,
					Max(data) as DataUltAtualizacao
				from Ocorrencia_vip
				Inner Join Usuarios On Ocorrencia_vip.OK_TECNICO = Usuarios.Nome 
				where AGE_data between DATEADD(D,1-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112)) and DATEADD(D,(SELECT DBO.FN_DIASDOMES (GETDATE()))-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112)) and
				OK_data between DATEADD(D,1-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112)) and DATEADD(D,(SELECT DBO.FN_DIASDOMES (GETDATE()))-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112)) AND 
				OK_TECNICO IS NOT NULL --and Usuarios.Codigo <> 63
				Group By Usuarios.Codigo, Usuarios.Nome,Ocorrencia_vip.Veiculo
				/*where AGE_data between '2019-11-01' and '2019-11-30' and
				OK_data between '2019-11-01' and '2019-11-30' AND */

	) as tmp
	Group By UsuarioId, UsuarioNome

GO


