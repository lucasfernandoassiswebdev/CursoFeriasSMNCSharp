using System;
using System.Collections.Generic;

namespace ProjetoCursoFeriasSMN.Domain.Entidades
{
    public class Venda
    {
        public int CodigoVenda { get; set; }
        public int CodigoCliente { get; set; }
        public int CodigoEnderecoEspecifico { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Desconto { get; set; }
        public Endereco Endereco { get; set; }
        public IEnumerable<Produto> Itens { get; set; }

        public decimal Total => SubTotal - Desconto;
    }
}
