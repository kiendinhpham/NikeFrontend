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
        [Inject]
        public ProductCategoryService _productCategoryService { get; set; }

        public ListProductModelRoot listProductResult { get; set; }
        public SingleProductModelRoot productResult { get; set; }
        public ProductCategoryModelRootobject listProductCategoryResult { get; set; }
        public List<ProductCategoryModel> listProductCategory { get; set; }

        public List<ProductModel> listProduct { get; set; }
        public ProductModel product { get; set; }

        private ProductModel newProduct = new ProductModel();
        private ProductModel editProduct = new ProductModel();

        public int idForDelete { get; set; }
        public string nameForDelete { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            Console.WriteLine("Product - OnAfterRenderAsync - firstRender = " + firstRender);

            if (firstRender)
            {
                await getListProductCategory();
                await getListProduct();
                StateHasChanged();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        public void passDataForDeleteModal(int id, string name)
        {
            idForDelete = id;
            nameForDelete = name;
        }

        public async Task getListProductCategory()
        {
            listProductCategoryResult = await _productCategoryService.getListProductCategory();
            listProductCategory = listProductCategoryResult.data;
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

        public async Task getProductForModalEdit(int id)
        {
            productResult = await _productService.getProduct(id);
            editProduct = productResult.data;
        }

        public async Task updateProduct()
        {
            HttpResponseMessage response = await _productService.editProduct(editProduct);
            Console.WriteLine(response);
            editProduct = new ProductModel();
            await getListProduct();
        }

        public async Task addProduct()
        {
            HttpResponseMessage response = await _productService.addProduct(newProduct);
            Console.WriteLine(response);
            newProduct = new ProductModel();
            await getListProduct();
        }

        public async Task deleteProduct(int id)
        {
            HttpResponseMessage response = await _productService.deleteProduct(id);
            Console.WriteLine(response);
            await getListProduct();
        }

    }
}