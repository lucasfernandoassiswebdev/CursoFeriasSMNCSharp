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
            CadastraItensVenda,
            SP_DelVenda,
            SP_SelVendas,
            SP_SelItensVenda,
            SP_InsItensVenda,
        }

        public string CadastraVenda(Venda venda)
        {
            BeginTransaction();
            ExecuteProcedure(Procedures.SP_InsVenda);

            AddParameter("@CodigoCliente", venda.CodigoCliente);
            AddParameter("@SubTotal", venda.SubTotal);
            AddParameter("@Total", venda.Total);
            AddParameter("@Desconto", venda.Desconto);
            AddParameter("@DataVenda", venda.DataVenda);
            

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

        public string CadastraItensVenda(IEnumerable<Produto> listaItens, int codigoVenda)
        {
            ExecuteProcedure(Procedures.CadastraItensVenda);
            AddParameter("@CodigoVenda", codigoVenda);

            var mensagemRetorno = string.Empty;
            foreach (var item in listaItens)
            {
                AddParameter("@CodigoProduto", item.CodigoProduto);
                AddParameter("@Preco", item.Preco);
                //AddParameter("@QuantidadeVendida", item.QuantidadeVendida);

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
            //ExecuteProcedure(Procedures.DeletaVenda);
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

        public Venda SelecionaVenda(int codigoVenda)
        {
            //ExecuteProcedure(Procedures.ListaVenda);
            AddParameter("@codigoVenda", codigoVenda);

            using (var reader = ExecuteReader())
                if (reader.Read())
                    return new Venda
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
                    };


            return null;
        }
    }
}
