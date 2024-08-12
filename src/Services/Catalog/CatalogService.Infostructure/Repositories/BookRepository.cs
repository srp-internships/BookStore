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
    public class BookRepository
        (CatalogDbContext dbContext) : IBookRepository
    {
        private readonly CatalogDbContext _dbcontext = dbContext;
        public async Task<Guid> CreateAsync(Book book, CancellationToken token = default)
        {
            await _dbcontext.Books.AddAsync(book, token);
            return book.Id;

        }
        public Task<Book> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            return _dbcontext.Books
                .Include(s => s.Categories)
                .Include(s => s.Authors)
                .Include(s => s.Publisher)
                .FirstOrDefaultAsync(x => x.Id.Equals(id), token);
        }

        public Task<List<Book>> GetAllAsync(CancellationToken token)
        {
            return _dbcontext.Books
                .Include(s => s.Categories)
                .Include(s => s.Authors)
                .Include(s => s.Publisher)
                .ToListAsync(token); ;
        }

        public Task<List<Book>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken token = default)
        {
            return _dbcontext.Books
                .Include(s => s.Categories)
                .Include(s => s.Authors)
                .Include(s => s.Publisher)
                .Where(p => ids.Contains(p.Id)).Include(p => p.Authors).ToListAsync(token); ;
        }

        public Task UpdateAsync(Book book, CancellationToken token = default)
        {
             _dbcontext.Books.Update(book);
            return Task.CompletedTask;
        }

        public async Task UpdateCategoriesAsync(Guid id, IEnumerable<Category> newCategories, CancellationToken token = default)
        {
            var book = await GetByIdAsync(id, token);
            book.Categories.Clear();
            book.Categories.AddRange(newCategories);

          
        }

        public async Task UpdateAuthorsAsync(Guid id, IEnumerable<Author> newAuthors, CancellationToken token = default)
        {
            var book = await GetByIdAsync(id, token);
            book.Authors.Clear();
            book.Authors.AddRange(newAuthors);
        }

        public async Task UpdatePublisherAsync(Guid id, Guid newPublisherId, CancellationToken token = default)
        {
            var book = await GetByIdAsync(id, token);
            book.PublisherId = newPublisherId;
        }
        public async Task DeleteAsync(Guid id, CancellationToken token = default)
        {
            await _dbcontext.Books.Where(p => p.Id.Equals(id)).ExecuteDeleteAsync(token);
        }


    }
}
