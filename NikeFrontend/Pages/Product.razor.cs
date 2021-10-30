using Microsoft.AspNetCore.Components;
using NikeFrontend.Data;
using NikeFrontend.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NikeFrontend.Pages
{
    public partial class Product
    {
        [Inject]
        public IHttpClientFactory _clientFactory { get; set; }

        [Inject]
        public ProductService _productService { get; set; }

        public ListProductModelRoot listProductResult { get; set; }
        public SingleProductModelRoot productResult { get; set; }

        public List<ProductModel> listProduct { get; set; }
        public ProductModel product { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            Console.WriteLine("Product - OnAfterRenderAsync - firstRender = " + firstRender);

            if (firstRender)
            {
                await getListProduct();
                StateHasChanged();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        public async Task getListProduct()
        {
            listProductResult = await _productService.getListProduct();
            listProduct = listProductResult.data;
        }

        public async Task getProduct(int id)
        {
            productResult = await _productService.getProduct(id);
            product = productResult.data;
        }

    }
}