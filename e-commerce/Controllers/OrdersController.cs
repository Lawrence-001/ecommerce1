using e_commerce.Data;
using e_commerce.Models;
using e_commerce.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace e_commerce.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderRepo _orderRepo;
        private readonly Cart _shoppingCart;
        private readonly IProductRepo _productRepo;
        private readonly SignInManager<AppUser> _signInManager;

        public OrdersController(IOrderRepo orderRepo, Cart shoppingCart, IProductRepo productRepo, SignInManager<AppUser> signInManager)
        {
            _orderRepo = orderRepo;
            _shoppingCart = shoppingCart;
            _productRepo = productRepo;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var orders = await _orderRepo.GetOrdersByUserIdAndRoleAsync(userId, userRole);
            return View(orders);
        }

        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetCartItems();
            _shoppingCart.CartItems = items;

            var response = new CartVM()
            {
                Cart = _shoppingCart,
                CartTotal = (decimal)_shoppingCart.CartTotal()
            };
            HttpContext.Session.SetString("amount", response.CartTotal.ToString());
            return View(response);
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var item =  _productRepo.GetProductById(id);

            if (item != null)
            {
                _shoppingCart.AddToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var item =  _productRepo.GetProductById(id);

            if (item != null)
            {
                _shoppingCart.RemoveFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            await _orderRepo.StoreOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }


    }
}
