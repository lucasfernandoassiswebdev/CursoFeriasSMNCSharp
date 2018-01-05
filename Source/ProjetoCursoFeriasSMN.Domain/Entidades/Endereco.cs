namespace ProjetoCursoFeriasSMN.Domain.Entidades
{
    public class Endereco
    {
        public int? CodigoEndereco { get; set; }
        public int Cep { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}
