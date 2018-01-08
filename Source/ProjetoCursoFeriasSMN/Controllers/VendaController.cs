﻿using ProjetoCursoFeriasSMN.Web.Application.Applications;
using ProjetoCursoFeriasSMN.Web.Application.Model;
using System.Net;
using System.Web.Mvc;

namespace ProjetoCursoFeriasSMN.Controllers
{
    public class VendaController : BaseController
    {
        private readonly VendaApplication _appVenda = new VendaApplication();
        private readonly ClienteApplication _appCliente = new ClienteApplication();
        private readonly ProdutoApplication _appProduto = new ProdutoApplication();

        public ActionResult Listar(int codigoCliente)
        {
            var response = _appVenda.Get(codigoCliente);
            if (response.Status != HttpStatusCode.OK)
                return Error(response.ContentAsString);

            return View("GridVendas",response.Content);
        }

        public ActionResult Cadastrar()
        {
            //Trazendo a lista de clientes
            var responseClientes = _appCliente.Get();
            if (responseClientes.Status != HttpStatusCode.OK)
                return Error(responseClientes.ContentAsString);

            //Trazendo a lista de produtos
            var responseProdutos = _appProduto.Get();
            if (responseProdutos.Status != HttpStatusCode.OK)
                return Error(responseProdutos.ContentAsString);

            return View("CadastraVenda", new Venda
            {
                Clientes = new SelectList(responseClientes.Content, "CodigoCliente", "Nome"),
                Itens = responseProdutos.Content
            });
        }

        public ActionResult CadastrarConfirma(Venda venda)
        {
            var response = _appVenda.Post(venda);
            if (response.Status != HttpStatusCode.OK)
                return Error(response.ContentAsString);

            return Content(response.Content);
        }

        public ActionResult Deletar(int codigoVenda)
        {
            var response = _appVenda.GetVenda(codigoVenda);
            if (response.Status != HttpStatusCode.OK)
                return Error(response.ContentAsString);
            if (response.Content == null)
                return Error("Venda não encontrada");

            var cliente = _appCliente.GetCliente(response.Content.Cliente.CodigoCliente);
            if (cliente.Status != HttpStatusCode.OK)
                return Error(cliente.ContentAsString);
            if (cliente.Content == null)
                return Error("Cliente da venda não encontrado");

            response.Content.Cliente = cliente.Content;

            return View("DeletaVenda", response.Content);
        }

        public ActionResult DeletarConfirma(int codigoVenda)
        {
            var response = _appVenda.Delete(codigoVenda);
            if (response.Status != HttpStatusCode.OK)
                return Error(response.ContentAsString);

            return Content(response.Content);
        }

        public ActionResult DetalhaVenda(int codigoVenda)
        {
            var response = _appVenda.GetVenda(codigoVenda);
            if (response.Status != HttpStatusCode.OK)
                return Error(response.ContentAsString);

            return View("DetalhaVenda",response.Content);
        }
    }
}