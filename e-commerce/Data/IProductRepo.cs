using System.Collections.Generic;
using e_commerce.Models;

namespace e_commerce.Data
{
    public interface IProductRepo
    {
        public Product GetProductById(int id);
        public Product AddProduct(Product product);
        public IEnumerable<Product> GetProducts();
        public Product UpdateProduct(Product product);
        public Product DeleteProduct(int id);
    }
}
