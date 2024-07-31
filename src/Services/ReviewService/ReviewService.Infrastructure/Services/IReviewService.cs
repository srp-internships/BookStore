using ReviewService.Domain.DTOs;
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
        Task<ReviewDto> GetByIdAsync(Guid id);
        Task<double> GetAverageRatingByBookIdAsync(Guid bookId);
        Task<IEnumerable<ReviewDto>> GetByBookIdAsync(Guid bookId);
        Task<IEnumerable<ReviewDto>> GetByUserIdAsync(Guid userId);
        Task<ReviewDto> AddAsync(CreateReviewDto reviewDto);
        Task DeleteAsync(Guid id);
    }
}
