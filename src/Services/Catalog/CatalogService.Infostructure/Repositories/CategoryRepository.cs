﻿using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Infostructure.Repositories
{
    public class CategoryRepository
    (CatalogDbContext dbContext) : ICategoryRepository
    {
        private CatalogDbContext _dbcontext = dbContext;
        public async Task<Guid> CreateAsync(Category category, CancellationToken token = default)
        {
            var existingCategory = await _dbcontext.Categories.FirstOrDefaultAsync(x => x.Name.Equals(category.Name), token);

            if (existingCategory != null)
            {
                return existingCategory.Id;
            }
            else
            {
                await _dbcontext.Categories.AddAsync(category, token);
                await _dbcontext.SaveChangesAsync();
                return category.Id;
            }
        }
        public async Task<Category> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            var category = await _dbcontext.Categories.FirstOrDefaultAsync(x => x.Id.Equals(id), token);
            return category;
        }

        public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken token = default)
        {
            var categorylist = await _dbcontext.Categories.ToListAsync(token);
            return categorylist;
        }
        public async Task<IEnumerable<Category>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken token = default)
        {
            var categoryList = await _dbcontext.Categories.Where(p => ids.Contains(p.Id)).ToListAsync(token);
            return categoryList;
        }

        public async Task UpdateAsync(Guid id, Category category, CancellationToken token = default)
        {
            var entity = await _dbcontext.Categories.FirstOrDefaultAsync(Category
                => Category.Id.Equals(id), token);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Category), category.Id);
            }
            entity.Name = category.Name;

            await _dbcontext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id, CancellationToken token = default)
        {
            var category = await _dbcontext.Categories.FirstOrDefaultAsync(x => x.Id.Equals(id), token);
            if (category == null)
            {
                throw new NotFoundException(nameof(Category), category.Id);
            }
            _dbcontext.Categories.Remove(category);
            await _dbcontext.SaveChangesAsync(token);
        }


    }
}
