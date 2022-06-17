/****** Object:  View [dbo].[vw_dash_monitor_Ocorrencia]    Script Date: 15/12/2021 15:04:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE View [dbo].[vw_dash_monitor_Ocorrencia]
as

	Select 
		UsuarioId, UsuarioNome,UsuarioVeiculo,
		Sum(QtdeOcorrencia) as QtdeOcorrencia, 
		Sum(QtdeOcorrenciaConcluida) as QtdeOcorrenciaConcluida,
		Sum(QtdeOcorrenciaExecucao) as QtdeOcorrenciaExecucao, 
		Sum(QtdeOcorrenciaPendente) as QtdeOcorrenciaPendente,
		 Max(DataUltAtualizacao) as DataUltAtualizacao
		From (

				--Total de ocorrencias por roteiro
				select 
					Usuarios.Codigo as UsuarioId, 
					Usuarios.Nome as UsuarioNome,
					Ocorrencia_vip.Veiculo as UsuarioVeiculo, 
					Count(1) as QtdeOcorrencia, 
					0 as QtdeOcorrenciaConcluida,
					0 as QtdeOcorrenciaExecucao,
					0 as QtdeOcorrenciaPendente,
					Case 
						When Max(DataFimVIP) IS NULL And Max(DataInicioVip) is not null Then Max(DataInicioVip)
						When Max(DataFimVIP) is not null Then Max(DataFimVIP)
						else Max(Data_Roteiro) End as DataUltAtualizacao
				from Ocorrencia_vip 
				--Inner Join Clientes On Clientes.Codigo = Ocorrencia_vip.Cliente
				Inner Join Usuarios On Ocorrencia_vip.Age_Tecnico = Usuarios.Codigo 
				Where Age_Data = Convert(Char(10), GetDate(), 102) And Age_nro is not null
				Group By Usuarios.Codigo, Usuarios.Nome,Ocorrencia_vip.Veiculo


				Union

				--Total Ocorrencias Concluidas
				select 
					Usuarios.Codigo as UsuarioId, 
					Usuarios.Nome as UsuarioNome,
					Ocorrencia_vip.Veiculo as UsuarioVeiculo, 
					0 as QtdeOcorrencia, 
					Count(1) as QtdeOcorrenciaConcluida,
					0 as QtdeOcorrenciaExecucao,
					0 as QtdeOcorrenciaPendente,
					Case 
						When Max(DataFimVIP) IS NULL And Max(DataInicioVip) is not null Then Max(DataInicioVip)
						When Max(DataFimVIP) is not null Then Max(DataFimVIP)
						else Max(Data_Roteiro) End as DataUltAtualizacao
				from Ocorrencia_vip 
				--Inner Join Clientes On Clientes.Codigo = Ocorrencia_vip.Cliente
				Inner Join Usuarios On Ocorrencia_vip.Age_Tecnico = Usuarios.Codigo 
				Where Age_Data = Convert(Char(10), GetDate(), 102) And Age_nro is not null And Ok_Data Is not null
				Group By Usuarios.Codigo, Usuarios.Nome,Ocorrencia_vip.Veiculo


				Union

				----Ocorrencias Iniciadas
				select  
					Usuarios.Codigo as UsuarioId, 
					Usuarios.Nome as UsuarioNome,
					Ocorrencia_vip.Veiculo as UsuarioVeiculo, 
					0  as QtdeOcorrencia, 
					0 as QtdeOcorrenciaConcluida,
					count(1) as QtdeOcorrenciaExecucao ,
					0 as QtdeOcorrenciaPendente,
					Case 
						When Max(DataFimVIP) IS NULL And Max(DataInicioVip) is not null Then Max(DataInicioVip)
						When Max(DataFimVIP) is not null Then Max(DataFimVIP)
						else Max(Data_Roteiro) End as DataUltAtualizacao
				from Ocorrencia_vip 
				--Inner Join Clientes On Clientes.Codigo = Ocorrencia_vip.Cliente
				Inner Join Usuarios On Ocorrencia_vip.Age_Tecnico = Usuarios.Codigo 
				Where  Age_Data = Convert(Char(10), GetDate(), 102) And 
					Age_nro is not null And DataInicioVIP is not null And DataFimVIP Is null And Ok_Data is null
				Group By  Usuarios.Codigo, Usuarios.Nome,Ocorrencia_vip.Veiculo

				Union

				--Total de ocorrencias pendentes
				select 
					Usuarios.Codigo as UsuarioId, 
					Usuarios.Nome as UsuarioNome,
					Ocorrencia_vip.Veiculo as UsuarioVeiculo, 
					0 as QtdeOcorrencia, 
					0 as QtdeOcorrenciaConcluida,
					0 as QtdeOcorrenciaExecucao,
					count(1) as QtdeOcorrenciaPendente,
					Case 
						When Max(DataFimVIP) IS NULL And Max(DataInicioVip) is not null Then Max(DataInicioVip)
						When Max(DataFimVIP) is not null Then Max(DataFimVIP)
						else Max(Data_Roteiro) End as DataUltAtualizacao
				from Ocorrencia_vip
				Inner Join Usuarios On Ocorrencia_vip.Age_Tecnico = Usuarios.Codigo 
				Where Age_Data = Convert(Char(10), GetDate(), 102) And Ok_Data is null And DataInicioVIP is null
				Group By Usuarios.Codigo, Usuarios.Nome,Ocorrencia_vip.Veiculo

	) as tmp
	Group By UsuarioId, UsuarioNome,UsuarioVeiculo










GO


