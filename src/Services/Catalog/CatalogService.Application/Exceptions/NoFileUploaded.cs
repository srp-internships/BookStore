using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Exceptions
{
    public class NoFileUploaded : Exception
    {
        public NoFileUploaded()
            : base("No File Uploaded") { }
    }
}
