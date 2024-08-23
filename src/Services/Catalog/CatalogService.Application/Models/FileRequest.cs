using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Models
{
    public class FileRequest
    {
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public byte[] Content { get; set; }

        FileResponse fileResponse = new FileResponse();
    }
}
