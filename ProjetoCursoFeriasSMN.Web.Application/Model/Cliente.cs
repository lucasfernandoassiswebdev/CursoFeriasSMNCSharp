namespace ProjetoCursoFeriasSMN.Models
{
    public class Cliente
    {
        public int CodigoCliente { get; set; }
        public int CodigoEnderecoEspecifico { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public Endereco Endereco { get; set; }
    }
}