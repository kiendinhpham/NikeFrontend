using NikeFrontend.Data;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace NikeFrontend.Services
{
    public class ProductService
    {
        private readonly IHttpClientFactory _clientFactory;

        public ProductService(IHttpClientFactory ClientFactory)
        {
            _clientFactory = ClientFactory;
        }

        public async Task<ListProductModelRoot> getListProduct()
        {
            var client = _clientFactory.CreateClient("KSC");
            var result = await client.GetFromJsonAsync<ListProductModelRoot>("Products");
            return await Task.FromResult(result);
        }

        public async Task<SingleProductModelRoot> getProduct(int id)
        {
            var client = _clientFactory.CreateClient("KSC");
            var result = await client.GetFromJsonAsync<SingleProductModelRoot>($"Products/{id}");
            return await Task.FromResult(result);
        }
    }
}