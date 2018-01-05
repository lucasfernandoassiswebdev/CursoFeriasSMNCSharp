IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_SelVendas]') AND objectproperty(id, N'IsPROCEDURE') = 1)
	DROP PROCEDURE [dbo].[SP_SelVendas]

GO

CREATE PROCEDURE [dbo].[SP_SelVendas]
	@CodigoCliente	int

	AS

	/* Documentação

		Arquivo.......: Venda.sql
		Autor.........: Bruno Alves
		Data..........: 04/01/2017
		Objetivo......: Seleciona vendas já cadastradas.

		Exemplo....... EXEC [dbo].[SP_SelVendas]

	*/

	BEGIN

		SELECT CodigoVenda,
			   DataVenda,
			   SubTotal,
			   Desconto,
			   Total
			FROM [dbo].[Vendas]
			WHERE CodigoCliente = @CodigoCliente

	END

GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_InsVenda]') AND objectproperty(id, N'IsPROCEDURE') = 1)
	DROP PROCEDURE [dbo].[SP_InsVenda]

GO

CREATE PROCEDURE [dbo].[SP_InsVenda]
	@CodigoCliente	int,
	@SubTotal		decimal(10,2),
	@Total			decimal(10,2),
	@Desconto		decimal(10,2),
	@Entrega		char(1)

	AS

	/* Documentação

		Arquivo.......: Venda.sql
		Autor.........: Bruno Alves
		Data..........: 04/01/2017
		Objetivo......: Cadastra uma nova venda
		Retornos......: -1 - Erro ao cadastrar a venda.
						
		Exemplo....... EXEC [dbo].[SP_InsVenda]

	*/

	BEGIN

		INSERT INTO [dbo].[Vendas](CodigoCliente, DataVenda, SubTotal, Desconto, Total, Entrega)
			VALUES(@CodigoCliente, GETDATE(), @SubTotal, @Desconto, @Total, @Entrega) 

		IF @@ERROR <> 0 OR @@ROWCOUNT = 0
			RETURN -1

		RETURN SCOPE_IDENTITY()

	END

GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_InsItensVenda]') AND objectproperty(id, N'IsPROCEDURE') = 1)
	DROP PROCEDURE [dbo].[SP_InsItensVenda]

GO

CREATE PROCEDURE [dbo].[SP_InsItensVenda]
	@CodigoVenda	int,
	@CodigoProduto	int,
	@Quantidade		int

	AS

	/* Documentação

		Arquivo.......: Venda.sql
		Autor.........: Bruno Alves
		Data..........: 04/01/2017
		Objetivo......: Cadastra o item de uma venda
		Retornos......: 0 - Processamento OK!
						1 - Produto esgotado.
						2 - Erro ao cadastrar o item da venda.
						3 - Erro ao atualizar o estoque do produto.

		Exemplo....... EXEC [dbo].[SP_InsItensVenda]

	*/

	BEGIN

		IF (SELECT Estoque - @Quantidade
				FROM [dbo].[Produtos] 
				WHERE CodigoProduto = @CodigoProduto) <= 0
			RETURN 1

		INSERT INTO [dbo].[VendaItem](CodigoVenda, CodigoProduto, QuantidadeVendida)
			VALUES(@CodigoVenda, @CodigoProduto, @Quantidade)

		IF @@ERROR <> 0 OR @@ROWCOUNT = 0
			RETURN 2

		UPDATE [dbo].[Produtos]
			SET Estoque = Estoque - @Quantidade
			WHERE CodigoProduto = @CodigoProduto

		IF @@ERROR <> 0 OR @@ROWCOUNT = 0
			RETURN 3

		RETURN 0

	END

GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_DelVenda]') AND objectproperty(id, N'IsPROCEDURE') = 1)
	DROP PROCEDURE [dbo].[SP_DelVenda]

GO

CREATE PROCEDURE [dbo].[SP_DelVenda]
	@CodigoVenda	int

	AS

	/* Documentação

		Arquivo.......: Venda.sql
		Autor.........: Bruno Alves
		Data..........: 04/01/2017
		Objetivo......: Deleta uma venda.
		Retornos......: 0 - Processamento OK!
						1 - Erro ao excluir os itens da venda.
						2 - Erro ao excluir a venda.

		Exemplo....... EXEC [dbo].[SP_DelVenda]

	*/

	BEGIN

		BEGIN TRANSACTION

			DELETE FROM [dbo].[VendaItem]
				WHERE CodigoVenda = @CodigoVenda

			IF @@ERROR <> 0 OR @@ROWCOUNT = 0
				BEGIN
					ROLLBACK TRANSACTION
					RETURN 1
				END

			DELETE FROM [dbo].[Vendas]
				WHERE CodigoVenda = @CodigoVenda

			IF @@ERROR <> 0 OR @@ROWCOUNT = 0
				BEGIN
					ROLLBACK TRANSACTION
					RETURN 2
				END

		COMMIT TRANSACTION
		RETURN 0

	END

GO


