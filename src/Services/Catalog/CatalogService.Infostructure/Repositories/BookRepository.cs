using CatalogService.Contracts;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Infostructure.Repositories
{
    public class BookRepository
        (CatalogDbContext dbContext,
        IUnitOfWork unitOfWork,
        IBus bus) : IBookRepository
    {
        private readonly CatalogDbContext _dbcontext = dbContext;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IBus _bus = bus;
        public async Task<Guid> CreateAsync(Book book, CancellationToken token = default)
        {
            var existingBook = await _dbcontext.Books.FirstOrDefaultAsync(x => x.ISBN.Equals(book.ISBN), token);
            if (existingBook != null)
            {
                return existingBook.Id;
            }
            await _dbcontext.Books.AddAsync(book, token);
            await _unitOfWork.SaveChangesAsync(token);
            List<Guid> authorIds = new List<Guid>();
            foreach (var author in book.Authors)
            {
                authorIds.Add(author.Id);
            }
            List<Guid> categoryIds = new List<Guid>();
            foreach (var category in book.Categories)
            {
                categoryIds.Add(category.Id);
            }

            await _bus.Publish(new BookCreatedEvent
            {
                Id = book.Id,
                Title = book.Title,
                Image = book.Image,
                AuthorIds = authorIds,
                CategoryIds = categoryIds
            });


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

        public async Task UpdateAsync(Book book, CancellationToken token = default)
        {
            var bookToUpdate = await GetByIdAsync(book.Id, token);
            bookToUpdate.Title = book.Title;
            bookToUpdate.Image = book.Image;
            await _unitOfWork.SaveChangesAsync(token);
            List<Guid> authorIds = new List<Guid>();
            foreach (var author in bookToUpdate.Authors)
            {
                authorIds.Add(author.Id);
            }
            List<Guid> categoryIds = new List<Guid>();
            foreach (var category in bookToUpdate.Categories)
            {
                categoryIds.Add(category.Id);
            }
            await _bus.Publish(new BookUpdatedEvent
            {
                Id = bookToUpdate.Id,
                Title = bookToUpdate.Title,
                Image = bookToUpdate.Image,
                CategoryIds = categoryIds,
                AuthorIds = authorIds
            });
        }

        public async Task UpdateCategoriesAsync(Guid id, IEnumerable<Category> newCategories, CancellationToken token = default)
        {
            var book = await GetByIdAsync(id, token);
            book.Categories.Clear();
            foreach (var category in newCategories)
            {
                book.Categories.Add(category);
            }
            await _unitOfWork.SaveChangesAsync(token);

            List<Guid> authorIds = new List<Guid>();
            foreach (var author in book.Authors)
            {
                authorIds.Add(author.Id);
            }
            List<Guid> categoryIds = new List<Guid>();
            foreach (var category in book.Categories)
            {
                categoryIds.Add(category.Id);
            }
            await _bus.Publish(new BookUpdatedEvent
            {
                Id = book.Id,
                Title = book.Title,
                Image = book.Image,
                CategoryIds = categoryIds,
                AuthorIds = authorIds
            });
        }

        public async Task UpdateAuthorsAsync(Guid id, IEnumerable<Author> newAuthors, CancellationToken token = default)
        {
            var book = await GetByIdAsync(id, token);
            book.Authors.Clear();
            foreach(var author in newAuthors)
            {
                book.Authors.Add(author);
            }
            await _unitOfWork.SaveChangesAsync(token);

            List<Guid> authorIds = new List<Guid>();
            foreach (var author in book.Authors)
            {
                authorIds.Add(author.Id);
            }
            List<Guid> categoryIds = new List<Guid>();
            foreach (var category in book.Categories)
            {
                categoryIds.Add(category.Id);
            }
            await _bus.Publish(new BookUpdatedEvent
            {
                Id = book.Id,
                Title = book.Title,
                Image = book.Image,
                CategoryIds = categoryIds,
                AuthorIds = authorIds
            });
        }

        public async Task UpdatePublisherAsync(Guid id, Guid newPublisherId, CancellationToken token = default)
        {
            var book = await GetByIdAsync(id, token);
            book.PublisherId = newPublisherId;
            await _unitOfWork.SaveChangesAsync(token);
        }
        public async Task DeleteAsync(Guid id, CancellationToken token = default)
        {
            var book = await _dbcontext.Books.FirstOrDefaultAsync(x => x.Id.Equals(id), token);
            if (book == null)
            {
                throw new NotFoundException(nameof(Book), book.Id);
            }
            _dbcontext.Books.Remove(book);
            await _unitOfWork.SaveChangesAsync(token);
        }


    }
}
