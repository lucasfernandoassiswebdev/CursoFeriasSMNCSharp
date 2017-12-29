using ProjetoCursoFeriasSMN.Domain.Entidades;
using ProjetoCursoFeriasSMN.Repository.Repositories;
using System.Net;
using System.Web.Http;

namespace ProjetoCursoFeriasSMN.Web.Api.Controllers
{
    [RoutePrefix("api/cliente")]
    public class ClienteController : ApiController
    {
        [HttpPost, Route("login")]
        public IHttpActionResult PostLogin(Cliente cliente)
        {
            var clienteRepository = new ClienteRepository();
            var response = clienteRepository.VerificaLogin(cliente);

            if (response != null)
                return Ok(response);

            return Content(HttpStatusCode.BadRequest, "Login ou senha incorretos");
        }
    }
}