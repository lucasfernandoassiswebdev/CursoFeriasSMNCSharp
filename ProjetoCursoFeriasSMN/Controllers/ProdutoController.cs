using ProjetoCursoFeriasSMN.Models;
using ProjetoCursoFeriasSMN.Web.Application.Applications;
using System.Net;
using System.Web.Mvc;

namespace ProjetoCursoFeriasSMN.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly  ProdutoApplication _appProduto = new ProdutoApplication();

        public ActionResult Editar(int codigoProduto)
        {
            var response = _appProduto.GetProduto(codigoProduto);
            if (response.Status != HttpStatusCode.OK)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Response.TrySkipIisCustomErrors = true;
                return Content("Erro ao listar clientes");
            }

            return View("EditaProduto", response.Content);
        }

        public ActionResult Salvar(Produto produto)
        {
            var response = produto.CodigoProduto != 0 ? _appProduto.Put(produto) : _appProduto.Post(produto);
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
            var response = _appProduto.GetProduto(codigoProduto);
            if (response.Status != HttpStatusCode.OK)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Response.TrySkipIisCustomErrors = true;
                return Content(response.ContentAsString);
            }

            return View("DeletaProduto",response.Content);
        }

        public ActionResult DeletarConfirmado(int codigoProduto)
        {
           var response = _appProduto.Delete(codigoProduto);
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
            var response = _appProduto.Get();
            if (response.Status != HttpStatusCode.OK)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Response.TrySkipIisCustomErrors = true;
                return Content("Erro ao listar clientes");
            }

            return View("GridProdutos", response.Content);
        }

        public ActionResult DetalharProduto(int codigoProduto)
        {
            var response = _appProduto.GetProduto(codigoProduto);
            if (response.Status != HttpStatusCode.OK)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Response.TrySkipIisCustomErrors = true;
                return Content("Erro ao listar clientes");
            }

            return View("DetalhaProduto", response.Content);
        }
    }
}