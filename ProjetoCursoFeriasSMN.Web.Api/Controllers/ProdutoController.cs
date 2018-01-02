using ProjetoCursoFeriasSMN.Domain.Entidades;
using ProjetoCursoFeriasSMN.Repository.Repositories;
using System.Net;
using System.Web.Http;

namespace ProjetoCursoFeriasSMN.Web.Api.Controllers
{
    [RoutePrefix("api/produto")]
    public class ProdutoController : ApiController
    {
        [HttpPost,Route("cadastra")]
        public IHttpActionResult Post(Produto produto)
        {
            var produtoRepository = new ProdutoRepository();
            var response = produtoRepository.CadastraProduto(produto);

            if (string.IsNullOrEmpty(response))
                return Ok(response);

            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpPut, Route("edita")]
        public IHttpActionResult Put(Produto produto)
        {
            var produtoRepository = new ProdutoRepository();
            var response = produtoRepository.EditaProduto(produto);

            if (string.IsNullOrEmpty(response))
                return Ok(response);

            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpDelete, Route("deleta")]
        public IHttpActionResult Delete(int idProduto)
        {
            var produtoRepository = new ProdutoRepository();
            var response = produtoRepository.DeletaProduto(idProduto);

            if (string.IsNullOrEmpty(response))
                return Ok(response);

            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpGet, Route("lista")]
        public IHttpActionResult Get()
        {
            var produtoRepository = new ProdutoRepository();
            return Ok(produtoRepository.ListaProdutos());
        }

        [HttpGet, Route("listaProduto")]
        public IHttpActionResult GetProduto(int codigoProduto)
        {
            var produtoRepository = new ProdutoRepository();
            var response = produtoRepository.ListaProdutos();

            if(response != null)
                return Ok(response);

            return BadRequest("Nenhum registro encontrado");
        }
    }
}