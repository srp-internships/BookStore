using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ReviewService.Application.Services;
using ReviewService.Application.Common.DTOs;

namespace ReviewService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IBookService _bookService;

        public ReviewsController(IReviewService reviewService, IBookService bookService)
        {
            _reviewService = reviewService;
            _bookService = bookService;
        }
        #region GetReviewById
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null)
            {
                return NotFound(new { message = "Review not found." });
            }
            return Ok(review);
        }
        #endregion

        #region Average-rating by BookId
        [HttpGet("book/{bookId}/average-rating")]
        public async Task<IActionResult> GetAverageRatingByBookId(Guid bookId)
        {
            try
            {
                var averageRating = await _reviewService.GetAverageRatingByBookIdAsync(bookId);
                averageRating = Math.Round(averageRating, 2);
                return Ok(new { BookId = bookId, AverageRating = averageRating });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred: {ex.Message}" });
            }
        }
        #endregion

        #region GetReviewByBookId
        [HttpGet("book/{bookId}")]
        public async Task<IActionResult> GetByBookId(Guid bookId)
        {
            try
            {
                var bookExists = await _bookService.BookExistsAsync(bookId);

                if (!bookExists)
                {
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred: {ex.Message}" });
            }
        }
        #endregion

        #region GetReviewByUserId
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            try
            {
                var reviews = await _reviewService.GetByUserIdAsync(userId);

                if (reviews == null || !reviews.Any())
                {
                    return NotFound(new { message = "User has not commented on any reviews." });
                }

                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred: {ex.Message}" });
            }
        }
        #endregion

        #region PostReview
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateReviewDto reviewDto)
        {
            try
            {
                var createdReview = await _reviewService.AddAsync(reviewDto);
                return CreatedAtAction(nameof(GetById), new { id = createdReview.Id }, createdReview);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = $"Error: {ex.Message}. Book might not be found." });
            }
        }
        #endregion

        #region DeleteReviewById
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null)
            {
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
            try
            {
                await _reviewService.DeleteReviewByUserAsync(id, userId);
                return NoContent(); 
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred: {ex.Message}" });
            }
        }
        #endregion
    }
}
