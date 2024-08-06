using ReviewService.Application.Services;
using ReviewService.Domain.Repositories;
using ReviewService.Infrastructure.Persistence.Contexts;

namespace ReviewService.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ReviewDbContext _context;
        public UnitOfWork(ReviewDbContext context,
                          IReviewRepository reviewRepository)
        {
            _context = context;
            Reviews = reviewRepository;
        }
        public IReviewRepository Reviews { get; }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
