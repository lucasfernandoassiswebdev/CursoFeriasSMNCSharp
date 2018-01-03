using System;
using System.Collections.Generic;

namespace ProjetoCursoFeriasSMN.Web.Application.Model
{
    public class Venda
    {
        public int CodigoCliente { get; set; }
        public int CodigoVenda { get; set; }
        public int CodigoEnderecoEntrega { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Desconto { get; set; }
        public IEnumerable<Produto> Itens;
        public Cliente Cliente { get; set; }
        public Endereco Endereco { get; set; }

        public decimal Total => SubTotal - Desconto;
    }
}