using Microsoft.EntityFrameworkCore;
using ReviewService.Application.IRepositories;
using ReviewService.Domain.Entities;
using ReviewService.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var averageRating = await _context.Reviews
                .Where(r => r.BookId == bookId)
                .AverageAsync(r => r.Rating);

            return averageRating;
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
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task DeleteAsync(Guid id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }
    }
}

