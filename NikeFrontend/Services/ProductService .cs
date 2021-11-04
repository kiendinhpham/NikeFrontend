using Blazored.SessionStorage;
using NikeFrontend.Data;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace NikeFrontend.Services
{
    public class ProductService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ISessionStorageService _sessionStorageService;

        public ProductService(IHttpClientFactory ClientFactory,
            ISessionStorageService SessionStorageService)
        {
            _clientFactory = ClientFactory;
            _sessionStorageService = SessionStorageService;
        }

        public async Task<ListProductModelRoot> getListProduct(string keyword)
        {
            var client = _clientFactory.CreateClient("KSC");
            var result = await client.GetFromJsonAsync<ListProductModelRoot>($"Products/list?keyword={keyword}");
            return await Task.FromResult(result);
        }

        public async Task<SingleProductModelRoot> getProduct(int id)
        {
            var client = _clientFactory.CreateClient("KSC");
            var result = await client.GetFromJsonAsync<SingleProductModelRoot>($"Products/{id}");
            return await Task.FromResult(result);
        }

        public async Task<HttpResponseMessage> addProduct(ProductModel productModel)
        {
            var token = await _sessionStorageService.GetItemAsync<string>("token");
            var client = _clientFactory.CreateClient("KSC");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            var result = await client.PostAsJsonAsync("Products", productModel);
            return await Task.FromResult(result);
        }

        public async Task<HttpResponseMessage> deleteProduct(int id)
        {
            var token = await _sessionStorageService.GetItemAsync<string>("token");
            var client = _clientFactory.CreateClient("KSC");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            var result = await client.DeleteAsync($"Products/{id}");
            return await Task.FromResult(result);
        }

        public async Task<HttpResponseMessage> editProduct(ProductModel productModel)
        {
            var token = await _sessionStorageService.GetItemAsync<string>("token");
            var client = _clientFactory.CreateClient("KSC");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            var result = await client.PutAsJsonAsync<ProductModel>("Products", productModel);
            return await Task.FromResult(result);
        }
    }
}