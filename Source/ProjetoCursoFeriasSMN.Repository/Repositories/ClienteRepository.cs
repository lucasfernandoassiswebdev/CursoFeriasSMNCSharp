using ProjetoCursoFeriasSMN.Domain.Entidades;
using ProjetoCursoFeriasSMN.Repository.DataBase;
using System.Collections.Generic;

namespace ProjetoCursoFeriasSMN.Repository.Repositories
{
    public class ClienteRepository : Execucao
    {
        private static readonly Conexao Conexao = new Conexao();

        public ClienteRepository() : base(Conexao)
        {
        }

        //Enum contendo o nome de nossas procedures
        public enum Procedures
        {
            SP_InsCliente,
            SP_UpdDadosCliente,
            SP_DelCliente,
            SP_SelClientes,
            SP_SelDadosCliente
        }

        public string CadastraCliente(Cliente cliente)
        {
            ExecuteProcedure(Procedures.SP_InsCliente);
            AddParameter("@CPF", cliente.Cpf);
            AddParameter("@Nome", cliente.Nome);
            AddParameter("@Telefone", cliente.Telefone);
            AddParameter("@Email", cliente.Email);
            AddParameter("@Cep", cliente.Endereco.Cep);
            AddParameter("@Logradouro", cliente.Endereco.Logradouro);
            AddParameter("@Bairro", cliente.Endereco.Bairro);
            AddParameter("@Cidade", cliente.Endereco.Cidade);
            AddParameter("@UF", cliente.Endereco.Estado);
            AddParameter("@Numero", cliente.Numero);
            AddParameter("@Complemento", cliente.Complemento);


            var retorno = ExecuteNonQueryWithReturn();
            var mensagemRetorno = string.Empty;

            switch (retorno)
            {
                case 1: mensagemRetorno = "Erro ao inserir o endereço"; break;
                case 2: mensagemRetorno = "Erro ao inserir o cliente"; break;
            }

            return mensagemRetorno != string.Empty ? mensagemRetorno : null;
        }

        public string EditaCliente(Cliente cliente)
        {
            ExecuteProcedure(Procedures.SP_UpdDadosCliente);
            AddParameter("@CodigoCliente", cliente.CodigoCliente);
            AddParameter("@CodigoEndereco", cliente.CodigoEndereco);
            AddParameter("@CPF", cliente.Cpf);
            AddParameter("@Nome", cliente.Nome);
            AddParameter("@Telefone", cliente.Telefone);
            AddParameter("@Email", cliente.Email);
            AddParameter("@Cep", cliente.Endereco.Cep);
            AddParameter("@Logradouro", cliente.Endereco.Logradouro);
            AddParameter("@Bairro", cliente.Endereco.Bairro);
            AddParameter("@Cidade", cliente.Endereco.Cidade);
            AddParameter("@UF", cliente.Endereco.Estado);
            AddParameter("@Numero", cliente.Numero);
            AddParameter("@Complemento", cliente.Complemento);

            var retorno = ExecuteNonQueryWithReturn();
            var mensagemRetorno = string.Empty;

            switch (retorno)
            {
                case 1: mensagemRetorno = "Erro ao atualizar as informações do cliente"; break;
                case 2: mensagemRetorno = "Erro ao atualizar as informações do endereço"; break;
            }

            return mensagemRetorno != string.Empty ? mensagemRetorno : null;
        }

        public string DeletaCliente(int codigoCliente, int codigoEndereco)
        {
            ExecuteProcedure(Procedures.SP_DelCliente);
            AddParameter("@CodigoCliente", codigoCliente);
            AddParameter("@CodigoEndereco", codigoEndereco);

            var retorno = ExecuteNonQueryWithReturn();
            var mensagemRetorno = string.Empty;

            switch (retorno)
            {
                case 1: mensagemRetorno = "Erro ao deletar o cliente"; break;
                case 2: mensagemRetorno = "Erro ao deletar o endereço"; break;
            }

            return mensagemRetorno != string.Empty ? mensagemRetorno : null;
        }

        public Cliente SelecionaCliente(int codigoCliente)
        {
            ExecuteProcedure(Procedures.SP_SelDadosCliente);
            AddParameter("@CodigoCliente", codigoCliente);

            using (var reader = ExecuteReader())
                if (reader.Read())
                    return new Cliente
                    {
                        CodigoCliente = reader.ReadAsInt("CodigoCliente"),
                        Cpf = reader.ReadAsString("CPF"),
                        Nome = reader.ReadAsString("Nome"),
                        Telefone = reader.ReadAsString("Telefone"),
                        Email = reader.ReadAsString("Email"),
                        Numero = reader.ReadAsShort("Numero"),
                        Complemento = reader.ReadAsString("Complemento"),
                        Endereco = new Endereco
                        {
                            CodigoEndereco = reader.ReadAsInt("CodigoEndereco"),
                            Cep = reader.ReadAsInt("Cep"),
                            Logradouro = reader.ReadAsString("Logradouro"),
                            Bairro = reader.ReadAsString("Bairro"),
                            Cidade = reader.ReadAsString("Cidade"),
                            Estado = reader.ReadAsString("UF")
                        }
                    };

            return null;
        }

        public IEnumerable<Cliente> ListaClientes()
        {
            ExecuteProcedure(Procedures.SP_SelClientes);

            var listaClientes = new List<Cliente>();

            using (var reader = ExecuteReader())
                while (reader.Read())
                    listaClientes.Add(new Cliente
                    {
                        CodigoCliente = reader.ReadAsInt("CodigoCliente"),
                        CodigoEndereco = reader.ReadAsInt("CodigoEndereco"),
                        Cpf = reader.ReadAsString("CPF"),
                        Nome = reader.ReadAsString("Nome"),
                        Telefone = reader.ReadAsString("Telefone"),
                        Email = reader.ReadAsString("Email")
                    });

            return listaClientes;
        }
    }
}
