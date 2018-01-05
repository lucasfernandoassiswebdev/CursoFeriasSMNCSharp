using ProjetoCursoFeriasSMN.Web.Application.Applications;
using ProjetoCursoFeriasSMN.Web.Application.Model;
using System.Net;
using System.Web.Mvc;

namespace ProjetoCursoFeriasSMN.Controllers
{
    public class ClienteController : BaseController
    {
        private readonly ClienteApplication _appCliente = new ClienteApplication();

        //Este método apenas carrega a tela de edição
        public ActionResult Editar(int codigoCliente)
        {
            //Trazendo as informações de um cliente especifico
            var response = _appCliente.GetCliente(codigoCliente);
            //Verificando se a API conseguiu responder com sucesso
            if (response.Status != HttpStatusCode.OK)
                return Error(response.ContentAsString);
            //Verificando se o cliente não veio nulo
            if (response.Content == null)
                return Error("Erro ao listar dados do cliente");

            return View("EditaCliente", response.Content);
        }

        public ActionResult Salvar(Cliente cliente)
        {
            if (cliente.Complemento == null)
                cliente.Complemento = string.Empty;

             //Se o objeto cliente que chegou no parâmetro tiver a propriedade CodigoCliente, significa que o cliente deve ser editado,
            //do contrário é um novo cadastro
            var response = cliente.CodigoCliente != 0 ? _appCliente.Put(cliente) : _appCliente.Post(cliente);
            if (response.Status != HttpStatusCode.OK)
                return Error(response.ContentAsString);

            return Content("Salvo com sucesso");
        }

        //Este método apenas carrega a tela que deleta o cliente
        public ActionResult Deletar(int codigoCliente)
        {
            var response = _appCliente.GetCliente(codigoCliente);
            if (response.Status != HttpStatusCode.OK)
                return Error(response.ContentAsString);
            if (response.Content == null)
                return Error("Erro ao listar dados do cliente");

            return View("DeletaCliente", response.Content);
        }

        //Este método apaga de fato o cliente
        public ActionResult DeletarConfirma(int codigoCliente, int codigoEndereco)
        {
            var response = _appCliente.Delete(codigoCliente,codigoEndereco);
            if (response.Status != HttpStatusCode.OK)
                return Error(response.ContentAsString);

            return Content(response.Content);
        }

        public ActionResult Listar()
        {
            var response = _appCliente.Get();
            if (response.Status != HttpStatusCode.OK)
                return Error(response.ContentAsString);

            //Aqui não precisamos verificar se a lista de clientes veio nula, pois a própria view faz essa verificação
            return View("GridClientes", response.Content);
        }

        public ActionResult DetalharCliente(int codigoCliente)
        {
            var response = _appCliente.GetCliente(codigoCliente);
            if (response.Status != HttpStatusCode.OK)
                return Error(response.ContentAsString);
            if (response.Content == null)
                return Error("Erro ao listar dados do cliente");
            //Trazendo o endereço do cliente


            return View("DetalhaCliente", response.Content);
        }
    }
}