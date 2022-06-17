
/****** Object:  View [dbo].[VW_SABADOS_MES]    Script Date: 15/12/2021 15:05:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[VW_SABADOS_MES] AS
	SELECT *, convert(varchar(20), data, 103 ) as DIA FROM SABADOS 
	WHERE DATA >= convert(date,getdate()) AND DATA <= DATEADD(D,31-DAY(GETDATE()),CONVERT(CHAR(8),GETDATE(),112))
	

GO


