If Exists (Select NAME From  SYSOBJECTS Where  NAME = N'Vw_OcorrenciaRat' And TYPE = 'V')
    Drop VIEW Vw_OcorrenciaRat
Go

CREATE View [dbo].[Vw_OcorrenciaRat]
as

select
	                Ocorrencia.ocorrencia as OcorrenciaId,
                    RoteiroId = (Select top 1 Roteiro_2.nro From Roteiro_2 Where Ocorrencia.ocorrencia = Roteiro_2.Ocorrencia Order By Chave desc),
					Sequencia = (Select top 1 Roteiro_2.Ordem From Roteiro_2 Where Ocorrencia.ocorrencia = Roteiro_2.Ocorrencia Order By Chave desc),
	                Ocorrencia.cliente as ClienteId,
	                Ocorrencia.Age_Tecnico as UsuarioId,
	                Ocorrencia.data as DataOcorrencia,
                    Ocorrencia.Ok_Nro as Rat,
	                datediff(day, Ocorrencia.data, getdate()) as QuantidadeDias,
	                Ocorrencia.data as DhOcorrencia,
	                Ocorrencia.data_roteiro as DhRoteiro,
                    Ocorrencia.Contato as Contato,
	                case
		                when (isnull(Ocorrencia.ok_data, 0) > 0)		then 3  
		                when (isnull(Ocorrencia.age_tecnico, 0) > 0)	and (isnull(ok_data, 0) = 0) then 2 
		                else 0
	                end as StatusId,
	                case
		                when Ocorrencia.prioridade = 'alta'			then 0
		                when Ocorrencia.prioridade = 'media'		then 1
		                else 2
	                end as Prioridade,
	                Ocorrencia.problema as Comentario,
	                Ocorrencia.problema_desc as Problema,
                    Ocorrencia.Solucao_desc as Solucao,
                    Ocorrencia.DataInicioVIP,
	                Ocorrencia.DataFimVIP,
					Ocorrencia.Ok_Data,
					Ocorrencia.enc_tecnico,
					Ocorrencia.Depto,
					
	                --clientes
	                clientes.cnpj as Cnpj,
	                clientes.nome as Nome,
	                clientes.razão as RazaoSocial,
	                clientes.cep as Cep,
	                clientes.endereço as Endereco,
	                clientes.end_num as Numero,
	                clientes.bairro as Bairro,
	                clientes.cidade as Cidade,
	                clientes.estado as Uf,
                    Isnull(clientes.DDD,0) + Isnull(clientes.Fone1,0) as Fone,
	                --usuario

	                excutando.nome	as NomeUsuario,
	                excutando.nome as [User],
	                excutando.senha as [Password],

                    frota_1.Nome as Veiculo
                from ocorrencia_vip Ocorrencia
                left join clientes on clientes.codigo = Ocorrencia.cliente
                left join usuarios emcaminhado	on Ocorrencia.enc_tecnico = emcaminhado.codigo
                left join usuarios excutando	on Ocorrencia.age_tecnico = excutando.codigo
                left join Roteiro_1 On Ocorrencia.Age_nro = Roteiro_1.Nro
                left join Roteiro_2 On Ocorrencia.ocorrencia = Roteiro_2.Ocorrencia
	            Left Join frota_1 On Roteiro_1.Veiculo = frota_1.Codigo
GO