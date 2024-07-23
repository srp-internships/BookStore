using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Dto
{
    public class BookDto
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public string ISBN { get; set; }
        public PublisherDto PublisherId { get; set; }
        public ICollection<CategoryDto> CategoryIds { get; set; }
        public ICollection<AuthorDto> AuthorIds { get; set; }
    }
}
