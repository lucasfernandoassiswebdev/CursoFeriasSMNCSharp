using ProjetoCursoFeriasSMN.Domain.Entidades;
using ProjetoCursoFeriasSMN.Repository.DataBase;
using System.Collections.Generic;

namespace ProjetoCursoFeriasSMN.Repository.Repositories
{
    public class VendaRepository : Execucao
    {
        private static readonly Conexao Conexao = new Conexao();

        public VendaRepository() : base(Conexao)
        {
        }

        public enum Procedures
        {
            SP_InsVenda,
            SP_InsItensVenda,
            SP_DelVenda,
            SP_SelVendas,
            SP_SelItensVenda
        }

        public string CadastraVenda(Venda venda)
        {
            BeginTransaction();
            ExecuteProcedure(Procedures.SP_InsVenda);

            AddParameter("@CodigoCliente", venda.Cliente.CodigoCliente);
            AddParameter("@SubTotal", venda.SubTotal);
            AddParameter("@Total", venda.Total);
            AddParameter("@Desconto", venda.Desconto);
            AddParameter("@Entrega", venda.IndicadorEntrega);
            if (venda.IndicadorEntrega == "S")
            {
                AddParameter("@Cep", venda.Endereco.Cep);
                AddParameter("@Logradouro", venda.Endereco.Cep);
                AddParameter("@Bairro", venda.Endereco.Bairro);
                AddParameter("@Cidade", venda.Endereco.Cidade);
                AddParameter("@UF", venda.Endereco.Estado);
                AddParameter("@Numero", venda.Cliente.Numero);
                AddParameter("@Complemento", venda.Cliente.Complemento);
            }


            var retorno = ExecuteNonQueryWithReturn();
            var mensagemRetorno = string.Empty;

            switch (retorno)
            {
                case -1: mensagemRetorno = "Erro ao cadastrar a venda"; break;
                case -2: mensagemRetorno = "Erro ao cadastrar o endereço"; break;
                case -3: mensagemRetorno = "Erro ao cadastrar o endereço de entrega"; break;
            }

            if (string.IsNullOrEmpty(mensagemRetorno))
            {
                var mensagemRetornoItens = CadastraItensVenda(venda.Itens, retorno);
                if (!string.IsNullOrEmpty(mensagemRetornoItens))
                {
                    RollBackTransaction();
                    mensagemRetorno = mensagemRetornoItens;
                }
            }

            return mensagemRetorno != string.Empty ? mensagemRetorno : null;
        }

        public string CadastraItensVenda(IEnumerable<Produto> listaItens, int codigoVenda)
        {
            ExecuteProcedure(Procedures.SP_InsItensVenda);
            AddParameter("@CodigoVenda", codigoVenda);

            var mensagemRetorno = string.Empty;
            foreach (var item in listaItens)
            {
                AddParameter("@CodigoProduto", item.CodigoProduto);
                AddParameter("@Quantidade", item.QuantidadeVendida);

                var retorno = ExecuteNonQueryWithReturn();

                switch (retorno)
                {
                    case 1: mensagemRetorno = "Produto esgotado"; break;
                    case 2: mensagemRetorno = "Erro ao cadastrar o item da venda"; break;
                    case 3: mensagemRetorno = "Erro ao atualizar o estoque do produto"; break;
                }

                if (string.IsNullOrEmpty(mensagemRetorno))
                {
                    RollBackTransaction();
                    break;
                }
            }

            CommitTransaction();
            return mensagemRetorno != string.Empty ? mensagemRetorno : null;
        }

        public string DeletaVenda(int codigoVenda)
        {
            ExecuteProcedure(Procedures.SP_DelVenda);

            //ExecuteProcedure(Procedures.DeletaVenda);
            AddParameter("@CodigoVenda", codigoVenda);

            var retorno = ExecuteNonQueryWithReturn();
            var mensagemRetorno = string.Empty;

            switch (retorno)
            {
                case 1: mensagemRetorno = "Erro ao excluir os itens da venda"; break;
                case 2: mensagemRetorno = "Erro ao excluir a venda"; break;
            }

            return mensagemRetorno != string.Empty ? mensagemRetorno : null;
        }

        public IEnumerable<Venda> ListaVendas(int codigoCliente)
        {
            //ExecuteProcedure(Procedures.ListaVendas);
            AddParameter("@codigoCliente", codigoCliente);

            var listaVendas = new List<Venda>();
            using (var reader = ExecuteReader())
                while (reader.Read())
                    listaVendas.Add(new Venda
                    {
                        CodigoCliente = reader.ReadAsInt("CodigoCliente"),
                        DataVenda = reader.ReadAsDateTime("DataVenda"),
                        Desconto = reader.ReadAsDecimal("Desconto"),
                        SubTotal = reader.ReadAsDecimal("SubTotal"),
                        Endereco = new Endereco
                        {
                            Estado = reader.ReadAsString("Estado"),
                            Cidade = reader.ReadAsString("Cidade"),
                            Cep = reader.ReadAsInt("Cep"),
                            Bairro = reader.ReadAsString("Bairro"),
                            Logradouro = reader.ReadAsString("Logradouro"),
                            CodigoEndereco = reader.ReadAsInt("CodigoEndereco")
                        }
                    });

            return listaVendas;
        }

        public IEnumerable<Produto> ListaItensVenda(int codigoVenda)
        {
            //ExecuteProcedure(Procedures.ListaVenda);
            AddParameter("@codigoVenda", codigoVenda);

            var listaProdutos = new List<Produto>();

            using (var reader = ExecuteReader())
                if (reader.Read())
                    listaProdutos.Add(new Produto
                    {
                        CodigoProduto = reader.ReadAsInt("CodigoProduto"),
                        Nome = reader.ReadAsString("Nome"),
                        Preco = reader.ReadAsDecimal("Preco"),
                        QuantidadeVendida = reader.ReadAsInt("QuantidadeVendida"),
                        Total = reader.ReadAsDecimal("Total")
                    });


            return listaProdutos;
        }
    }
}
