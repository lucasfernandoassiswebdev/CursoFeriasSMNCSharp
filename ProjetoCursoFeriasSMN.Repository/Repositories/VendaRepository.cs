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
            CadastraVenda,
            CadastraItensVenda,
            DeletaVenda,
            ListaVendas
        }

        public string CadastraVenda(Venda venda)
        {
            BeginTransaction();
            ExecuteProcedure(Procedures.CadastraVenda);

            AddParameter("@CodigoCliente", venda.CodigoCliente);
            AddParameter("@CodigoEnderecoEspecifico", venda.CodigoEnderecoEspecifico);
            AddParameter("@DataVenda", venda.DataVenda);
            AddParameter("@Desconto", venda.Desconto);
            AddParameter("@SubTotal", venda.SubTotal);
            AddParameter("@Total", venda.Total);

            var retorno = ExecuteNonQueryWithReturn();
            var mensagemRetorno = string.Empty;

            switch (retorno)
            {
                case -1: mensagemRetorno = "Pau 1"; break;
                case -2: mensagemRetorno = "Pau 2"; break;
                case -3: mensagemRetorno = "Pau 3"; break;
            }

            if (string.IsNullOrEmpty(mensagemRetorno))
            {
                var mensagemRetornoItens = CadastraItensVenda(venda.Itens, retorno);
                if (string.IsNullOrEmpty(mensagemRetornoItens))
                    CommitTransaction();
                else
                {
                    RollBackTransaction();
                    mensagemRetorno += " - " + mensagemRetornoItens;
                }
            }

            return mensagemRetorno != string.Empty ? mensagemRetorno : null;
        }

        public string CadastraItensVenda(IEnumerable<Itens> listaItens, int codigoVenda)
        {
            ExecuteProcedure(Procedures.CadastraItensVenda);

            var mensagemRetorno = string.Empty;
            foreach (var item in listaItens)
            {
                AddParameter("@CodigoVenda", item.CodigoVenda);
                AddParameter("@CodigoProduto", item.CodigoProduto);
                AddParameter("@Preco", item.Preco);
                AddParameter("@QuantidadeVendida", item.QuantidadeVendida);

                var retorno = ExecuteNonQueryWithReturn();

                switch (retorno)
                {
                    case 1: mensagemRetorno = "Pau 1"; break;
                    case 2: mensagemRetorno = "Pau 2"; break;
                    case 3: mensagemRetorno = "Pau 3"; break;
                }
            }

            return mensagemRetorno != string.Empty ? mensagemRetorno : null;
        }

        public string DeletaVenda(int idVenda)
        {
            ExecuteProcedure(Procedures.DeletaVenda);
            AddParameter("@idVenda", idVenda);

            var retorno = ExecuteNonQueryWithReturn();
            var mensagemRetorno = string.Empty;

            switch (retorno)
            {
                case -1: mensagemRetorno = "Pau 1"; break;
                case -2: mensagemRetorno = "Pau 2"; break;
                case -3: mensagemRetorno = "Pau 3"; break;
            }

            return mensagemRetorno != string.Empty ? mensagemRetorno : null;
        }

        public IEnumerable<Venda> ListaVendas(int codigoCliente)
        {
            ExecuteProcedure(Procedures.ListaVendas);
            AddParameter("@codigoCliente", codigoCliente);

            var listaVendas = new List<Venda>();
            using (var reader = ExecuteReader())
            {
                while (reader.Read())
                {
                    listaVendas.Add(new Venda
                    {
                        CodigoCliente = reader.ReadAsInt("CodigoCliente"),
                        NomeCliente = reader.ReadAsString("NomeCliente"),
                        DataVenda = reader.ReadAsDateTime("DataVenda"),
                        Desconto = reader.ReadAsDecimal("Desconto"),
                        SubTotal = reader.ReadAsDecimal("SubTotal"),
                        Endereco = new Endereco
                        {
                            Estado = reader.ReadAsString("Estado"),
                            Cidade = reader.ReadAsString("Cidade"),
                            Cep = reader.ReadAsString("Cep"),
                            Bairro = reader.ReadAsString("Bairro"),
                            Logradouro = reader.ReadAsString("Logradouro"),
                            CodigoEndereco = reader.ReadAsInt("CodigoEndereco")
                        }
                    });
                }
            }

            return listaVendas;
        }
    }
}
