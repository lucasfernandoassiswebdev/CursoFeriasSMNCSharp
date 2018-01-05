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
            EditaProduto,
            DeletaProdtuo,
            SP_SelProdutos,
            SelecionarProduto
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
            ExecuteProcedure(Procedures.EditaProduto);
            AddParameter("@nome", produto.Nome);
            AddParameter("@estoque", produto.Estoque);
            AddParameter("@idProduto", produto.CodigoProduto);

            var retorno = ExecuteNonQueryWithReturn();
            var mensagemRetorno = string.Empty;

            switch (retorno)
            {
                case 1: mensagemRetorno = "Pau 1"; break;
                case 2: mensagemRetorno = "Pau 2"; break;
                case 3: mensagemRetorno = "Pau 3"; break;
            }

            return mensagemRetorno != string.Empty ? mensagemRetorno : null;
        }

        public string DeletaProduto(int codigoProduto)
        {
            ExecuteProcedure(Procedures.DeletaProdtuo);
            AddParameter("@idProduto", codigoProduto);

            var retorno = ExecuteNonQueryWithReturn();
            var mensagemRetorno = string.Empty;

            switch (retorno)
            {
                case 1: mensagemRetorno = "Pau 1"; break;
                case 2: mensagemRetorno = "Pau 2"; break;
                case 3: mensagemRetorno = "Pau 3"; break;
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
            ExecuteProcedure(Procedures.SelecionarProduto);
            AddParameter("@codigoProduto", codigoProduto);

            using (var reader = ExecuteReader())
                if (reader.Read())
                    return new Produto
                    {
                        CodigoProduto = reader.ReadAsInt("CodigoProduto"),
                        Nome = reader.ReadAsString("Nome"),
                        Preco = reader.ReadAsDecimal("Preco"),
                        Estoque = reader.ReadAsInt("Estoque")
                    };

            return null;
        }
    }
}
