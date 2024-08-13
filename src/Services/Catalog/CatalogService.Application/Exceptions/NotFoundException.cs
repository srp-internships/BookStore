using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, Guid key)
            : base($"Entity '{name}' ({key}) not found") { }

        public NotFoundException(string name)
            : base($"Entity '{name}' not found") { }
    }
}
