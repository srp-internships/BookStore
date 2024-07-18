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
        Task AddBookSaleAsync(BookSale bookSale);
        Task<BookSale> GetBookSaleByIdAsync(Guid id);
        Task<IEnumerable<BookSale>> GetAllBookSalesAsync();
        Task UpdateBookSaleAsync(BookSale bookSale);
        Task DeleteBookSaleAsync(Guid id);
        Task<List<AnalyticsReport>> GetSalesReportByDateAsync(DateTime startDate, DateTime endDate);
    }
}
