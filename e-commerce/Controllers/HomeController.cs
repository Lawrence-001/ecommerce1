using e_commerce.Data;
using e_commerce.Models;
using e_commerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepo _productRepo;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostBuilder;
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, IProductRepo productRepo, AppDbContext context, IWebHostEnvironment webHostEnvironment,
            SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _productRepo = productRepo;
            _context = context;
            _webHostBuilder = webHostEnvironment;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var model = _productRepo.GetProducts();
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index(string searchString)
        {
            var productQuery = from m in _context.Products select m;        //creating a linq query to select the products
            if (!String.IsNullOrEmpty(searchString))
            {
                productQuery = productQuery.Where(s => s.Name.Contains(searchString) || s.Description.Contains(searchString));
            }
            return View(productQuery);
            //var model = _productRepo.GetProducts();
            //return View(model);
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(ProductCreateVM model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(_webHostBuilder.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    //using (var stream = new FileStream(filePath, FileMode.Create))
                    //{
                    //    model.Photo.CopyTo(stream);
                    //}
                }
                Product product = new Product()
                {
                    Name = model.Name,
                    Description = model.Description,
                    ProductCategory = model.ProductCategory,
                    ImgUrl = uniqueFileName,
                    Cost = model.Cost
                };
                _productRepo.AddProduct(product);
                return RedirectToAction("details");
            }

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var prod = _productRepo.GetProductById(id);
            if (prod != null)
            {
                return View(prod);
            }
            return View("NotFound");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var product = _productRepo.GetProductById(id);
            if (product != null)
            {
                ProductEditVM prod = new ProductEditVM()
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Description = product.Description,
                    ProductCategory = (ProductCategory)product.ProductCategory,
                    ExistingPhoto = product.ImgUrl,
                    Cost = (double)product.Cost
                };
                return View(prod);
            }
            return View("NotFound");
        }

        [HttpPost]
       [Authorize(Roles = "Admin")]
        public IActionResult Edit(ProductEditVM model)
        {
            if (ModelState.IsValid)
            {
                var prod = _productRepo.GetProductById(model.ProductId);
                prod.Name = model.Name;
                prod.Description = model.Description;
                prod.ProductCategory = model.ProductCategory;
                prod.Cost = model.Cost;
                string uniqueFileName = ProcessUploadedFile(model);
                if (model.Photo != null)
                {
                    if (model.ExistingPhoto != null)
                    {
                        string filePath = Path.Combine(_webHostBuilder.WebRootPath, "images", model.ExistingPhoto);
                        System.IO.File.Delete(filePath);
                    }
                    prod.ImgUrl = ProcessUploadedFile(model);
                }

                Product updatedProduct = _productRepo.UpdateProduct(prod);

                return RedirectToAction("index");
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var prod = _productRepo.GetProductById(id);
            if (prod != null)
            {
                _productRepo.DeleteProduct(id);
                return RedirectToAction("index");
            }
            return View("NotFound");
        }
        public IActionResult Privacy()
        {
            return View();
        }
        private string ProcessUploadedFile(ProductCreateVM model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(_webHostBuilder.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));

                //using (var stream = new FileStream(filePath, FileMode.Create))
                //{
                //    model.Photo.CopyTo(stream);
                //}
            }

            return uniqueFileName;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
    }
}
