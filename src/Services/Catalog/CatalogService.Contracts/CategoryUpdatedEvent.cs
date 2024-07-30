using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Contracts
{
    public class CategoryUpdatedEvent
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
}
