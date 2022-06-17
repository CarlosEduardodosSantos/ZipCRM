/****** Object:  View [dbo].[vw_dash_OcorrenciaAtendimentoCliente]    Script Date: 15/12/2021 15:05:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE View [dbo].[vw_dash_OcorrenciaAtendimentoCliente]
As
	select  
		Usuarios.Codigo as UsuarioId, 
		Usuarios.Nome as UsuarioNome, 
		Usuarios.Imagem,
		Clientes.Codigo as ClienteId,
		Clientes.Nome as ClienteNome,
		Age_Data as DataAgendamento,
		DataInicioVIP,
		DataFimVIP,
		Case 
			When Ok_Data Is not null Then 'Concluido'
			When DataInicioVIP Is not null And DataFimVIP Is null Then 'Execução'
			When DataInicioVIP Is null And DataFimVIP Is null Then 'Pendente'
		End as Situacao,
		Roteiro_1.Data as DataRoteiro,
		Roteiro_1.Hora as HoraRoreito,
		frota_1.Nome as Veiculo
	from Ocorrencia_vip 
	Inner Join Clientes On Clientes.Codigo = Ocorrencia_vip.Cliente
	Inner Join Usuarios On Ocorrencia_vip.Age_Tecnico = Usuarios.Codigo 
	Left Join Roteiro_1 On Ocorrencia_vip.Age_nro = Roteiro_1.Nro
	Left Join frota_1 On Roteiro_1.Veiculo = frota_1.Codigo
	Where  Age_nro is not null And (Ok_Data Is null or Age_Data = Convert(Char(10), GetDate(), 102) )


GO


