using CatalogService.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class PublisherListVm
    {
        public IList<PublisherDto> Publishers { get; set; }
    }
}
