using ProjetoCursoFeriasSMN.Models;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace ProjetoCursoFeriasSMN.Web.Application.Applications
{
    public class ClienteApplication
    {
        private readonly string _enderecoApi = $"{ApiConfig.EnderecoApi}/Cliente";

        public Response<Cliente> VerificaLogin(Cliente cliente)
        {
            using (var client = new HttpClient())
            {
                var response = client.PostAsync($"{_enderecoApi}/login",cliente, new JsonMediaTypeFormatter()).Result;
                return new Response<Cliente>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }
    }
}
