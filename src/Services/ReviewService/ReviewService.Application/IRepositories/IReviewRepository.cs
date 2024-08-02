using ReviewService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewService.Application.IRepositories
{
    public interface IReviewRepository
    {
        Task<Review> GetByIdAsync(Guid id);
        Task<double> GetAverageRatingByBookIdAsync(Guid bookId);
        Task<IEnumerable<Review>> GetByBookIdAsync(Guid bookId);
        Task<IEnumerable<Review>> GetByUserIdAsync(Guid userId);
        Task<Review> AddAsync(Review review);
        Task DeleteAsync(Guid id);
        Task<Review?> GetByIdAndUserIdAsync(Guid id, Guid userId); 
    }
}
