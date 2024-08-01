using ReviewService.Application.IRepositories;
using ReviewService.Application.Services;
using ReviewService.Domain.DTOs;
using ReviewService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewService.Infrastructure.Services
{
    public class ReviewServices : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewServices(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<ReviewDto> GetByIdAsync(Guid id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
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
            var averageRating = await _reviewRepository.GetAverageRatingByBookIdAsync(bookId);
            return Math.Round(averageRating, 2); 
        }

        public async Task<IEnumerable<ReviewDto>> GetByBookIdAsync(Guid bookId)
        {
            var reviews = await _reviewRepository.GetByBookIdAsync(bookId);
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
            var reviews = await _reviewRepository.GetByUserIdAsync(userId);
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

            var createdReview = await _reviewRepository.AddAsync(review);

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
            await _reviewRepository.DeleteAsync(id);
        }
    }
}
