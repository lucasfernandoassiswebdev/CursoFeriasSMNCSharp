using ProjetoCursoFeriasSMN.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace ProjetoCursoFeriasSMN.Web.Application.Applications
{
    public class ProdutoApplication
    {
        private readonly string _enderecoApi = $"{ApiConfig.EnderecoApi}/produto";

        public Response<string> Post(Produto produto)
        {
            using (var client = new HttpClient())
            {
                var response = client.PostAsync($"{_enderecoApi}/cadastra", produto, new JsonMediaTypeFormatter()).Result;
                return new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<string> Put(Produto produto)
        {
            using (var client = new HttpClient())
            {
                var response = client.PutAsync($"{_enderecoApi}/edita", produto, new JsonMediaTypeFormatter()).Result;
                return new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<string> Delete(int codigoProduto)
        {
            using (var client = new HttpClient())
            {
                var response = client.PutAsync($"{_enderecoApi}/deleta", codigoProduto, new JsonMediaTypeFormatter()).Result;
                return new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<IEnumerable<Produto>> Get()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{_enderecoApi}/lista").Result;
                return new Response<IEnumerable<Produto>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<Produto> GetProduto(int codigoProduto)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{_enderecoApi}/listaProduto/{codigoProduto}").Result;
                return new Response<Produto>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }
    }
}
