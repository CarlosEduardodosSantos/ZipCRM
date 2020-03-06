USE [Vip_Order]
GO

/****** Object:  View [dbo].[vw_dash_monitor_OcorrenciaConcluidasMesAnterior]    Script Date: 06/03/2020 14:04:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE View [dbo].[vw_dash_monitor_OcorrenciaConcluidasMesAnterior]
as

	Select 
		UsuarioId, UsuarioNome,
		Sum(QtdeConcluida30) as QtdeConcluida30Anterior,
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
				where AGE_data >= DATEADD(D,1,DATEADD(M,-2,EOMONTH(GETDATE()))) AND AGE_data < DATEADD(D,1,DATEADD(M,-1,EOMONTH(GETDATE()))) and
				OK_data >= DATEADD(D,1,DATEADD(M,-2,EOMONTH(GETDATE()))) AND OK_data < DATEADD(D,1,DATEADD(M,-1,EOMONTH(GETDATE()))) AND 
				OK_TECNICO IS NOT NULL 
				Group By Usuarios.Codigo, Usuarios.Nome,Ocorrencia_vip.Veiculo
			

	) as tmp
	Group By UsuarioId, UsuarioNome

GO


