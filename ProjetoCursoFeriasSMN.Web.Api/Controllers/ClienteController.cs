using ProjetoCursoFeriasSMN.Domain.Entidades;
using ProjetoCursoFeriasSMN.Repository.Repositories;
using System.Net;
using System.Web.Http;

namespace ProjetoCursoFeriasSMN.Web.Api.Controllers
{
    [RoutePrefix("api/cliente")]
    public class ClienteController : ApiController
    {
        [HttpPost, Route("cadastra")]
        public IHttpActionResult Post(Cliente cliente)
        {
            var clienteRepository = new ClienteRepository();
            var response = clienteRepository.CadastraCliente(cliente);

            if (string.IsNullOrEmpty(response))
                return Ok(response);

            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpPut, Route("edita")]
        public IHttpActionResult Put(Cliente cliente)
        {
            var clienteRepository = new ClienteRepository();
            var response = clienteRepository.EditaCliente(cliente);

            if (string.IsNullOrEmpty(response))
                return Ok(response);

            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpDelete, Route("deleta")]
        public IHttpActionResult Delete(int codigoCliente)
        {
            var clienteRepository = new ClienteRepository();
            var response = clienteRepository.DeletaCliente(codigoCliente);

            if (string.IsNullOrEmpty(response))
                return Ok(response);

            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpGet, Route("lista")]
        public IHttpActionResult Get()
        {
            var clienteRepository = new ClienteRepository();
            return Ok(clienteRepository.ListaClientes());
        }

        [HttpGet, Route("selecionaCliente")]
        public IHttpActionResult GetCliente(int codigoCliente)
        {
            var clienteRepository = new ClienteRepository();
            var response = clienteRepository.SelecionaCliente(codigoCliente);

            if(response != null)
                return Ok(response);

            return BadRequest("Nenhum registro encontrado");
        }
    }
}