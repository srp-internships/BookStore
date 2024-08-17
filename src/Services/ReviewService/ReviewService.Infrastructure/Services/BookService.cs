﻿using CatalogService.Contracts;
using ReviewService.Application.Services;
using ReviewService.Domain.Repositories;

namespace ReviewService.Infrastructure.Services
{
    public class BookService: IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task AddBookAsync(BookCreatedEvent bookCreatedEvent)
        {
            var book = new Book
            {
                Id = bookCreatedEvent.Id,
            };

            await _bookRepository.AddBookAsync(book);
        }

        public async Task<bool> BookExistsAsync(Guid bookId)
        {
            return await _bookRepository.BookExistsAsync(bookId);
        }
    }
}
