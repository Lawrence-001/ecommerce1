//using e_commerce.Models;
//using Microsoft.EntityFrameworkCore;

//namespace e_commerce.Data
//{
//    public static class DbInitializer
//    {
//        public static void Seed(this ModelBuilder modelbuilder)
//        {
//            modelbuilder.Entity<Product>().HasData(
//                new Product
//                {
//                    ProductId = 1,
//                    Name = "Plastic chair",
//                    Description = "plastic chair",
//                    ProductCategory = ProductCategory.Furniture,
//                    Cost = 500
//                },
//                new Product
//                {
//                    ProductId = 2,
//                    Name = "Office chair",
//                    Description = "Office chair",
//                    ProductCategory = ProductCategory.Furniture,
//                    Cost = 5000
//                },
//                new Product
//                {

//                    ProductId = 3,
//                    Name = "Hp Laptop",
//                    Description = "Laptop",
//                    ProductCategory = ProductCategory.Electronic,
//                    Cost = 50000
//                },
//                new Product
//                {
//                    ProductId = 4,
//                    Name = "Pressure cooker",
//                    Description = "Utensils",
//                    ProductCategory = ProductCategory.Kitchen,
//                    Cost = 5000
//                });
//        }
//    }
//}
