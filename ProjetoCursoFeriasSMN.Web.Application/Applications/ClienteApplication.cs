using ProjetoCursoFeriasSMN.Models;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace ProjetoCursoFeriasSMN.Web.Application.Applications
{
    public class ClienteApplication
    {
        private readonly string _enderecoApi = $"{ApiConfig.EnderecoApi}/Cliente";

        public Response<string> Post(Cliente cliente)
        {
            using (var client = new HttpClient())
            {
                var response = client.PostAsync($"{_enderecoApi}/cadastra",cliente, new JsonMediaTypeFormatter()).Result;
                return new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<string> Put(Cliente cliente)
        {
            using (var client = new HttpClient())
            {
                var response = client.PutAsync($"{_enderecoApi}/edita", cliente, new JsonMediaTypeFormatter()).Result;
                return new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<string> Delete(int idCliente)
        {
            using (var client = new HttpClient())
            {
                var response = client.PutAsync($"{_enderecoApi}/deleta", idCliente, new JsonMediaTypeFormatter()).Result;
                return new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }
    }
}
