using Microsoft.AspNetCore.Components;
using NikeFrontend.Data;
using NikeFrontend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NikeFrontend.Pages
{
    public partial class NikeShop
    {
        [Inject]
        public IHttpClientFactory _clientFactory { get; set; }

        [Inject]
        public ProductService _productService { get; set; }


        public ListProductModelRoot listProductResult { get; set; }

        public  List<ProductModel>  listProductModel { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await getListProduct();
        }

        public async Task getListProduct()
        {
            listProductResult = await _productService.getListProduct();
            listProductModel = listProductResult.data;


        }
    }
}
