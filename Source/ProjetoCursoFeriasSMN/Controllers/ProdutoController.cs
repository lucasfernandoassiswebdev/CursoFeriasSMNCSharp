using ProjetoCursoFeriasSMN.Web.Application.Applications;
using ProjetoCursoFeriasSMN.Web.Application.Model;
using System.Net;
using System.Web.Mvc;

namespace ProjetoCursoFeriasSMN.Controllers
{
    public class ProdutoController : BaseController
    {
        private readonly ProdutoApplication _appProduto = new ProdutoApplication();

        public ActionResult Editar(int codigoProduto)
        {
            var response = _appProduto.GetProduto(codigoProduto);
            if (response.Status != HttpStatusCode.OK)
                return Error(response.ContentAsString);
            if (response.Content == null)
                return Error("Produto não encontrado");

            return View("EditaProduto", response.Content);
        }

        public ActionResult Salvar(Produto produto)
        {
            var response = produto.CodigoProduto != 0 ? _appProduto.Put(produto) : _appProduto.Post(produto);
            if (response.Status != HttpStatusCode.OK)
                return Error(response.ContentAsString);

            return Content(response.Content);
        }

        public ActionResult Deletar(int codigoProduto)
        {
            var response = _appProduto.GetProduto(codigoProduto);
            if (response.Status != HttpStatusCode.OK)
                return Error(response.ContentAsString);
            if (response.Content == null)
                return Error("Produto não encontrado");

            return View("DeletaProduto", response.Content);
        }

        public ActionResult DeletarConfirmado(int codigoProduto)
        {
            var response = _appProduto.Delete(codigoProduto);
            if (response.Status != HttpStatusCode.OK)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Response.TrySkipIisCustomErrors = true;
                return Content(response.ContentAsString);
            }

            return Content(response.Content);
        }

        public ActionResult Listar()
        {
            var response = _appProduto.Get();
            if (response.Status != HttpStatusCode.OK)
                return Error(response.ContentAsString);

            return View("GridProdutos", response.Content);
        }

        public ActionResult DetalharProduto(int codigoProduto)
        {
            var response = _appProduto.GetProduto(codigoProduto);
            if (response.Status != HttpStatusCode.OK)
                return Error(response.ContentAsString);
            if (response.Content == null)
                return Error("Produto não encontrado");

            return View("DetalhaProduto", response.Content);
        }
    }
}