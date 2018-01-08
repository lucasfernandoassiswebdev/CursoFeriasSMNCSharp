namespace ProjetoCursoFeriasSMN.Domain.Entidades
{
    public class Produto
    {
        public int CodigoProduto { get; set; }
        public int QuantidadeVendida { get; set; }
        public string Nome { get; set; }
        public decimal Total { get; set; }
        public int Estoque { get; set; }
        public decimal Preco { get; set; }
    }
}
