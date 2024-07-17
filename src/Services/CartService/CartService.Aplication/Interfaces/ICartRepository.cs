using CartService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Aplication.Interfaces
{
    public interface ICartRepository
    {
        Task AddAsync(CartItem item);
        Task<List<CartItem>> GetCartItemsAsync(string userId);
        Task UpdateAsync(CartItem item);
        Task RemoveAsync(int cartItemId);
    }
}
