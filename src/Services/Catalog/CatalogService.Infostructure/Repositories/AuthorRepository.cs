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
    public class AuthorRepository
        (CatalogDbContext dbContext,
        IUnitOfWork unitOfWork) : IAuthorRepository
    {
        private readonly CatalogDbContext _dbcontext = dbContext;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Guid> CreateAsync(Author author, CancellationToken token = default)
        {
            var existingAuthor = await _dbcontext.Authors
                .FirstOrDefaultAsync(x => x.Name.Equals(author.Name), token);

            if(existingAuthor is null)
            {
                await _dbcontext.Authors.AddAsync(author, token);
                await _unitOfWork.SaveChangesAsync();
                return author.Id;
            }
            return existingAuthor.Id;
        }
        public Task<Author> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            return _dbcontext.Authors.FirstOrDefaultAsync(x => x.Id.Equals(id), token);
        }

        public Task<List<Author>> GetAllAsync(CancellationToken token)
        {
            return _dbcontext.Authors.ToListAsync(token);
        }
        public Task<List<Author>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken token = default)
        {
            return _dbcontext.Authors.Where(p => ids.Contains(p.Id)).ToListAsync(token); ;
        }

        public async Task UpdateAsync(Author author, CancellationToken token = default)
        {
            var entity = await _dbcontext.Authors.FirstOrDefaultAsync(e
                => e.Id.Equals(author.Id), token);
            entity.Name = author.Name;
            entity.Description = author.Description;

            await _unitOfWork.SaveChangesAsync(token);
        }
        public async Task DeleteAsync(Guid id, CancellationToken token = default)
        {
            var entity = await _dbcontext.Authors.FirstOrDefaultAsync(x => x.Id.Equals(id), token);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Book), entity.Id);
            }
            _dbcontext.Authors.Remove(entity);
            await _unitOfWork.SaveChangesAsync(token);
        }
        

    }
}
