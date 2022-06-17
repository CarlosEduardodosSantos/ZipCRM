/****** Object:  View [dbo].[vw_dash_monitor_OcorrenciaPendentesMesAnterior]    Script Date: 15/12/2021 15:03:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE View [dbo].[vw_dash_monitor_OcorrenciaPendentesMesAnterior]
as

	Select 
		UsuarioId, UsuarioNome, 
		Sum(QtdeOcorrencia) as QtdeOcorrencia, 
		Sum(QtdeOcorrenciaConcluida) as QtdeOcorrenciaConcluida,
		Sum(QtdeOcorrenciaExecucao) as QtdeOcorrenciaExecucao, 
		Sum(QtdeOcorrenciaPendente30) as QtdeOcorrenciaPendente30Anterior,
		 Max(DataUltAtualizacao) as DataUltAtualizacao
		From (

				--Total de ocorrencias pendentes
				select 
					Usuarios.Codigo as UsuarioId, 
					Usuarios.Nome as UsuarioNome, 
					0 as QtdeOcorrencia, 
					0 as QtdeOcorrenciaConcluida,
					0 as QtdeOcorrenciaExecucao,
					count(1) as QtdeOcorrenciaPendente30,
					Max(data) as DataUltAtualizacao
				from Ocorrencia_vip
				Inner Join Usuarios On Ocorrencia_vip.Tecnico = Usuarios.Codigo 
				Where DATA >= DATEADD(D,1,DATEADD(M,-2,EOMONTH(GETDATE()))) AND DATA < DATEADD(D,1,DATEADD(M,-1,EOMONTH(GETDATE())))
				and ocorrencia not in (select ocorrencia from ocorrencia_vip where DATA >= DATEADD(D,1,DATEADD(M,-2,EOMONTH(GETDATE()))) AND DATA < DATEADD(D,1,DATEADD(M,-1,EOMONTH(GETDATE()))) and age_tecnico = 268)
				and ocorrencia not in (select ocorrencia from Ocorrencia_vip Where DATA >= DATEADD(D,1,DATEADD(M,-2,EOMONTH(GETDATE()))) AND DATA < DATEADD(D,1,DATEADD(M,-1,EOMONTH(GETDATE()))) and solucao = 'Atendimento/Fone')
				Group By Usuarios.Codigo, Usuarios.Nome
				

	) as tmp
	Group By UsuarioId, UsuarioNome


GO


