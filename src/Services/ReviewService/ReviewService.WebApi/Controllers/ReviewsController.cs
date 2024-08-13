using Microsoft.AspNetCore.Mvc;
using ReviewService.Application.Common.DTOs;
using ReviewService.Application.Services;
using ReviewService.Domain.Exceptions;

namespace ReviewService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IBookService _bookService;
        private readonly ILogger<ReviewsController> _logger;

        public ReviewsController(IReviewService reviewService, IBookService bookService, ILogger<ReviewsController> logger)
        {
            _reviewService = reviewService;
            _bookService = bookService;
            _logger = logger;
        }
        #region GetReviewById
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation("Getting review with ID {Id}", id);
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null)
            {
                _logger.LogWarning("Review with ID {Id} not found", id);
                return NotFound(new { message = "Review not found." });
            }
            return Ok(review);
        }
        #endregion

        #region Average-rating by BookId
        [HttpGet("book/{bookId}/average-rating")]
        public async Task<IActionResult> GetAverageRatingByBookId(Guid bookId)
        {
            _logger.LogInformation("Getting average rating for book with ID {BookId}", bookId);
            try
            {
                var averageRating = await _reviewService.GetAverageRatingByBookIdAsync(bookId);
                averageRating = Math.Round(averageRating, 2);
                return Ok(new { BookId = bookId, AverageRating = averageRating });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting average rating for book with ID {BookId}", bookId);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred: {ex.Message}" });
            }
        }
        #endregion

        #region GetReviewByBookId
        [HttpGet("book/{bookId}")]
        public async Task<IActionResult> GetByBookId(Guid bookId)
        {
            _logger.LogInformation("Getting reviews for book with ID {BookId}", bookId);
            try
            {
                var bookExists = await _bookService.BookExistsAsync(bookId);

                if (!bookExists)
                {
                    _logger.LogWarning("Book with ID {BookId} not found", bookId);
                    return NotFound(new { message = "Book not found in database." });
                }

                var reviews = await _reviewService.GetByBookIdAsync(bookId);
                var averageRating = await _reviewService.GetAverageRatingByBookIdAsync(bookId);

                averageRating = Math.Round(averageRating, 2);

                if (reviews == null || !reviews.Any())
                {
                    return Ok(new
                    {
                        Message = "No reviews found for this book.",
                        AverageRating = averageRating
                    });
                }

                return Ok(new
                {
                    Reviews = reviews,
                    AverageRating = averageRating.ToString("F2")
                });
            }
            catch (ReviewNotFoundException ex)
            {
                _logger.LogError(ex, "Review not found for book with ID {BookId}", bookId);
                return NotFound(new { message = $"Review not found: {ex.Message}" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting reviews for book with ID {BookId}", bookId);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An internal server error occurred." });
            }
        }
        #endregion

        #region GetReviewByUserId
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            _logger.LogInformation("Getting reviews for user with ID {UserId}", userId);
            try
            {
                var reviews = await _reviewService.GetByUserIdAsync(userId);

                if (reviews == null || !reviews.Any())
                {
                    _logger.LogWarning("User with ID {UserId} has not commented on any reviews", userId);
                    return NotFound(new { message = "User has not commented on any reviews." });
                }

                return Ok(reviews);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting reviews for user with ID {UserId}", userId);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred: {ex.Message}" });
            }
        }
        #endregion

        #region PostReview
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateReviewDto reviewDto)
        {
            _logger.LogInformation("Adding new review for book with ID {BookId}", reviewDto.BookId);
            try
            {
                var createdReview = await _reviewService.AddAsync(reviewDto);
                return CreatedAtAction(nameof(GetById), new { id = createdReview.Id }, createdReview);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding review for book with ID {BookId}", reviewDto.BookId);
                return NotFound(new { message = $"Error: {ex.Message}. Book might not be found." });
            }
        }
        #endregion

        #region DeleteReviewById
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Deleting review with ID {Id}", id);
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null)
            {
                _logger.LogWarning("Review with ID {Id} not found", id);
                return NotFound(new { message = "Review not found." });
            }
            await _reviewService.DeleteAsync(id);
            return NoContent();
        }
        #endregion

        #region DeleteReviewByUserId
        [HttpDelete("{id}/user/{userId}")]
        public async Task<IActionResult> DeleteByUserId(Guid id, Guid userId)
        {
            _logger.LogInformation("Deleting review with ID {Id} for user with ID {UserId}", id, userId);
            try
            {
                await _reviewService.DeleteReviewByUserAsync(id, userId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Review with ID {Id} or user with ID {UserId} not found", id, userId);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting review with ID {Id} for user with ID {UserId}", id, userId);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred: {ex.Message}" });
            }
        }
        #endregion
    }
}
