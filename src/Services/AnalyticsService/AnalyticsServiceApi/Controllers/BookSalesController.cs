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
        private readonly IBookSaleService _bookSaleService;

        public BookSalesController(IBookSaleService bookSaleService)
        {
            _bookSaleService = bookSaleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookSale>>> GetAllBookSales()
        {
            var bookSales = await _bookSaleService.GetAllBookSalesAsync();
            return Ok(bookSales);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookSale>> GetBookSaleById(Guid id)
        {
            var bookSale = await _bookSaleService.GetBookSaleByIdAsync(id);
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
                var report = await _bookSaleService.GetSalesReportByDateAsync(startDate, endDate);
                return Ok(report);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("api/analytics/sallerId")]
        public async Task<IActionResult> GetBySellerId(Guid sellerId)
        {
            try
            {
                var report = await _bookSaleService.GetBookSaleByIdAsync(sellerId);
                return Ok(report);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}