using CatalogService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Interfaces.BlobServices
{
    public interface IBlobService
    {
        Task<string> UploadAsync(byte[] content, string contentType, CancellationToken token = default);
        Task<FileResponse> DownloadAsync(string url, CancellationToken token = default);
        Task DeleteAsync(string url, CancellationToken token = default);
    }
}
