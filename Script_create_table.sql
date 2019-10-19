USE [CadastroDB]

CREATE TABLE [dbo].[Funcionarios](
	[FuncionarioId] [int] identity(1,1) not null,
	[Nome][nvarchar](80) not null,
	[Cidade][nvarchar](50) not null,
	[Departamento][nvarchar](50) null,
	[Sexo][nvarchar](10) not null
)