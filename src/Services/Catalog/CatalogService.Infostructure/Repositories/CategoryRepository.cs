using CatalogService.Domain.Entities;
using CatalogService.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Infostructure.Repositories
{
    public class CategoryRepository(
        CatalogDbContext dbContext) : ICategoryRepository
    {
        private CatalogDbContext _dbcontext = dbContext;
        public async Task<Guid> CreateAsync(Category category, CancellationToken token = default)
        {
            var existingCategory = await _dbcontext.Categories.FirstOrDefaultAsync(x => x.Name.Equals(category.Name), token);

            if (existingCategory != null)
            {
                return existingCategory.Id;
            }
            await _dbcontext.Categories.AddAsync(category, token);
            return category.Id;
        }
        public Task<Category> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            return _dbcontext.Categories.FirstOrDefaultAsync(x => x.Id.Equals(id), token);
        }

        public Task<List<Category>> GetAllAsync(CancellationToken token = default)
        {
            return _dbcontext.Categories.ToListAsync(token);
        }
        public Task<List<Category>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken token = default)
        {
            return _dbcontext.Categories.Where(p => ids.Contains(p.Id)).ToListAsync(token); ;
        }

        public async Task UpdateAsync(Category category, CancellationToken token = default)
        {
            await _dbcontext.Categories
             .Where(u => u.Id.Equals(category.Id))
             .ExecuteUpdateAsync(update => update
                .SetProperty(u => u.Name, category.Name)
                .SetProperty(u => u.Description, category.Description), token);
        }
        public async Task DeleteAsync(Guid id, CancellationToken token = default)
        {
            await _dbcontext.Categories.Where(p => p.Id.Equals(id)).ExecuteDeleteAsync(token);
        }

        public Task<bool> AnyAsync(Guid id, CancellationToken token = default)
        {
            return _dbcontext.Categories.AnyAsync(prop => prop.Id.Equals(id));
        }
    }
}
