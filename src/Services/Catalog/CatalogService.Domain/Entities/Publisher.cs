using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.Entities
{
    public class Publisher
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Address { get; set; }
        public string Logo { get; set; } = string.Empty;
        public ICollection<Book> Books { get; set; }
    }
}
