using ProjetoCursoFeriasSMN.Domain.Entidades;
using ProjetoCursoFeriasSMN.Repository.DataBase;

namespace ProjetoCursoFeriasSMN.Repository.Repositories
{
    public class ClienteRepository : Execucao
    {
        private static  readonly Conexao Conexao = new Conexao();
        public ClienteRepository() : base(Conexao)
        {
        }

        //Enum contendo o nome de nossas procedures
        public enum Procedures
        {
            VerificaLogin
        }

        public Cliente VerificaLogin(Cliente cliente)
        {
            ExecuteProcedure(Procedures.VerificaLogin);
            AddParameter("@login",cliente.Login);
            AddParameter("@senha",cliente.Senha);

            using (var reader = ExecuteReader())
            {
                //Caso o login esteja correto a proc retornará o cliente deste login
                if (reader.Read())
                    return new Cliente
                    {
                        CodigoCliente = reader.ReadAsInt("CodCliente"),
                        Nome = reader.ReadAsString("Nome"),
                        Telefone = reader.ReadAsString("Telefone"),
                        Email = reader.ReadAsString("Email"),
                        Endereco = new Endereco
                        {
                            Cep = reader.ReadAsString("Cep"),
                            Logradouro = reader.ReadAsString("Logradouro"),
                            Bairro = reader.ReadAsString("Bairro"),
                            Cidade = reader.ReadAsString("Cidade"),
                            Estado = reader.ReadAsString("Estado")
                        }
                    };
            }
            //Caso o login esteja incorreto o método retorna null
            return null;
        }
    }
}
