using e_commerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.ViewComponents
{
    public class CartSummary : ViewComponent
    {
        private readonly Cart _cart;

        public CartSummary(Cart cart)
        {
            _cart = cart;
        }

        public IViewComponentResult Invoke()
        {
            var result = _cart.GetCartItems();
            return View(result.Count);
        }
    }
}
