using e_commerce.Data;
using e_commerce.Models;
using e_commerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace e_commerce.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepo _repo;
        private readonly Cart _cart;

        public CartController(IProductRepo repo, Cart cart)
        {
            _repo = repo;
            _cart = cart;
        }
        public IActionResult Index()
        {
            var results = _cart.GetCartItems();
            results = _cart.CartItems;

            CartVM cartVM = new CartVM()
            {
                Cart = _cart,
                CartTotal = (decimal)_cart.CartTotal()
            };
            return View(cartVM);
        }
        public RedirectToActionResult AddToCart(int id)
        {
            var product = _repo.GetProductById(id);
            if (product!=null)
            {
                _cart.AddToCart(product);
            }
            return RedirectToAction("index");
        }
        public RedirectToActionResult RemoveFromCart(int id)
        {
            var product = _repo.GetProductById(id);
            if (product!=null)
            {
                _cart.RemoveFromCart(product);
            }
            return RedirectToAction("index");
        }
    }
}
