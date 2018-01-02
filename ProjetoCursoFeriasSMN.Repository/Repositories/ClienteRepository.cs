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
            CadastraCliente,
            EditaCliente,
            DeletaCliente,
            ListaClientes,
            SelecionaCliente
        }

        public string CadastraCliente(Cliente cliente)
        {
            ExecuteProcedure(Procedures.CadastraCliente);
            AddParameter("@login", cliente.Nome);
            AddParameter("@Cpf", cliente.Cpf);
            AddParameter("@Telefone", cliente.Telefone);
            AddParameter("@Email", cliente.Email);
            AddParameter("@Bairro", cliente.Endereco.Bairro);
            AddParameter("@Cep", cliente.Endereco.Cep);
            AddParameter("@Cidade", cliente.Endereco.Cidade);
            AddParameter("@Estado", cliente.Endereco.Estado);
            AddParameter("@Logradouro", cliente.Endereco.Logradouro);

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

        public string EditaCliente(Cliente cliente)
        {
            ExecuteProcedure(Procedures.CadastraCliente);
            AddParameter("@codigoCliente", cliente.CodigoCliente);
            AddParameter("@login", cliente.CodigoEnderecoEspecifico);
            AddParameter("@login", cliente.Nome);
            AddParameter("@cpf", cliente.Cpf);
            AddParameter("@telefone", cliente.Telefone);
            AddParameter("@email", cliente.Email);
            AddParameter("@bairro", cliente.Endereco.Bairro);
            AddParameter("@cep", cliente.Endereco.Cep);
            AddParameter("@cidade", cliente.Endereco.Cidade);
            AddParameter("@estado", cliente.Endereco.Estado);
            AddParameter("@logradouro", cliente.Endereco.Logradouro);

            var retorno = ExecuteNonQueryWithReturn();
            var mensagemRetorno = "";

            switch (retorno)
            {
                case 1: mensagemRetorno = "Pau 1"; break;
                case 2: mensagemRetorno = "Pau 2"; break;
                case 3: mensagemRetorno = "Pau 3"; break;
            }

            return mensagemRetorno != string.Empty ? mensagemRetorno : null;
        }

        public string DeletaCliente(int codigoCliente)
        {
            ExecuteProcedure(Procedures.DeletaCliente);
            AddParameter("@idCliente", codigoCliente);

            var retorno = ExecuteNonQueryWithReturn();
            var mensagemRetorno = "";

            switch (retorno)
            {
                case 1: mensagemRetorno = "Pau 1"; break;
                case 2: mensagemRetorno = "Pau 2"; break;
                case 3: mensagemRetorno = "Pau 3"; break;
            }

            return mensagemRetorno != string.Empty ? mensagemRetorno : null;
        }

        public Cliente SelecionaCliente(int codigoCliente)
        {
            ExecuteProcedure(Procedures.SelecionaCliente);
            AddParameter("@codigoCliente", codigoCliente);

            using (var reader = ExecuteReader())
            {
                if (reader.Read())
                    return new Cliente
                    {
                        CodigoCliente = reader.ReadAsInt("CodigoCliente"),
                        Nome = reader.ReadAsString("Nome"),
                        Cpf = reader.ReadAsString("Cpf"),
                        Email = reader.ReadAsString("Email"),
                        Telefone = reader.ReadAsString("Telefone"),
                        Endereco = new Endereco
                        {
                            Estado = reader.ReadAsString("Estado"),
                            Cidade = reader.ReadAsString("Cidade"),
                            Cep = reader.ReadAsString("Cep"),
                            Bairro = reader.ReadAsString("Bairro"),
                            Logradouro = reader.ReadAsString("Logradouro"),
                            CodigoEndereco = reader.ReadAsInt("CodigoEndereco")
                        }
                    };
            }

            return null;
        }

        public IEnumerable<Cliente> ListaClientes()
        {
            ExecuteProcedure(Procedures.ListaClientes);

            var listaClientes = new List<Cliente>();
            using (var reader = ExecuteReader())
            {
                while (reader.Read())
                {
                    listaClientes.Add(new Cliente
                    {
                        CodigoCliente = reader.ReadAsInt("CodigoCliente"),
                        Nome = reader.ReadAsString("Nome"),
                        Cpf = reader.ReadAsString("Cpf"),
                        Email = reader.ReadAsString("Email"),
                        Telefone = reader.ReadAsString("Telefone"),
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

            return listaClientes;
        }
    }
}
