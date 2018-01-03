using ProjetoCursoFeriasSMN.Domain.Entidades;
using ProjetoCursoFeriasSMN.Repository.Repositories;
using System.Net;
using System.Web.Http;

namespace ProjetoCursoFeriasSMN.Web.Api.Controllers
{
    [RoutePrefix("api/cliente")]
    public class ClienteController : ApiController
    {
        private readonly ClienteRepository _clienteRepository = new ClienteRepository();

        [HttpPost, Route("cadastra")]
        public IHttpActionResult Post(Cliente cliente)
        {
            var response = _clienteRepository.CadastraCliente(cliente);

            if (string.IsNullOrEmpty(response))
                return Ok(response);

            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpPut, Route("edita")]
        public IHttpActionResult Put(Cliente cliente)
        {
            var response = _clienteRepository.EditaCliente(cliente);

            if (string.IsNullOrEmpty(response))
                return Ok(response);

            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpDelete, Route("deleta")]
        public IHttpActionResult Delete(int codigoCliente)
        {
            var response = _clienteRepository.DeletaCliente(codigoCliente);

            if (string.IsNullOrEmpty(response))
                return Ok(response);

            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpGet, Route("lista")]
        public IHttpActionResult Get()
        {
            return Ok(_clienteRepository.ListaClientes());
        }

        [HttpGet, Route("selecionaCliente/{codigoCliente}")]
        public IHttpActionResult GetCliente(int codigoCliente)
        {
            return Ok(_clienteRepository.SelecionaCliente(codigoCliente));
        }
    }
}