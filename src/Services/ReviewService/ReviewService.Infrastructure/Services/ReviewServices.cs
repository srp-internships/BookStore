using ReviewService.Application.IRepositories;
using ReviewService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewService.Infrastructure.Services
{
    public class ReviewServices: IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewServices(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<Review> GetReviewByIdAsync(Guid id)
        {
            return await _reviewRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Review>> GetReviewsByBookIdAsync(Guid bookId)
        {
            return await _reviewRepository.GetByBookIdAsync(bookId);
        }

        public async Task<IEnumerable<Review>> GetReviewsByUserIdAsync(Guid userId)
        {
            return await _reviewRepository.GetByUserIdAsync(userId);
        }

        public async Task AddReviewAsync(Review review)
        {
            // Валидация данных отзыва перед добавлением
            if (review.Rating < 1 || review.Rating > 5)
            {
                throw new ArgumentException("Rating must be between 1 and 5");
            }

            review.Id = Guid.NewGuid();
            review.CreatedDate = DateTime.UtcNow;

            await _reviewRepository.AddAsync(review);
        }

        public async Task UpdateReviewAsync(Review review)
        {
            // Валидация данных отзыва перед обновлением
            if (review.Rating < 1 || review.Rating > 5)
            {
                throw new ArgumentException("Rating must be between 1 and 5");
            }

            await _reviewRepository.UpdateAsync(review);
        }

        public async Task DeleteReviewAsync(Guid id)
        {
            await _reviewRepository.DeleteAsync(id);
        }
    }
}
