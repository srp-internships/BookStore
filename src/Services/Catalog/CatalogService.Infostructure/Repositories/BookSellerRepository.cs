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
            Guid sellerId = bookSeller.SellerId;
            var seller = await _dbcontext.Sellers
                .FirstOrDefaultAsync(x => x.Id.Equals(sellerId), token);
            var book = await _dbcontext.Books
                .FirstOrDefaultAsync(x => x.Id.Equals(bookSeller.BookId), token);

            if (seller == null)
                throw new NotFoundException(nameof(Seller), bookSeller.SellerId);
            if (book == null)
                throw new NotFoundException(nameof(Book), bookSeller.BookId);

            var newBookSeller = new BookSeller
            {
                Id = Guid.NewGuid(),
                Book = book,
                Amount = bookSeller.Amount,
                Price = bookSeller.Price,
                Description = bookSeller.Description
            };

            seller.BookSellers.Add(newBookSeller);

            await _dbcontext.SaveChangesAsync(token);

            return newBookSeller.Id;

        }
        public async Task<BookSeller> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            var bookSeller = await _dbcontext.BookSellers.FirstOrDefaultAsync(x => x.Id.Equals(id), token);
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
