using System.Collections.Generic;

namespace NikeFrontend.Data
{
    public class ListProductModelRoot
    {
        public List<ProductModel> data { get; set; }
        public bool succeeded { get; set; }
        public Error error { get; set; }
    }

    public class SingleProductModelRoot
    {
        public ProductModel data { get; set; }
        public bool succeeded { get; set; }
        public object error { get; set; }
    }

    public class ProductModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public object image { get; set; }
        public int price { get; set; }
        public int productCategoryId { get; set; }
        public string productCategoryName { get; set; }
    }

    public class Error
    {
        public string message { get; set; }
        public int code { get; set; }
    }

}