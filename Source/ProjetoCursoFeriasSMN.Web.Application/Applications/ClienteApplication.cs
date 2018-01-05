using ProjetoCursoFeriasSMN.Web.Application.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace ProjetoCursoFeriasSMN.Web.Application.Applications
{
    public class ClienteApplication
    {
        private readonly string _enderecoApi = $"{ApiConfig.EnderecoApi}/cliente";

        public Response<string> Post(Cliente cliente)
        {
            using (var client = new HttpClient())
            {
                var response = client.PostAsync($"{_enderecoApi}/cadastra", cliente, new JsonMediaTypeFormatter()).Result;
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

        public Response<string> Delete(int codigoCliente, int codigoEndereco)
        {
            using (var client = new HttpClient())
            {
                var response = client.DeleteAsync($"{_enderecoApi}/deleta/{codigoCliente}/{codigoEndereco}").Result;
                return new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<IEnumerable<Cliente>> Get()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{_enderecoApi}/lista").Result;
                return new Response<IEnumerable<Cliente>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<Cliente> GetCliente(int codigoCliente)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{_enderecoApi}/selecionaCliente/{codigoCliente}").Result;
                return new Response<Cliente>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }
    }
}
