using Microsoft.AspNetCore.Components;
using NikeFrontend.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace NikeFrontend.Services
{
    public class ProductCategoryService
    {
        private readonly IHttpClientFactory _clientFactory;

        public ProductCategoryService(IHttpClientFactory ClientFactory )
        {
            _clientFactory = ClientFactory;
        }

        public async Task<ProductCategoryModelRootobject> getListProductCategory()
        {
            var client = _clientFactory.CreateClient("KSC");
            var result = await client.GetFromJsonAsync<ProductCategoryModelRootobject>("ProductCategories");
            return await Task.FromResult(result);
        }

        public async Task<SingleProductCategoryModelRootobject> getProductCategory(int id)
        {
            var client = _clientFactory.CreateClient("KSC");
            var result = await client.GetFromJsonAsync<SingleProductCategoryModelRootobject>($"ProductCategories/{id}");
            return await Task.FromResult(result);
        }
    }
}
