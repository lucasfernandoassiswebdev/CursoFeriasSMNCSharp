using ProjetoCursoFeriasSMN.Models;
using System.Collections.Generic;
using System.Net.Http;

namespace ProjetoCursoFeriasSMN.Web.Application.Applications
{
    public class VendaApplication
    {
        private readonly string _enderecoApi = $"{ApiConfig.EnderecoApi}/venda";

        public Response<IEnumerable<Venda>> Get(int codigoCliente)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{_enderecoApi}/lista/{codigoCliente}").Result;
                return new Response<IEnumerable<Venda>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }
    }
}
