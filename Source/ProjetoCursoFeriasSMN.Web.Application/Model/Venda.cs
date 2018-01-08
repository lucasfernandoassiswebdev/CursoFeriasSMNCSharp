﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ProjetoCursoFeriasSMN.Web.Application.Model
{
    public class Venda
    {
        public Cliente Cliente { get; set; }
        public Endereco Endereco { get; set; }
        public DateTime DataVenda { get; set; }
        public IEnumerable<Produto> Itens { get; set; }
        public SelectList Clientes { get; set; }
        public int CodigoVenda { get; set; }
        public int CodigoEnderecoEntrega { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Desconto { get; set; }
        public string IndicadorEntrega { get; set; }
        public string Complemento { get; set; }

        public decimal Total => SubTotal - Desconto;
    }
}