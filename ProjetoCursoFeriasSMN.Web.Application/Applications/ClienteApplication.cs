using ProjetoCursoFeriasSMN.Models;
using System.Net.Http;

namespace ProjetoCursoFeriasSMN.Web.Application.Applications
{
    public class ClienteApplication
    {
        private readonly string _enderecoApi = $"{ApiConfig.EnderecoApi}/Cliente";

        public Response<Cliente> VerificaLogin(Cliente cliente)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(_enderecoApi).Result;
                return new Response<Cliente>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }
    }
}
