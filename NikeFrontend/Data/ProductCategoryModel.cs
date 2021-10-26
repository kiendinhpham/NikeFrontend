using System.Collections.Generic;

namespace NikeFrontend.Data
{
    public class ProductCategoryModelRootobject
    {
        public List<ProductCategoryModel> data { get; set; }
        public bool succeeded { get; set; }
        public object error { get; set; }
    }

    public class ProductCategoryModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string createDate { get; set; }
        public List<ProductModel> products { get; set; }
    }

    public class SingleProductCategoryModelRootobject
    {
        public ProductCategoryModel data { get; set; }
        public bool succeeded { get; set; }
        public object error { get; set; }
    }
}