using Microsoft.Extensions.Logging;
using ReviewService.Application.IRepositories;
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
        private readonly IBookService _bookService;
        private readonly ILogger<ReviewServices> _logger;

        public ReviewServices(IReviewRepository reviewRepository, IBookService bookService, ILogger<ReviewServices> logger)
        {
            _reviewRepository = reviewRepository;
            _bookService = bookService;
            _logger = logger;
        }

        public async Task<Review> GetReviewByIdAsync(Guid id)
        {
            _logger.LogInformation("Fetching review by ID: {Id}", id);
            return await _reviewRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Review>> GetReviewsByBookIdAsync(Guid bookId)
        {
            _logger.LogInformation("Fetching reviews for book ID: {BookId}", bookId);
            return await _reviewRepository.GetByBookIdAsync(bookId);
        }

        public async Task<IEnumerable<Review>> GetReviewsByUserIdAsync(Guid userId)
        {
            _logger.LogInformation("Fetching reviews by user ID: {UserId}", userId);
            return await _reviewRepository.GetByUserIdAsync(userId);
        }

        public async Task AddReviewAsync(Review review)
        {
            _logger.LogInformation("Adding review for book ID: {BookId}", review.BookId);

            // Validate review rating
            if (review.Rating < 1 || review.Rating > 5)
            {
                _logger.LogWarning("Invalid rating: {Rating}", review.Rating);
                throw new ArgumentException("Rating must be between 1 and 5");
            }

            // Check if the book exists
            var bookExists = await _bookService.BookExistsAsync(review.BookId);
            if (!bookExists)
            {
                _logger.LogWarning("Book does not exist: {BookId}", review.BookId);
                throw new ArgumentException("The book does not exist.");
            }

            review.Id = Guid.NewGuid();
            review.CreatedDate = DateTime.UtcNow;

            await _reviewRepository.AddAsync(review);
            _logger.LogInformation("Review added with ID: {Id}", review.Id);
        }

        public async Task UpdateReviewAsync(Review review)
        {
            _logger.LogInformation("Updating review ID: {Id}", review.Id);

            // Validate review rating
            if (review.Rating < 1 || review.Rating > 5)
            {
                _logger.LogWarning("Invalid rating: {Rating}", review.Rating);
                throw new ArgumentException("Rating must be between 1 and 5");
            }

            await _reviewRepository.UpdateAsync(review);
            _logger.LogInformation("Review updated: {Id}", review.Id);
        }

        public async Task DeleteReviewAsync(Guid id)
        {
            _logger.LogInformation("Deleting review ID: {Id}", id);
            await _reviewRepository.DeleteAsync(id);
            _logger.LogInformation("Review deleted: {Id}", id);
        }
    }
}
