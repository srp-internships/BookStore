using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecommendationService.Application;
using RecommendationService.Application.Interfaces;
using RecommendationService.Contracts;

namespace RecommendationService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IApplicationDbContext _dbContext;
        public BookController(IApplicationDbContext dbContext)
        {

            _dbContext = dbContext;

        }
        

        [HttpGet("popular/{bookId}")]
        public async Task<IActionResult> GetPopularBooks(Guid bookId)
        {            
            var book = await _dbContext.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == bookId);

            if (book == null)
            {
                return NotFound("Book not found.");
            }
                       
            var popularBooks = await _dbContext.Books
                .Where(b => b.BookCategories == book.BookCategories) 
                .Select(b => new
                {
                    b.Id,
                    b.Title,
                    b.BookCategories
                })
                .ToListAsync();

            return Ok(popularBooks);
        }
        
        
        [HttpGet("similar/{bookId}")]
        public async Task<IActionResult> GetSimilarBooks(Guid bookId)
        {
            
            var book = await _dbContext.Books
                .AsNoTracking() 
                .FirstOrDefaultAsync(b => b.Id == bookId);

            if (book == null)
            {
                return NotFound("Book not found.");
            }

            
            var similarBooks = await _dbContext.Books
                .Where(b => b.AuthorId == book.AuthorId && b.Id != bookId) 
                .Select(b => new
                {
                    b.Id,
                    b.Title,
                    b.AuthorId
                })
                .ToListAsync();

            return Ok(similarBooks);
        }
    }
}
