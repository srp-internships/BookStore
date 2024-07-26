using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Image { get; set; }
        public required string ISBN { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Author> Authors { get; set; }
        public ICollection<Seller> Sellers { get; set; }
        public ICollection<BookSeller> BookSellers { get; set; }
        public Guid PublisherId { get; set; }
        public Publisher? Publisher { get; set; }
    }
}
