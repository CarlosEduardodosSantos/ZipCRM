/****** Object:  View [dbo].[vw_dash_monitor_OcorrenciaPendentesMes]    Script Date: 15/12/2021 15:03:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE View [dbo].[vw_dash_monitor_OcorrenciaPendentesMes]
as

	Select 
		UsuarioId, UsuarioNome, 
		Sum(QtdeOcorrencia) as QtdeOcorrencia, 
		Sum(QtdeOcorrenciaConcluida) as QtdeOcorrenciaConcluida,
		Sum(QtdeOcorrenciaExecucao) as QtdeOcorrenciaExecucao, 
		Sum(QtdeOcorrenciaPendente30) as QtdeOcorrenciaPendente30,
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
				Where data between DATEADD(D,1-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112)) and DATEADD(D,(SELECT DBO.FN_DIASDOMES (GETDATE()))-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112))
				and ocorrencia not in (select ocorrencia from ocorrencia_vip where data between DATEADD(D,1-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112)) and DATEADD(D,(SELECT DBO.FN_DIASDOMES (GETDATE()))-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112)) and age_tecnico = 268)
				and ocorrencia not in (select ocorrencia from Ocorrencia_vip Where data between DATEADD(D,1-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112)) and DATEADD(D,(SELECT DBO.FN_DIASDOMES (GETDATE()))-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112)) and solucao = 'Atendimento/Fone')
				Group By Usuarios.Codigo, Usuarios.Nome
				

	) as tmp
	Group By UsuarioId, UsuarioNome


GO


