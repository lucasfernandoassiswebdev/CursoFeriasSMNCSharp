IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_SelProdutos]') AND objectproperty(id, N'IsPROCEDURE') = 1)
	DROP PROCEDURE [dbo].[SP_SelProdutos]

GO

CREATE PROCEDURE [dbo].[SP_SelProdutos]

	AS

	/* Documentação

		Arquivo.......: Produtos.sql
		Autor.........: Bruno Alves
		Data..........: 04/01/2017
		Objetivo......: Seleciona produtos já cadastrados.

		Exemplo....... EXEC [dbo].[SP_SelProdutos]

	*/

	BEGIN

		SELECT CodigoProduto,
			   Nome,
			   Preco,
			   Estoque
			FROM [dbo].[Produtos]

	END

GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_InsProduto]') AND objectproperty(id, N'IsPROCEDURE') = 1)
	DROP PROCEDURE [dbo].[SP_InsProduto]

GO

CREATE PROCEDURE [dbo].[SP_InsProduto]
	@Nome		varchar(50),
	@Preco		decimal(10,2),
	@Estoque	smallint

	AS

	/* Documentação

		Arquivo.......: Produtos.sql
		Autor.........: Bruno Alves
		Data..........: 04/01/2017
		Objetivo......: Cadastrar um novo produto.
		Retornos......: 0 - Processamento OK!
						1 - Erro ao inserir o produto.

		Exemplo....... EXEC [dbo].[SP_SelProdutos]

	*/

	BEGIN

		INSERT INTO [dbo].[Produtos](Nome, Preco, Estoque)
			VALUES(@Nome, @Preco, @Estoque)

		IF @@ERROR <> 0 OR @@ROWCOUNT = 0
			RETURN 1

		RETURN 0

	END

GO