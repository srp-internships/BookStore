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
        public async Task<Author> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            var author = await _dbcontext.Authors.FirstOrDefaultAsync(x => x.Id.Equals(id), token);
            return author;
        }

        public async Task<IEnumerable<Author>> GetAllAsync(CancellationToken token)
        {
            var authorlist = await _dbcontext.Authors.ToListAsync(token);
            return authorlist;
        }
        public async Task<IEnumerable<Author>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken token = default)
        {
            var authorlist = await _dbcontext.Authors.Where(p => ids.Contains(p.Id)).ToListAsync(token);
            return authorlist;
        }

        public async Task UpdateAsync(Author author, CancellationToken token = default)
        {
            var entity = await _dbcontext.Authors.FirstOrDefaultAsync(author
                => author.Id.Equals(author.Id), token);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Author), author.Id);
            }
            entity.Name = author.Name;
            entity.Description = author.Description;

            await _unitOfWork.SaveChangesAsync();
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
