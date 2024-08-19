using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalyticService.Domain.Entities;

namespace AnalyticsService.Application.Interfaces
{
    public interface IBookSaleRepository
    {
        Task<BookSale> GetBookSaleByIdAsync(Guid id);
        Task<IEnumerable<BookSale>> GetAllBookSalesAsync();
        Task<List<AnalyticsReport>> GetSalesReportByDateAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<BookSale>> GetSalesReportBySeller(Guid SellerId);
        public bool IsSellerExest(Guid id);
    }
}
