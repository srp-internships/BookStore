using CatalogService.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class BookListVm
    {
        public List<BookDto> Books { get; set; }
    }
}
