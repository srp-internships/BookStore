using ReviewService.Domain.Repositories;
using ReviewService.Infrastructure.Persistence.Contexts;

namespace ReviewService.Infrastructure.Repositories
{
    public class ReviewRepository: IReviewRepository
    {
        private readonly ReviewDbContext _context;

        public ReviewRepository(ReviewDbContext context)
        {
            _context = context;
        }

        public async Task<Review> GetByIdAsync(Guid id)
        {
            return await _context.Reviews.FindAsync(id);
        }

        public async Task<double> GetAverageRatingByBookIdAsync(Guid bookId)
        {
            return await _context.Reviews
                .Where(r => r.BookId == bookId)
                .AverageAsync(r => r.Rating);
        }

        public async Task<IEnumerable<Review>> GetByBookIdAsync(Guid bookId)
        {
            return await _context.Reviews
                .Where(r => r.BookId == bookId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Reviews
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task<Review> AddAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
            return review;
        }

        public async Task DeleteAsync(Guid id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
            }
        }

        public async Task<Review?> GetByIdAndUserIdAsync(Guid id, Guid userId)
        {
            return await _context.Reviews
                .Where(r => r.Id == id && r.UserId == userId)
                .FirstOrDefaultAsync();
        }
    }
}

