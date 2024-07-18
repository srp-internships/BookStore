using AnalyticService.Domain.Entities;
using AnalyticsService.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnalyticsServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookSalesController : ControllerBase
    {
        private readonly IBookSaleRepository _bookSaleRepository;

        public BookSalesController(IBookSaleRepository bookSaleRepository)
        {
            _bookSaleRepository = bookSaleRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookSale>>> GetAllBookSales()
        {
            var bookSales = await _bookSaleRepository.GetAllBookSalesAsync();
            return Ok(bookSales);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookSale>> GetBookSaleById(Guid id)
        {
            var bookSale = await _bookSaleRepository.GetBookSaleByIdAsync(id);
            if (bookSale == null)
            {
                return NotFound();
            }
            return Ok(bookSale);
        }
        
        [HttpGet("api/analytics/salesreport")]
        public async Task<IActionResult> GetSalesReport(DateTime startDate, DateTime endDate)
        {
            try
            {
                var report = await _bookSaleRepository.GetSalesReportByDateAsync(startDate, endDate);
                return Ok(report);
            }
            catch (Exception ex)
            {
                // Обработка исключений и возврат соответствующего ответа
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddBookSale([FromBody] BookSale bookSale)
        {
            if (bookSale == null)
            {
                return BadRequest();
            }

            await _bookSaleRepository.AddBookSaleAsync(bookSale);
            return CreatedAtAction(nameof(GetBookSaleById), new { id = bookSale.Id }, bookSale);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBookSale(Guid id, [FromBody] BookSale bookSale)
        {
            if (id != bookSale.Id)
            {
                return BadRequest();
            }

            var existingBookSale = await _bookSaleRepository.GetBookSaleByIdAsync(id);
            if (existingBookSale == null)
            {
                return NotFound();
            }

            await _bookSaleRepository.UpdateBookSaleAsync(bookSale);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBookSale(Guid id)
        {
            var existingBookSale = await _bookSaleRepository.GetBookSaleByIdAsync(id);
            if (existingBookSale == null)
            {
                return NotFound();
            }

            await _bookSaleRepository.DeleteBookSaleAsync(id);
            return NoContent();
        }

    }
}
