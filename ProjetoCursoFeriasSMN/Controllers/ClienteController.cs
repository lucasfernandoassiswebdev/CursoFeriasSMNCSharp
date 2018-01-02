using ProjetoCursoFeriasSMN.Models;
using ProjetoCursoFeriasSMN.Web.Application.Applications;
using System.Net;
using System.Web.Mvc;

namespace ProjetoCursoFeriasSMN.Controllers
{
    public class ClienteController : Controller
    {
        //Este método apenas carrega a tela de edição
        public ActionResult Editar(int codigoCliente)
        {
            //Trazendo as informações de um cliente especifico
            var appCliente = new ClienteApplication();

            var response = appCliente.GetCliente(codigoCliente);
            if (response.Status != HttpStatusCode.OK || response.Content == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Response.TrySkipIisCustomErrors = true;
                return Content("Não foi possível encontrar o usuário");
            }

            return View("EditaCliente", response.Content);
        }

        public ActionResult Salvar(Cliente cliente)
        {
            var appCliente = new ClienteApplication();

            //Se o objeto cliente que chegou no parâmetro tiver a propriedade CodigoCliente, significa que o cliente deve ser editado,
            //do contrário é um novo cadastro
            var response = cliente.CodigoCliente != 0 ? appCliente.Put(cliente) : appCliente.Post(cliente);
            if (response.Status != HttpStatusCode.OK)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Response.TrySkipIisCustomErrors = true;
                return Content(response.Content);
            }

            return Content(response.Content);
        }

        //Este método apenas carrega a tela que deleta o cliente
        public ActionResult Deletar(int codigoCliente)
        {
            var appCliente = new ClienteApplication();

            var response = appCliente.GetCliente(codigoCliente);
            if (response.Status != HttpStatusCode.OK || response.Content == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Response.TrySkipIisCustomErrors = true;
                return Content("Não foi possível encontrar o usuário");
            }

            return View("DeletaCliente", response.Content);
        }

        //Este método apaga de fato o cliente
        public ActionResult DeletarConfirma(int codigoCliente)
        {
            var appCliente = new ClienteApplication();

            var response = appCliente.Delete(codigoCliente);
            if (response.Status != HttpStatusCode.OK)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Response.TrySkipIisCustomErrors = true;
                return Content(response.Content);
            }

            return Content(response.Content);
        }

        public ActionResult Listar()
        {
            var appCliente = new ClienteApplication();

            var response = appCliente.Get();
            if (response.Status != HttpStatusCode.OK)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Response.TrySkipIisCustomErrors = true;
                return Content("Erro ao listar clientes");
            }

            return View("GridClientes", response.Content);
        }
    }
}