namespace ProjetoCursoFeriasSMN.Web.Application.Model
{
    public class Produto
    {
        public int CodigoProduto { get; set;}
        public string Nome { get; set; }
        public int Estoque { get; set; }
        public int QuantidadeVendida { get; set; }
        public decimal Preco { get; set; }
    }
}