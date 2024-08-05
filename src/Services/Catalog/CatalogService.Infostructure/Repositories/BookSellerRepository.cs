
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
        (CatalogDbContext dbContext,
        IUnitOfWork unitOfWork) : IBookSellerRepository
    {
        private readonly CatalogDbContext _dbcontext = dbContext;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

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
            entity.Price = bookSeller.Price;
            entity.Description = bookSeller.Description;

            await _unitOfWork.SaveChangesAsync(token);
        }

        public async Task DeleteAsync(Guid id, CancellationToken token = default)
        {
            var entity = await _dbcontext.BookSellers
                .FirstOrDefaultAsync(x => x.Id.Equals(id), token);
            _dbcontext.BookSellers.Remove(entity);
            await _unitOfWork.SaveChangesAsync(token);
        }


    }
}
