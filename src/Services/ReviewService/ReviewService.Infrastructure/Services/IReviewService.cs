using ReviewService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewService.Infrastructure.Services
{
    public interface IReviewService
    {
        Task<Review> GetReviewByIdAsync(Guid id);
        Task<IEnumerable<Review>> GetReviewsByBookIdAsync(Guid bookId);
        Task<IEnumerable<Review>> GetReviewsByUserIdAsync(Guid userId);
        Task AddReviewAsync(Review review);
        Task UpdateReviewAsync(Review review);
        Task DeleteReviewAsync(Guid id);
    }
}
