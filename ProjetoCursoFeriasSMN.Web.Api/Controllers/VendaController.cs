using ProjetoCursoFeriasSMN.Domain.Entidades;
using ProjetoCursoFeriasSMN.Repository.Repositories;
using System.Net;
using System.Web.Http;

namespace ProjetoCursoFeriasSMN.Web.Api.Controllers
{
    [RoutePrefix("api/venda")]
    public class VendaController : ApiController
    {
        [HttpPost, Route("cadastra")]
        public IHttpActionResult Post(Venda venda)
        {
            var vendaRepository = new VendaRepository();
            var response = vendaRepository.CadastraVenda(venda);

            if (string.IsNullOrEmpty(response))
                return Ok(response);

            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpDelete, Route("deleta")]
        public IHttpActionResult Delete(int idVenda)
        {
            var vendaRepository = new VendaRepository();
            var response = vendaRepository.DeletaVenda(idVenda);

            if (string.IsNullOrEmpty(response))
                return Ok(response);

            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpGet, Route("lista")]
        public IHttpActionResult Get(int codigoCliente)
        {
            var vendaRepository = new VendaRepository();
            return Ok(vendaRepository.ListaVendas(codigoCliente));
        }
    }
}