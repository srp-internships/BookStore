using ReviewService.Application.Common.DTOs;
using ReviewService.Application.Services;
using ReviewService.Domain.Entities;

namespace ReviewService.Infrastructure.Services
{
    public class ReviewServices : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ReviewDto> GetByIdAsync(Guid id)
        {
            var review = await _unitOfWork.Reviews.GetByIdAsync(id);
            if (review == null)
            {
                return null;
            }

            return new ReviewDto
            {
                Id = review.Id,
                BookId = review.BookId,
                UserId = review.UserId,
                Comment = review.Comment,
                Rating = review.Rating,
                CreatedDate = review.CreatedDate
            };
        }

        public async Task<double> GetAverageRatingByBookIdAsync(Guid bookId)
        {
            var averageRating = await _unitOfWork.Reviews.GetAverageRatingByBookIdAsync(bookId);
            return Math.Round(averageRating, 2);
        }

        public async Task<IEnumerable<ReviewDto>> GetByBookIdAsync(Guid bookId)
        {
            var reviews = await _unitOfWork.Reviews.GetByBookIdAsync(bookId);
            return reviews.Select(r => new ReviewDto
            {
                Id = r.Id,
                BookId = r.BookId,
                UserId = r.UserId,
                Comment = r.Comment,
                Rating = r.Rating,
                CreatedDate = r.CreatedDate
            });
        }

        public async Task<IEnumerable<ReviewDto>> GetByUserIdAsync(Guid userId)
        {
            var reviews = await _unitOfWork.Reviews.GetByUserIdAsync(userId);
            return reviews.Select(r => new ReviewDto
            {
                Id = r.Id,
                BookId = r.BookId,
                UserId = r.UserId,
                Comment = r.Comment,
                Rating = r.Rating,
                CreatedDate = r.CreatedDate
            });
        }

        public async Task<ReviewDto> AddAsync(CreateReviewDto reviewDto)
        {
            var review = new Review
            {
                Id = Guid.NewGuid(),
                BookId = reviewDto.BookId,
                UserId = reviewDto.UserId,
                Comment = reviewDto.Comment,
                Rating = reviewDto.Rating,
                CreatedDate = DateTime.UtcNow
            };

            var createdReview = await _unitOfWork.Reviews.AddAsync(review);
            await _unitOfWork.CompleteAsync();

            return new ReviewDto
            {
                Id = createdReview.Id,
                BookId = createdReview.BookId,
                UserId = createdReview.UserId,
                Comment = createdReview.Comment,
                Rating = createdReview.Rating,
                CreatedDate = createdReview.CreatedDate
            };
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.Reviews.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteReviewByUserAsync(Guid reviewId, Guid userId)
        {
            var review = await _unitOfWork.Reviews.GetByIdAndUserIdAsync(reviewId, userId);
            if (review == null)
            {
                throw new KeyNotFoundException("Review not found or does not belong to the user.");
            }

            await _unitOfWork.Reviews.DeleteAsync(reviewId);
            await _unitOfWork.CompleteAsync();
        }
    }
}
