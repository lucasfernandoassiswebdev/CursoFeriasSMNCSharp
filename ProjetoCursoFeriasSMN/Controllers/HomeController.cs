using ProjetoCursoFeriasSMN.Models;
using ProjetoCursoFeriasSMN.Web.Application.Applications;
using System.Net;
using System.Web.Mvc;

namespace ProjetoCursoFeriasSMN.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(Cliente cliente)
        {
            var clienteApplication = new ClienteApplication();
            var response = clienteApplication.VerificaLogin(cliente);

            if (response.Status != HttpStatusCode.OK)
            {   /* Mudando o status da resposta fazemos com que o método Error da requisição
                   ajax seja executado */
                Response.Status = HttpStatusCode.BadRequest.ToString();
                Response.TrySkipIisCustomErrors = true;
                return Content(response.ContentAsString);
            }

            return View("Home");
        }
    }
}