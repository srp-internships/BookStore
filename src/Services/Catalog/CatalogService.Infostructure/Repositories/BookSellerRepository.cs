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
    public class BookSellerRepository
        (CatalogDbContext dbContext,
        IBus bus) : IBookSellerRepository
    {
        private readonly CatalogDbContext _dbcontext = dbContext;
        private readonly IBus _bus = bus;
        public async Task<Guid> CreateAsync(BookSeller bookSeller, CancellationToken token = default)
        {
            var existingBookSeller = await _dbcontext.BookSellers
                .FirstOrDefaultAsync(p => p.SellerId.Equals(bookSeller.SellerId), token);
            if (existingBookSeller.BookId.Equals(bookSeller.BookId))
            {
                return existingBookSeller.Id;
            }

            await _dbcontext.BookSellers.AddAsync(bookSeller, token);
            await _dbcontext.SaveChangesAsync(token);
            await _bus.Publish(new BookSellerCreatedEvent
            {
                Id = Guid.NewGuid(),
                BookId = bookSeller.BookId,
                SellerId = bookSeller.SellerId,
                Price = bookSeller.Price,
                Amount = bookSeller.Amount
            });
            var guid = bookSeller.Id;
            return guid;

        }
        public async Task<BookSeller> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            var bookSeller = await _dbcontext.BookSellers
                .FirstOrDefaultAsync(x => x.Id.Equals(id), token);
            return bookSeller;
        }

        public async Task UpdateAsync(BookSeller bookSeller, CancellationToken token = default)
        {
            BookSeller entity = await _dbcontext.BookSellers.FirstOrDefaultAsync(bookSeller
                => bookSeller.Id.Equals(bookSeller), token);
            if (entity == null)
            {
                throw new NotFoundException(nameof(BookSeller), bookSeller.Id);
            }
            entity.Price = bookSeller.Price;
            entity.Amount = bookSeller.Amount;
            entity.Description = bookSeller.Description;

            await _dbcontext.SaveChangesAsync(token);

            await _bus.Publish(new BookSellerUpdatedEvent
            {
                Id = Guid.NewGuid(),
                BookId = bookSeller.BookId,
                SellerId = bookSeller.SellerId,
                Price = bookSeller.Price,
                Amount = bookSeller.Amount
            });
        }

        public async Task DeleteAsync(Guid id, CancellationToken token = default)
        {
            Author entity = await _dbcontext.Authors
                .FirstOrDefaultAsync(x => x.Id.Equals(id), token);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Book), entity.Id);
            }
            _dbcontext.Authors.Remove(entity);
            await _dbcontext.SaveChangesAsync(token);
        }


    }
}
