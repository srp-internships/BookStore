using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Infostructure
{
    public class UnitOfWork(
        CatalogDbContext dbContext,
        IAuthorRepository authorRepository,
        IBookRepository bookRepository,
        ICategoryRepository categoryRepository,
        IBookSellerRepository bookSellerRepository,
        IPublisherRepository publisherRepository) : IUnitOfWork 
    {
        private readonly CatalogDbContext _dbContext = dbContext;
        public IAuthorRepository Authors { get; } = authorRepository;
        public IBookRepository Books { get; } = bookRepository;
        public ICategoryRepository Categories { get; } = categoryRepository;
        public IBookSellerRepository BookSellers { get; } = bookSellerRepository;
        public IPublisherRepository Publishers { get; } = publisherRepository;

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            
            _dbContext.Dispose();
        }


        public static IEnumerable<int> Range(int start, int count)
        {
            long max = ((long)start) + count - 1;
            if (count < 0 || max > int.MaxValue)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count);
            }

            if (count == 0)
            {
                return Empty<int>();
            }

            return new RangeIterator(start, count);
        }

    }
}
