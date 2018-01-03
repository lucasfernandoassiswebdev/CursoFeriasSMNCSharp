using ProjetoCursoFeriasSMN.Domain.Entidades;
using ProjetoCursoFeriasSMN.Repository.Repositories;
using System.Net;
using System.Web.Http;

namespace ProjetoCursoFeriasSMN.Web.Api.Controllers
{
    [RoutePrefix("api/produto")]
    public class ProdutoController : ApiController
    {
        private readonly ProdutoRepository _produtoRepository = new ProdutoRepository();

        [HttpPost,Route("cadastra")]
        public IHttpActionResult Post(Produto produto)
        {
            var response = _produtoRepository.CadastraProduto(produto);

            if (string.IsNullOrEmpty(response))
                return Ok(response);

            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpPut, Route("edita")]
        public IHttpActionResult Put(Produto produto)
        {
           var response = _produtoRepository.EditaProduto(produto);

            if (string.IsNullOrEmpty(response))
                return Ok(response);

            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpDelete, Route("deleta")]
        public IHttpActionResult Delete(int idProduto)
        {
            var response = _produtoRepository.DeletaProduto(idProduto);

            if (string.IsNullOrEmpty(response))
                return Ok(response);

            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpGet, Route("lista")]
        public IHttpActionResult Get()
        {
            return Ok(_produtoRepository.ListaProdutos());
        }

        [HttpGet, Route("listaProduto/{codigoProduto}")]
        public IHttpActionResult GetProduto(int codigoProduto)
        {
            return Ok(_produtoRepository.ListaProdutos());
        }
    }
}