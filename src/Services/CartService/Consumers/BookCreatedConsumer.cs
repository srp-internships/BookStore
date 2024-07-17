using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using CartService.Domain.Entities;
using System.Threading.Tasks;
using CartService.Infrastructure.Persistence.Contexts;

namespace Consumers
{

    public class BookCreatedConsumer : IConsumer<Book>
    {
        private readonly CartDbContext _context;

        public BookCreatedConsumer(CartDbContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<Book> context)
        {
            var book = context.Message;

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }
    }

}
