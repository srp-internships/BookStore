using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using CatalogService.Application.Interfaces.BlobServices;
using CatalogService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.BlobServices
{
    public class BlobService(BlobServiceClient blobServiceClient) : IBlobService
    {
        private const string ContainerName = "images";

        public async Task<string> UploadAsync(byte[] content, string contentType, CancellationToken token = default)
        {
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);
            BlobClient blobClient = containerClient.GetBlobClient(Guid.NewGuid().ToString());

            using (var memoryStream = new MemoryStream(content))
            {
                await blobClient.UploadAsync(memoryStream, new BlobHttpHeaders { ContentType = contentType }, cancellationToken: token);
            }

            return blobClient.Uri.ToString();
        }
        public async Task DeleteAsync(string url, CancellationToken token = default)
        {
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);
            BlobClient blobClient = containerClient.GetBlobClient(url);
            await blobClient.DeleteIfExistsAsync(cancellationToken: token);
        }

        public async Task<FileResponse> DownloadAsync(string url, CancellationToken token = default)
        {
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);
            BlobClient blobClient = containerClient.GetBlobClient(url);
            BlobDownloadResult response = await blobClient.DownloadContentAsync(cancellationToken: token);

            return new FileResponse
            {
                FileName = url, 
                Content = response.Content.ToStream(),
                ContentType = response.Details.ContentType
            };
        }
    }
}
