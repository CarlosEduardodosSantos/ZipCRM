

/****** Object:  View [dbo].[vw_dash_OcorrenciaTotalizador]    Script Date: 06/02/2020 17:32:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--select * from [vw_dash_OcorrenciaTotalizador]


create View [dbo].[vw_dash_OcorrenciaTotalizador]
As
Select 
	OcorrenciasRoteiro = (Select count(1) from Ocorrencia_vip  Where Age_Data = Convert(Char(10), GetDate(), 102) And Age_nro is not null),
	OcorrenciasConcluidas = (Select count(1) from Ocorrencia_vip  Where Age_Data = Convert(Char(10), GetDate(), 102)  And Ok_Data Is not null ),
	OcorrenciasExecucao = (Select count(1) from Ocorrencia_vip  Where  Age_Data = Convert(Char(10), GetDate(), 102) And Age_nro is not null And DataInicioVIP is not null And DataFimVIP Is null),
	OcorrenciaAbertaGeral = (Select count(1) from Ocorrencia_vip  Where Ok_data Is null And Age_nro Is null),
	OcorrenciaPendentes = (Select count(1) from Ocorrencia_vip Where Age_Data = Convert(Char(10), GetDate(), 102) And Ok_Data is null And DataInicioVIP is null and Age_Tecnico is not null),
	OcorrenciaConcluidasTotal = (select Sum(QtdeConcluida30) from (select Count(1) as QtdeConcluida30 from Ocorrencia_vip where AGE_data between DATEADD(D,1-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112)) and DATEADD(D,(SELECT DBO.FN_DIASDOMES (GETDATE()))-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112)) and
								OK_data between DATEADD(D,1-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112)) and DATEADD(D,(SELECT DBO.FN_DIASDOMES (GETDATE()))-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112)) AND 
								OK_TECNICO IS NOT NULL ) as tmp),
	OcorrenciaPendentesTotal = (select Sum(QtdeOcorrenciaPendente30) as QtdeOcorrenciaPendente30 from (select count(1) as QtdeOcorrenciaPendente30 from Ocorrencia_vip Where data between DATEADD(D,1-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112)) and DATEADD(D,(SELECT DBO.FN_DIASDOMES (GETDATE()))-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112))
								and ocorrencia not in (select ocorrencia from ocorrencia_vip where data between DATEADD(D,1-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112)) and DATEADD(D,(SELECT DBO.FN_DIASDOMES (GETDATE()))-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112)) and age_tecnico = 268)
								and ocorrencia not in (select ocorrencia from Ocorrencia_vip Where data between DATEADD(D,1-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112)) and DATEADD(D,(SELECT DBO.FN_DIASDOMES (GETDATE()))-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112)) and solucao = 'Atendimento/Fone')) as tmp)
GO




