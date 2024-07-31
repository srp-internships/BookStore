using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc;
using ReviewService.Infrastructure.Services;
using ReviewService.Domain.Entities;
using ReviewService.Domain.DTOs;

namespace ReviewService.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
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
                var reviews = await _reviewService.GetByBookIdAsync(bookId);
                return Ok(reviews);
            }
            catch (Exception ex) 
            {
                return NotFound("Book not found in database");
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            try
            {
                var reviews = await _reviewService.GetByUserIdAsync(userId);
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
                var content = CreatedAtAction(nameof(GetById), new { id = createdReview.Id }, createdReview);
                return Ok($" Created Review by {reviewDto.UserId} to {reviewDto.BookId} ");
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message +"Book Not Found" });
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
