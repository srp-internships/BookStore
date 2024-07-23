using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.Entities
{
    public class Seller
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<BookSeller> BookSellers { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
