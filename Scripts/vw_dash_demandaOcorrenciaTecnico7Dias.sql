/****** Object:  View [dbo].[vw_dash_demandaOcorrenciaTecnico7Dias]    Script Date: 15/12/2021 15:04:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create View [dbo].[vw_dash_demandaOcorrenciaTecnico7Dias]
As
Select 
	UsuarioId,
	UsuarioNome,
	Imagem, 
	DataAgendamento, 
	Sum(Ocorrencias) as Ocorrencias, 
	Sum(Finalizadas) as Finalizadas 
	From (
		select  
			Usuarios.Codigo as UsuarioId, 
			Usuarios.Nome as UsuarioNome, 
			Usuarios.Imagem,
			Age_Data as DataAgendamento,
			Count(1) as Ocorrencias,
			0 as Finalizadas
		from Ocorrencia_vip 
		Inner Join Clientes On Clientes.Codigo = Ocorrencia_vip.Cliente
		Inner Join Usuarios On Ocorrencia_vip.Age_Tecnico = Usuarios.Codigo 
		Where  
			Age_Data Between  Convert(Char(10), DATEADD(d, -7, GetDate()), 102) And Convert(Char(10), GetDate(), 102)
		Group By 
			Usuarios.Codigo, 
			Usuarios.Nome,
			Usuarios.Imagem,
			Age_Data

	Union 

		select  
			Usuarios.Codigo as UsuarioId, 
			Usuarios.Nome as UsuarioNome, 
			Usuarios.Imagem,
			Age_Data as DataAgendamento,
			0 as Ocorrencias,
			Count(1) as Finalizadas
		from Ocorrencia_vip 
		Inner Join Clientes On Clientes.Codigo = Ocorrencia_vip.Cliente
		Inner Join Usuarios On Ocorrencia_vip.Age_Tecnico = Usuarios.Codigo 
		Where  
			Ok_Data is not null And Age_Data Between  Convert(Char(10), DATEADD(d, -7, GetDate()), 102) And Convert(Char(10), GetDate(), 102)
		Group By 
			Usuarios.Codigo, 
			Usuarios.Nome,
			Usuarios.Imagem,
			Age_Data


	) as tmp
	Group By 
		UsuarioId,UsuarioNome,Imagem, DataAgendamento
GO


