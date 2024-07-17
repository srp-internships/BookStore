using CartService.Aplication.Interfaces;
using CartService.Domain.Entities;
using CartService.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Infrastructure.Repositories
{
    public class CartRepository: ICartRepository
    {
        private readonly CartDbContext _dbContext; // Замените DbContext на вашу реализацию

        public CartRepository(CartDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(CartItem item)
        {
            _dbContext.Set<CartItem>().Add(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Cart>> GetCartItemsAsync(Guid userId)
        {
            return await _dbContext.Set<Cart>()
                .Where(item => item.UserId == userId)
                .ToListAsync();
        }

        public async Task UpdateAsync(CartItem item)
        {
            _dbContext.Entry(item).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid cartItemId)
        {
            var item = await _dbContext.Set<CartItem>().FindAsync(cartItemId);
            if (item != null)
            {
                _dbContext.Set<CartItem>().Remove(item);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
