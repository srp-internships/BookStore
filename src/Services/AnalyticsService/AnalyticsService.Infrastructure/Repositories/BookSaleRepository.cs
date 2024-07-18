using AnalyticService.Domain.Entities;
using AnalyticsService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalyticsService.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using MassTransit;

namespace AnalyticsService.Infrastructure.Repositories
{
    public class BookSaleRepository : IBookSaleRepository
    {
        private readonly AnalyticsDbContext _context;

        public BookSaleRepository(AnalyticsDbContext context)
        {
            _context = context;
        }

        public async Task AddBookSaleAsync(BookSale bookSale)
        {
            await _context.BookSales.AddAsync(bookSale);
            await _context.SaveChangesAsync();
        }

        public async Task<BookSale> GetBookSaleByIdAsync(Guid id)
        {
            return await _context.BookSales.FindAsync(id);
        }

        public async Task<IEnumerable<BookSale>> GetAllBookSalesAsync()
        {
            return await _context.BookSales.ToListAsync();
        }

        public async Task UpdateBookSaleAsync(BookSale bookSale)
        {
            _context.BookSales.Update(bookSale);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookSaleAsync(Guid id)
        {
            var bookSale = await _context.BookSales.FindAsync(id);
            if (bookSale != null)
            {
                _context.BookSales.Remove(bookSale);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<AnalyticsReport>> GetSalesReportByDateAsync(DateTime startDate, DateTime endDate)
        {
            // Проверка валидности дат
            if (startDate > endDate)
            {
                throw new ArgumentException("Start date must be earlier than end date.");
            }

            // Извлечение данных из базы и агрегирование по дате
            var report = await _context.BookSales
                .Where(sale => sale.PurchaseDate >= startDate && sale.PurchaseDate <= endDate)
                .GroupBy(sale => sale.PurchaseDate.Date)
                .Select(group => new AnalyticsReport
                {
                    Date = group.Key,
                    TotalBooksSold = group.Sum(sale => sale.Quantity)
                })
                .OrderBy(report => report.Date)
                .ToListAsync();

            return report;
        }
    }
}
