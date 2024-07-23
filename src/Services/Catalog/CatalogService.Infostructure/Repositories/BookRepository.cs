﻿using CatalogService.Contracts;
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
        IBus bus) : IBookRepository
    {
        private readonly CatalogDbContext _dbcontext = dbContext;
        private readonly IBus _bus = bus;
        public async Task<Guid> CreateAsync(Book book, CancellationToken token = default)
        {
            var existingBook = await _dbcontext.Books.FirstOrDefaultAsync(x => x.ISBN.Equals(book.ISBN), token);
            if (existingBook != null)
            {
                return existingBook.Id;
            }
            else
            {
                await _dbcontext.Books.AddAsync(book, token);
                await _dbcontext.SaveChangesAsync(token);
                List<Guid> authorIds = new List<Guid>();
                foreach(var author in book.Authors)
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
            
        }
        public async Task<Book> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            var book = await _dbcontext.Books.FirstOrDefaultAsync(x => x.Id.Equals(id), token);
            if (book == null)
            {
                throw new NotFoundException(nameof(Book), book.Id);
            }
            return book;
        }

        public async Task<IEnumerable<Book>> GetAllAsync(CancellationToken token)
        {
            var booklist = await _dbcontext.Books.ToListAsync(token);
            return booklist;
        }

        public async Task<IEnumerable<Book>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken token = default)
        {
            var booklist = await _dbcontext.Books.Where(p => ids.Contains(p.Id)).ToListAsync(token);
            return booklist;
        }

        public async Task UpdateTitleAsync(Guid id, string newTitle, CancellationToken token = default)
        {
            var book = await GetByIdAsync(id, token);
            book.Title = newTitle;
            await _dbcontext.SaveChangesAsync(token);
        }

        public async Task UpdateImageAsync(Guid id, string newImage, CancellationToken token = default)
        {
            var book = await GetByIdAsync(id, token);
            book.Image = newImage;
            await _dbcontext.SaveChangesAsync(token);
        }

        public async Task UpdateCategoriesAsync(Guid id, IEnumerable<Category> newCategories, CancellationToken token = default)
        {
            var book = await GetByIdAsync(id, token);
            foreach(var category in newCategories)
            {
                book.Categories.Add(category);
            }
            await _dbcontext.SaveChangesAsync(token);
        }

        public async Task UpdateAuthorsAsync(Guid id, IEnumerable<Author> newAuthors, CancellationToken token = default)
        {
            var book = await GetByIdAsync(id, token);
            foreach(var author in newAuthors)
            {
                book.Authors.Add(author);
            }
            await _dbcontext.SaveChangesAsync(token);
        }

        public async Task UpdatePublisherAsync(Guid id, Guid newPublisherId, CancellationToken token = default)
        {
            var book = await GetByIdAsync(id, token);
            book.PublisherId = newPublisherId;
            await _dbcontext.SaveChangesAsync(token);
        }
        public async Task DeleteAsync(Guid id, CancellationToken token = default)
        {
            Book book = await _dbcontext.Books.FirstOrDefaultAsync(x => x.Id.Equals(id), token);
            if (book == null)
            {
                throw new NotFoundException(nameof(Book), book.Id);
            }
            _dbcontext.Books.Remove(book);
            _dbcontext.SaveChangesAsync(token);
        }


    }
}
