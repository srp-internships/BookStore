using AnalyticService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticsService.Application.Interfaces
{
    public interface IBookSaleService
    {
        Task<BookSale> GetBookSaleByIdAsync(Guid id);
        Task<IEnumerable<BookSale>> GetAllBookSalesAsync();
        Task<List<AnalyticsReport>> GetSalesReportByDateAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<BookSale>> GetSalesReportBySeller(Guid SellerId);
    }
}
