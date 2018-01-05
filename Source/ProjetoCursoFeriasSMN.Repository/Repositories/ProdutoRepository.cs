using ProjetoCursoFeriasSMN.Domain.Entidades;
using ProjetoCursoFeriasSMN.Repository.DataBase;
using System.Collections.Generic;

namespace ProjetoCursoFeriasSMN.Repository.Repositories
{
    public class ProdutoRepository : Execucao
    {
        private static readonly Conexao Conexao = new Conexao();

        public ProdutoRepository() : base(Conexao)
        {
        }

        public enum Procedures
        {
            SP_InsProduto,
            SP_UpdProduto,
            SP_DelProduto,
            SP_SelProdutos,
            SP_SelDadosProduto
        }

        public string CadastraProduto(Produto produto)
        {
            ExecuteProcedure(Procedures.SP_InsProduto);
            AddParameter("@Nome", produto.Nome);
            AddParameter("@Preco", produto.Preco);
            AddParameter("@Estoque", produto.Estoque);

            var retorno = ExecuteNonQueryWithReturn();
            var mensagemRetorno = string.Empty;

            switch (retorno)
            {
                case 1: mensagemRetorno = "Erro ao inserir o produto"; break;
            }

            return mensagemRetorno != string.Empty ? mensagemRetorno : null;
        }

        public string EditaProduto(Produto produto)
        {
            ExecuteProcedure(Procedures.SP_UpdProduto);
            AddParameter("@CodigoProduto", produto.CodigoProduto);
            AddParameter("@Nome", produto.Nome);
            AddParameter("@Preco", produto.Preco);
            AddParameter("@Estoque", produto.Estoque);

            var retorno = ExecuteNonQueryWithReturn();
            var mensagemRetorno = string.Empty;

            switch (retorno)
            {
                case 1: mensagemRetorno = "Erro ao atualizar as informações do produto"; break;
            }

            return mensagemRetorno != string.Empty ? mensagemRetorno : null;
        }

        public string DeletaProduto(int codigoProduto)
        {
            ExecuteProcedure(Procedures.SP_DelProduto);
            AddParameter("@idProduto", codigoProduto);

            var retorno = ExecuteNonQueryWithReturn();
            var mensagemRetorno = string.Empty;

            switch (retorno)
            {
                case 1: mensagemRetorno = "Exclusão não permitida, o produto esta vinculada a uma venda"; break;
                case 2: mensagemRetorno = "Erro ao excluir o produto"; break;
            }

            return mensagemRetorno != string.Empty ? mensagemRetorno : null;
        }

        public IEnumerable<Produto> ListaProdutos()
        {
            ExecuteProcedure(Procedures.SP_SelProdutos);

            var listaProdutos = new List<Produto>();

            using (var reader = ExecuteReader())
                while (reader.Read())
                    listaProdutos.Add(new Produto
                    {
                        CodigoProduto = reader.ReadAsInt("CodigoProduto"),
                        Nome = reader.ReadAsString("Nome"),
                        Preco = reader.ReadAsDecimal("Preco"),
                        Estoque = reader.ReadAsShort("Estoque")
                    });

            return listaProdutos;
        }

        public Produto SelecionaProduto(int codigoProduto)
        {
            ExecuteProcedure(Procedures.SP_SelDadosProduto);
            AddParameter("@codigoProduto", codigoProduto);

            using (var reader = ExecuteReader())
                if (reader.Read())
                    return new Produto
                    {
                        CodigoProduto = reader.ReadAsInt("CodigoProduto"),
                        Nome = reader.ReadAsString("Nome"),
                        Preco = reader.ReadAsDecimal("Preco"),
                        Estoque = reader.ReadAsShort("Estoque")
                    };

            return null;
        }
    }
}
