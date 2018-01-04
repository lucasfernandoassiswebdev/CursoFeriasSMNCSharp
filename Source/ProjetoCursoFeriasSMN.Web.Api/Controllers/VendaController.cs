using ProjetoCursoFeriasSMN.Domain.Entidades;
using ProjetoCursoFeriasSMN.Repository.Repositories;
using System.Net;
using System.Web.Http;

namespace ProjetoCursoFeriasSMN.Web.Api.Controllers
{
    [RoutePrefix("api/venda")]
    public class VendaController : ApiController
    {
        private readonly VendaRepository _vendaRepository = new VendaRepository();

        [HttpPost, Route("cadastra")]
        public IHttpActionResult Post(Venda venda)
        {
            var response = _vendaRepository.CadastraVenda(venda);

            if (string.IsNullOrEmpty(response))
                return Ok(response);

            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpDelete, Route("deleta/{codigoVenda}")]
        public IHttpActionResult Delete(int codigoVenda)
        {
            var response = _vendaRepository.DeletaVenda(codigoVenda);

            if (string.IsNullOrEmpty(response))
                return Ok(response);

            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpGet, Route("lista/{codigoCliente}")]
        public IHttpActionResult Get(int codigoCliente)
        {
            return Ok(_vendaRepository.ListaVendas(codigoCliente));
        }

        [HttpGet, Route("selecionaVenda/{codigoVenda}")]
        public IHttpActionResult GetVenda(int codigoVenda)
        {
            return Ok(_vendaRepository.SelecionaVenda(codigoVenda));
        }
    }
}