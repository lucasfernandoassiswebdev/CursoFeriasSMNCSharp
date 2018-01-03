using ProjetoCursoFeriasSMN.Web.Application.Applications;
using System.Net;
using System.Web.Mvc;

namespace ProjetoCursoFeriasSMN.Controllers
{
    public class VendaController : BaseController
    {
        private readonly VendaApplication _appVenda = new VendaApplication();

        public ActionResult Listar(int codigoCliente)
        {
            var response = _appVenda.Get(codigoCliente);
            if (response.Status != HttpStatusCode.OK)
                return Error(response.ContentAsString);

            return View("GridVendas",response.Content);
        }
    }
}