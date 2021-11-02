using PromocionesFravega.Core.ApiClient.Dtos;
using PromocionesFravega.Core.Exceptions;
using PromocionesFravega.Core.Interfaces;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PromocionesFravega.Infrastructure.Api
{
    public class ApiClient : IApiClient
    {
        private readonly IHttpClientFactory _clientFact;

        public ApiClient(IHttpClientFactory IHttpClientFactory)
        {
            _clientFact = IHttpClientFactory;
        }

       public async Task<TResponse> Get<TResponse>(string uri)
        {
            var cliente = _clientFact.CreateClient("Api.ML");
            var response = await cliente.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TResponse>(content);
                return result;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new BusinessException("No se encuentra el recurso solicitado en Api Mercado Libre");
            }
            else
                throw new BusinessException("Error en solicitud Api Mercado Libre");            
        }

        public async Task<ItemResponse> Items(string Producto)
        {
            return await Get<ItemResponse>(string.Format("items/{0}",Producto));
        }
        
    }
}
