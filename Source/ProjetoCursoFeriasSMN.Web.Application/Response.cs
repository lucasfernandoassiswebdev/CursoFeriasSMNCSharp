using Newtonsoft.Json;
using System.Net;

namespace ProjetoCursoFeriasSMN.Web.Application
{
    //Este "T" representa uma classe genérica
    public class Response<T>
    {
        public Response(string json, HttpStatusCode statusCode)
        {
            Json = json;
            Status = statusCode;
        }

        public Response(HttpStatusCode statusCode)
        {
            Status = statusCode;
        }

        private string Json { get; }
        public HttpStatusCode Status { get; }

        // Essas linhas desserializam os objetos retornados da API(JSON -> objeto c#)
        public T Content => JsonConvert.DeserializeObject<T>(Json);
        public string ContentAsString => JsonConvert.DeserializeObject<string>(Json);
    }
}
