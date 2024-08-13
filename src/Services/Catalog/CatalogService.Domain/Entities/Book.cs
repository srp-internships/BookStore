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
        public string ISBN { get; set; }
        public List<Category> Categories { get; set; }
        public List<Author> Authors { get; set; }
        public List<Seller> Sellers { get; set; }
        public List<BookSeller> BookSellers { get; set; }
        public Guid PublisherId { get; set; }
        public Publisher? Publisher { get; set; }
    }
}
