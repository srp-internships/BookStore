using ReviewService.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewService.Application.Services
{
    public interface IReviewService
    {
        Task<ReviewDto> GetByIdAsync(Guid id);
        Task<double> GetAverageRatingByBookIdAsync(Guid bookId);
        Task<IEnumerable<ReviewDto>> GetByBookIdAsync(Guid bookId);
        Task<IEnumerable<ReviewDto>> GetByUserIdAsync(Guid userId);
        Task<ReviewDto> AddAsync(CreateReviewDto reviewDto);
        Task DeleteAsync(Guid id);
        Task DeleteReviewByUserAsync(Guid reviewId, Guid userId);
    }
}
