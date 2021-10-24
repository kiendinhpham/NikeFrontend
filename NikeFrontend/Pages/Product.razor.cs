using Microsoft.AspNetCore.Components;
using NikeFrontend.Data;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace NikeFrontend.Pages
{
    public partial class Product
    {
        [Inject]
        public IHttpClientFactory _clientFactory { get; set; }

        public List<ProductModel> listProduct { get; set; }
        public ProductModel product { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await Task.Run(getListProduct);
        }

        public async Task getListProduct()
        {
            var client = _clientFactory.CreateClient("KSC");
            try
            {
                var result = await client.GetFromJsonAsync<ListProductModelRoot>("Products");
                listProduct = result.data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public async Task getProduct(int id)
        {
            Console.WriteLine("run get product by id");
            var client = _clientFactory.CreateClient("KSC");
            try
            {
                var result = await client.GetFromJsonAsync<SingleProductModelRoot>($"Products/{id}");
                product = result.data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}