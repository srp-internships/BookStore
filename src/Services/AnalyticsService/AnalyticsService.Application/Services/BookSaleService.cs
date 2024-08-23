using AnalyticService.Domain.Entities;
using AnalyticsService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalyticService.Domain.Exceptions;

namespace AnalyticsService.Application.Services
{
    public class BookSaleService : IBookSaleService
    {
        private readonly IBookSaleRepository _bookSaleRepository;

        public BookSaleService(IBookSaleRepository bookSaleRepository)
        {
            _bookSaleRepository = bookSaleRepository;
        }

        public async Task<IEnumerable<BookSale>> GetAllBookSalesAsync()
        {

            return await _bookSaleRepository.GetAllBookSalesAsync();
        }

        public async Task<BookSale> GetBookSaleByIdAsync(Guid id)
        {
            return await _bookSaleRepository.GetBookSaleByIdAsync(id);
        }

        public async Task<List<AnalyticsReport>> GetSalesReportByDateAsync(DateTime startDate, DateTime endDate)
        {
            return await _bookSaleRepository.GetSalesReportByDateAsync(startDate, endDate);
        }

        public async Task<IEnumerable<BookSale>> GetSalesReportBySeller(Guid sellerId)
        {
            if (_bookSaleRepository.IsSellerExest(sellerId))
            {
            return await _bookSaleRepository.GetSalesReportBySeller(sellerId);

            }
            throw new NotFoundException("seller",sellerId.ToString());
        }
    }

}
