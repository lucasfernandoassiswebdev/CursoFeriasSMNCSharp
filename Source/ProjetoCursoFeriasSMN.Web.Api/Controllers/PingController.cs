using System;
using System.Web.Http;

namespace ProjetoCursoFeriasSMN.Web.Api.Controllers
{
    public class PingController : ApiController
    {
        [HttpGet, Route("api")]
        public IHttpActionResult Ping()
        {
            return Ok("Status: OK - " + DateTime.Now);
        }
    }
}