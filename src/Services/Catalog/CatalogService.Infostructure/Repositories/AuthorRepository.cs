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
    public class AuthorRepository
        (CatalogDbContext dbContext) : IAuthorRepository
    {
        private readonly CatalogDbContext _dbcontext = dbContext;

        public async Task<Guid> CreateAsync(Author author, CancellationToken token = default)
        {
            await _dbcontext.Authors.AddAsync(author, token);
            return author.Id;
        }
        public ValueTask<Author?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            return _dbcontext.Authors.FindAsync(id);
        }

        public async Task<List<Author>> GetAllAsync(CancellationToken token)
        {
            return await _dbcontext.Authors.ToListAsync(token);
        }
        public async Task<List<Author>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken token = default)
        {
            return await _dbcontext.Authors.Where(p => ids.Contains(p.Id)).ToListAsync(token); ;
        }

        public async Task UpdateAsync(Author author, CancellationToken token = default)
        {
            await _dbcontext.Authors
                .Where(u => u.Id.Equals(author.Id))
                .ExecuteUpdateAsync(update => update
                    .SetProperty(u => u.Name, author.Name)
                    .SetProperty(u => u.Description, author.Description), token);
        }
        public async Task DeleteAsync(Guid id, CancellationToken token = default)
        {
            await _dbcontext.Authors.Where(p => p.Id.Equals(id)).ExecuteDeleteAsync(token);
        }

        public Task<bool> AnyAsync(Guid id, CancellationToken token = default)
        {
            return _dbcontext.Authors.AnyAsync(prop => prop.Id.Equals(id));
        }
    }
}
