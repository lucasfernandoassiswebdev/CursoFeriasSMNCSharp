using ProjetoCursoFeriasSMN.Web.Application.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace ProjetoCursoFeriasSMN.Web.Application.Applications
{
    public class VendaApplication
    {
        private readonly string _enderecoApi = $"{ApiConfig.EnderecoApi}/venda";

        public Response<string> Post(Venda venda)
        {
            using (var client = new HttpClient())
            {
                var response = client.PostAsync($"{_enderecoApi}/cadastra",venda,new JsonMediaTypeFormatter()).Result;
                return new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<IEnumerable<Venda>> Get(int codigoCliente)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{_enderecoApi}/lista/{codigoCliente}").Result;
                return new Response<IEnumerable<Venda>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<Venda> GetVenda(int codigoVenda)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{_enderecoApi}/selecionaVenda/{codigoVenda}").Result;
                return new Response<Venda>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<string> Delete(int codigoVenda)
        {
            using (var client = new HttpClient())
            {
                var response = client.DeleteAsync($"{_enderecoApi}/deleta/{codigoVenda}").Result;
                return new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }
    }
}
