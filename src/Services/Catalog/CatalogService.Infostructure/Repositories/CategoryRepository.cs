using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Infostructure.Repositories
{
    public class CategoryRepository(
        CatalogDbContext dbContext,
        IUnitOfWork unitOfWork) : ICategoryRepository
    {
        private CatalogDbContext _dbcontext = dbContext;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Guid> CreateAsync(Category category, CancellationToken token = default)
        {
            var existingCategory = await _dbcontext.Categories.FirstOrDefaultAsync(x => x.Name.Equals(category.Name), token);

            if (existingCategory != null)
            {
                return existingCategory.Id;
            }
            await _dbcontext.Categories.AddAsync(category, token);
            await _unitOfWork.SaveChangesAsync();
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
            var entity = await _dbcontext.Categories.FirstOrDefaultAsync(x
                => x.Id.Equals(category.Id), token);
            entity.Name = category.Name;
            entity.Description = category.Description;

            await _unitOfWork.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id, CancellationToken token = default)
        {
            var category = await _dbcontext.Categories.FirstOrDefaultAsync(x => x.Id.Equals(id), token);
            _dbcontext.Categories.Remove(category);
            await _unitOfWork.SaveChangesAsync(token);
        }


    }
}
