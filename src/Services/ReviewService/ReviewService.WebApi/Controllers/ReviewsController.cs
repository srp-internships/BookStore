using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc;
using ReviewService.Infrastructure.Services;
using ReviewService.Domain.Entities;
using ReviewService.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using ReviewService.Application.Services;

namespace ReviewService.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IBookService _bookService;

        public ReviewsController(IReviewService reviewService, IBookService bookService)
        {
            _reviewService = reviewService;
            _bookService = bookService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }
       
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
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        
        [HttpGet("book/{bookId}")]
        public async Task<IActionResult> GetByBookId(Guid bookId)
        {
            try
            {
                var bookExists = await _bookService.BookExistsAsync(bookId);

                if (!bookExists)
                {
                    return NotFound("Book not found in database.");
                }

                var reviews = await _reviewService.GetByBookIdAsync(bookId);

                if (reviews == null || !reviews.Any())
                {
                    var averageRating = await _reviewService.GetAverageRatingByBookIdAsync(bookId);
                    averageRating = Math.Round(averageRating, 2); 
                    return Ok(new
                    {
                        Message = "No reviews found for this book.",
                        AverageRating = averageRating
                    });
                }

                var response = new
                {
                    Reviews = reviews,
                    AverageRating = (await _reviewService.GetAverageRatingByBookIdAsync(bookId)).ToString("F2") 
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            try
            {
                var reviews = await _reviewService.GetByUserIdAsync(userId);

                if (reviews == null || !reviews.Any())
                {
                    return NotFound("User has not commented on any reviews.");
                }

                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

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
                return NotFound(new { message = ex.Message + " Book Not Found" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null)
            {
                return NotFound("Review not found!");
            }
            await _reviewService.DeleteAsync(id);
            return Ok("Successfully deleted");
        }
    }

}
