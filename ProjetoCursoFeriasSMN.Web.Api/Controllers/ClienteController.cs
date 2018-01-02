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
        public IHttpActionResult Put(Cliente cliente, int idCliente)
        {
            var clienteRepository = new ClienteRepository();
            var response = clienteRepository.EditaCliente(cliente, idCliente);

            if (string.IsNullOrEmpty(response))
                return Ok(response);

            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpDelete, Route("deleta")]
        public IHttpActionResult Delete(int idCliente)
        {
            var clienteRepository = new ClienteRepository();
            var response = clienteRepository.DeletaCliente(idCliente);

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
    }
}