﻿using ProjetoCursoFeriasSMN.Models;
using ProjetoCursoFeriasSMN.Web.Application.Applications;
using System.Net;
using System.Web.Mvc;

namespace ProjetoCursoFeriasSMN.Controllers
{
    public class ProdutoController : Controller
    {
        public ActionResult Salvar(Produto produto)
        {
            var appProduto = new ProdutoApplication();

            var response = produto.CodigoProduto != 0 ? appProduto.Put(produto) : appProduto.Post(produto);
            if (response.Status != HttpStatusCode.OK)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Response.TrySkipIisCustomErrors = true;
                return Content(response.Content);
            }

            return Content(response.Content);
        }

        public ActionResult Deletar(int codigoProduto)
        {
            var appProduto = new ProdutoApplication();

            var response = appProduto.Delete(codigoProduto);
            if (response.Status != HttpStatusCode.OK)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Response.TrySkipIisCustomErrors = true;
                return Content(response.Content);
            }

            return Content(response.Content);
        }

        public ActionResult Listar()
        {
            var appProduto = new ProdutoApplication();

            var response = appProduto.Get();
            if (response.Status != HttpStatusCode.OK)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Response.TrySkipIisCustomErrors = true;
                return Content("Erro ao listar clientes");
            }

            return View("GridProdutos", response.Content);
        }
    }
}