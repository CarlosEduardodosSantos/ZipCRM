/****** Object:  View [dbo].[vw_dash_demandaOcorrenciaGeral7Dias]    Script Date: 15/12/2021 15:04:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE View [dbo].[vw_dash_demandaOcorrenciaGeral7Dias]
As
	Select 
		DataOcorrencia, 
		Sum(Ocorrencias) as Ocorrencias, 
		Sum(Finalizadas) as Finalizadas 
		From (
			select  
				Convert(Char(10), Data, 111) as DataOcorrencia,
				Count(1) as Ocorrencias,
				0 as Finalizadas
			from Ocorrencia_vip 
			Where  
				Data Between  Convert(Char(10), DATEADD(d, -9, GetDate()), 102) And Convert(Char(10), DATEADD(d, -1, GetDate()), 102)
			Group By 
				Data

		Union 

			select  
				Convert(Char(10), Data, 111) as DataOcorrencia,
				0 as Ocorrencias,
				Count(1) as Finalizadas
			from Ocorrencia_vip 
			Where  
				Ok_Data is not null 
				And Data Between  Convert(Char(10), DATEADD(d, -9, GetDate()), 102) And Convert(Char(10), DATEADD(d, -1, GetDate()), 102)
			Group By 
				Data


		) as tmp
		Group By 
			DataOcorrencia
GO


