namespace ProjetoCursoFeriasSMN.Domain.Entidades
{
    public class Cliente
    {
        public int CodigoCliente { get; set; }
        public int CodigoEndereco { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public Endereco Endereco { get; set; }
    }
}
