using e_commerce.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Models
{
    public class Cart
    {
        private readonly AppDbContext _context;

        public string CartId { get; set; }
        public List<CartItem> CartItems { get; set; }

        public Cart(AppDbContext context)
        {
            _context = context;
        }
        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();

            string cartId = session.GetString("ShoppingCartId") ?? Guid.NewGuid().ToString();
            session.SetString("ShoppingCartId", cartId);
            return new Cart(context) { CartId = cartId };
        }
        public void AddToCart(Product product)
        {
            var cartItem = _context.ShoppingCartItems.FirstOrDefault(p => p.Product.ProductId == product.ProductId
            && p.CartId == CartId);
            if (cartItem == null)
            {
                cartItem = new CartItem()
                {
                    CartId = CartId,
                    Product = product,
                    Quantity = 1
                };
                _context.ShoppingCartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }
            _context.SaveChanges();
        }
        public void RemoveFromCart(Product product)
        {
            var cartItem = _context.ShoppingCartItems.FirstOrDefault(p => p.Product.ProductId == product.ProductId
             && p.CartId == CartId);
            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(cartItem);
                }
            }
            _context.SaveChanges();
        }

        public List<CartItem> GetCartItems()
        {
            return CartItems ?? (CartItems = _context.ShoppingCartItems.Where(c => c.CartId == CartId)
                .Include(c => c.Product).ToList());
        }
        public double CartTotal()
        {
            var total = _context.ShoppingCartItems.Where(p => p.CartId == CartId).Select(p => p.Product.Cost * p.Quantity).Sum();
            return total;
        }

        public async Task ClearShoppingCartAsync()
        {
            var items = await _context.ShoppingCartItems.Where(n => n.CartId == CartId).ToListAsync();
            _context.ShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

    }
}
