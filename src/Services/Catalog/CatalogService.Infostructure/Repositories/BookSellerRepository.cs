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
    public class BookSellerRepository
        (CatalogDbContext dbContext) : IBookSellerRepository
    {
        private CatalogDbContext _dbcontext = dbContext;
        public async Task<Guid> CreateAsync(BookSeller bookSeller, CancellationToken token = default)
        {
            var existingBookSeller = await _dbcontext.BookSellers.FirstOrDefaultAsync(x => x.SellerId.Equals(bookSeller.SellerId), token);

            if (existingBookSeller.Id.Equals(bookSeller.Id))
            {
                throw new Exception("Already exists");
            }
            else
            {
                await _dbcontext.BookSellers.AddAsync(bookSeller, token);
                await _dbcontext.SaveChangesAsync(token);
                return bookSeller.Id;
            }
        }
        public async Task<BookSeller> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            var bookSeller = await _dbcontext.BookSellers.FirstOrDefaultAsync(x => x.Id.Equals(id), token);
            return bookSeller;
        }

        public async Task UpdatePriceAsync(Guid id, decimal price, CancellationToken token = default)
        {
            BookSeller entity = await _dbcontext.BookSellers.FirstOrDefaultAsync(bookSeller
                => bookSeller.Id.Equals(id), token);
            if (entity == null)
            {
                throw new NotFoundException(nameof(BookSeller), id);
            }
            entity.Price = price;

            await _dbcontext.SaveChangesAsync(token);
        }
        public async Task UpdateAmountAsync(Guid id, int amount, CancellationToken token = default)
        {
            BookSeller entity = await _dbcontext.BookSellers.FirstOrDefaultAsync(bookSeller
                => bookSeller.Id.Equals(id), token);
            if (entity == null)
            {
                throw new NotFoundException(nameof(BookSeller), id);
            }
            entity.Amount = amount;

            await _dbcontext.SaveChangesAsync(token);
        }
        public async Task UpdateDescriptionAsync(Guid id, string description, CancellationToken token = default)
        {
            BookSeller entity = await _dbcontext.BookSellers.FirstOrDefaultAsync(bookSeller
                => bookSeller.Id.Equals(id), token);
            if (entity == null)
            {
                throw new NotFoundException(nameof(BookSeller), id);
            }
            entity.Description = description;

            await _dbcontext.SaveChangesAsync(token);
        }
        public async Task DeleteAsync(Guid id, CancellationToken token = default)
        {
            Author entity = await _dbcontext.Authors.FirstOrDefaultAsync(x => x.Id.Equals(id), token);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Book), entity.Id);
            }
            _dbcontext.Authors.Remove(entity);
            await _dbcontext.SaveChangesAsync(token);
        }


    }
}
