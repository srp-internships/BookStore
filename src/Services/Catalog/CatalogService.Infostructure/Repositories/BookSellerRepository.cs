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
        IUnitOfWork unitOfWork,
        IBus bus) : IBookSellerRepository
    {
        private readonly CatalogDbContext _dbcontext = dbContext;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IBus _bus = bus;
        public async Task CreateAsync(BookSeller bookSeller, CancellationToken token = default)
        {
            var existingBookSeller = await _dbcontext.BookSellers
                .FirstOrDefaultAsync(p => p.SellerId.Equals(bookSeller.SellerId), token);
            if(existingBookSeller is not null)
            {
                if (existingBookSeller.BookId.Equals(bookSeller.BookId))
                {
                    throw new Exception("Bookseller already exists");
                }
            }

            await _dbcontext.BookSellers.AddAsync(bookSeller, token);

            await _unitOfWork.SaveChangesAsync(token);
            await _bus.Publish(new PriceCreatedEvent
            {
                BookId = bookSeller.BookId,
                SellerId = bookSeller.SellerId,
                Price = bookSeller.Price,
            });

        }
        public Task<BookSeller> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            return _dbcontext.BookSellers
                .Include(bs => bs.Book)
                .Include(bs => bs.Seller)
                .FirstOrDefaultAsync(bs => bs.Id.Equals(id), token); ;
        }

        public async Task UpdateAsync(BookSeller bookSeller, CancellationToken token = default)
        {
            var entity = await _dbcontext.BookSellers.FirstOrDefaultAsync(x
                => x.Id.Equals(bookSeller.Id), token);
            if (entity == null)
            {
                throw new NotFoundException(nameof(BookSeller), bookSeller.Id);
            }
            entity.Price = bookSeller.Price;
            entity.Description = bookSeller.Description;

            await _unitOfWork.SaveChangesAsync(token);

            await _bus.Publish(new PriceUpdatedEvent
            {
                BookId = bookSeller.BookId,
                SellerId = bookSeller.SellerId,
                Price = bookSeller.Price,
            });
        }

        public async Task DeleteAsync(Guid id, CancellationToken token = default)
        {
            var entity = await _dbcontext.BookSellers
                .FirstOrDefaultAsync(x => x.Id.Equals(id), token);
            if (entity == null)
            {
                throw new NotFoundException(nameof(BookSeller), entity.Id);
            }
            _dbcontext.BookSellers.Remove(entity);
            await _unitOfWork.SaveChangesAsync(token);
        }


    }
}
