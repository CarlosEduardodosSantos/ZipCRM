/****** Object:  View [dbo].[vw_dash_monitor_OcorrenciaConcluidas]    Script Date: 15/12/2021 15:04:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE View [dbo].[vw_dash_monitor_OcorrenciaConcluidas]
as

	Select 
		UsuarioId, UsuarioNome, 
		Sum(QtdeConcluida) as QtdeConcluida,
		Max(DataUltAtualizacao) as DataUltAtualizacao
		From (

				--Total de ocorrencias ABERTAS
				select 
					Usuarios.Codigo as UsuarioId, 
					Usuarios.Nome as UsuarioNome, 
					Count(1) as QtdeConcluida,
					Max(data) as DataUltAtualizacao
				from Ocorrencia_vip
				Inner Join Usuarios On Ocorrencia_vip.AGE_Tecnico = Usuarios.Codigo 
				where AGE_data  = Convert(Char(10), GetDate(), 102) 
				AND OK_TECNICO IS NOT NULL --and VEICULO IS NOT NULL
				Group By Usuarios.Codigo, Usuarios.Nome
		

	) as tmp
	Group By UsuarioId, UsuarioNome








GO


