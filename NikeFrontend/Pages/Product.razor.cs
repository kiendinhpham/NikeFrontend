using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using NikeFrontend.Data;
using NikeFrontend.Services;
using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.Threading;

namespace NikeFrontend.Pages
{
    public partial class Product
    {
        [Inject]
        public IHttpClientFactory _clientFactory { get; set; }
        [Inject]
        public IToastService _toastService { get; set; }
        [Inject]
        public IWebHostEnvironment env { get; set; }
        [Inject]
        public ProductService _productService { get; set; }
        [Inject]
        public ProductCategoryService _productCategoryService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

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
        IBrowserFile file;
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

        private void LoadFiles(InputFileChangeEventArgs e)
        {
            file = e.File;
            Console.WriteLine("selected file: " + file.Name);

        }

        private async Task uploadFile()
        {
            Stream stream = file.OpenReadStream(10000000);
            var path = $"{env.WebRootPath}\\img\\{file.Name}";
            FileStream fs = File.Create(path);
            await stream.CopyToAsync(fs);
            stream.Close();
            fs.Close();
            Console.WriteLine(file.Name + "uploaded");

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

        public void reloadPage()
        {
            NavigationManager.NavigateTo("/product", true);
        }


        public async Task updateProduct()
        {
            if(file != null)
            {
                editProduct.image = "img/" + file.Name;
                await uploadFile();
            }
            HttpResponseMessage response = await _productService.editProduct(editProduct);
            Console.WriteLine(response);
            editProduct = new ProductModel();
            if (response.IsSuccessStatusCode)
            {
                await getListProduct();
                _toastService.ShowSuccess("Product updated");
                //need to reload the whole app or the hosting will fucked up the new file, only god know why.
            }
            else
            {
                _toastService.ShowError("There was an error");
            }
        }

        public async Task addProduct()
        {
            if (file != null)
            {
                newProduct.image = "img/" + file.Name;
                await uploadFile();
            }
            HttpResponseMessage response = await _productService.addProduct(newProduct);
            Console.WriteLine(response);
            if (response.IsSuccessStatusCode)
            {
                newProduct = new ProductModel();
                await getListProduct();
                _toastService.ShowSuccess("New product added");
            }
            else
            {
                _toastService.ShowError("There was an error");
            }
        }

        public async Task deleteProduct(int id)
        {
            HttpResponseMessage response = await _productService.deleteProduct(id);
            Console.WriteLine(response);
            if (response.IsSuccessStatusCode)
            {
                await getListProduct();
                _toastService.ShowSuccess("product deleted");
            }
            else
            {
                _toastService.ShowError("There was an error");
            }
        }
    }
}