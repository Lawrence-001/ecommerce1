using System.Collections.Generic;
using System.Linq;
using e_commerce.Models;

namespace e_commerce.Data
{
    public class ProductRepo : IProductRepo
    {
        private readonly AppDbContext _context;

        public ProductRepo(AppDbContext context)
        {
            _context = context;
        }
        public Product AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public Product DeleteProduct(int id)
        {
            var prod = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (prod != null)
            {
                _context.Products.Remove(prod);
                _context.SaveChanges();
            }
            return prod;
        }

        public Product GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.ProductId == id);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public Product UpdateProduct(Product product)
        {
            var prod = _context.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
            if (prod != null)
            {
                prod.Name = product.Name;
                prod.Description = product.Description;
                prod.ProductCategory = product.ProductCategory;
                prod.ImgUrl = product.ImgUrl;
                prod.Cost = product.Cost;

                _context.Products.Update(prod);
                _context.SaveChanges();
            }
            return product;
        }
    }
}
