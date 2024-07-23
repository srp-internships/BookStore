using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Dto
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string ISBN { get; set; }
        public Guid PublisherId { get; set; }
        public ICollection<Guid> CategoryIds { get; set; }
        public ICollection<Guid> AuthorIds { get; set; }
    }
}
