using System;

namespace ProjetoCursoFeriasSMN.Models
{
    public class Venda
    {
        public int CodigoVenda { get; set; }
        public int CodigoCliente { get; set; }
        public int CodigoEnderecoEntrega { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Desconto { get; set; }

        public decimal Total => SubTotal - Desconto;
    }
}