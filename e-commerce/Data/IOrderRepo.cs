using e_commerce.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace e_commerce.Data
{
    public interface IOrderRepo
    {
        Task StoreOrderAsync(List<CartItem> items, string userId, string userEmailAddress);
        Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);
    }
}
