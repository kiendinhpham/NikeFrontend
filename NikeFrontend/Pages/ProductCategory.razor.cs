using Microsoft.AspNetCore.Components;
using NikeFrontend.Data;
using NikeFrontend.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NikeFrontend.Pages
{
    public partial class ProductCategory
    {
        [Inject]
        public IHttpClientFactory _clientFactory { get; set; }

        [Inject]
        public ProductCategoryService _productCategoryService { get; set; }

        public ProductCategoryModelRootobject listProductCategoryResult { get; set; }
        public SingleProductCategoryModelRootobject productCategoryResult { get; set; }

        public List<ProductCategoryModel> listProductCategory { get; set; }
        public ProductCategoryModel productCategory { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            Console.WriteLine("Product - OnAfterRenderAsync - firstRender = " + firstRender);

            if (firstRender)
            {
                await getListProductCategory();
                StateHasChanged();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        public async Task getListProductCategory()
        {
            listProductCategoryResult = await _productCategoryService.getListProductCategory();
            listProductCategory = listProductCategoryResult.data;
        }

        public async Task getProductCategory(int id)
        {
            productCategoryResult = await _productCategoryService.getProductCategory(id);
            productCategory = productCategoryResult.data;
        }
    }
}