using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using MimeMapping;

namespace CalendarApi.BusinessLayer.Providers
{
    public class AzureStorageProvider : IStorageProvider
    {
        private readonly AzureStorageSettings settings;
        private readonly BlobServiceClient blobServiceClient;

        public AzureStorageProvider(AzureStorageSettings settings)
        {
            this.settings = settings;
            blobServiceClient = new BlobServiceClient(settings.ConnectionString);
        }

        public async Task SaveAsync(string path, byte[] content)
        {
            var blobContainerClient = await GetBlobClientAsync(settings.ContainerName, true);
            var blobClient = blobContainerClient.GetBlobClient(path);

            using var stream = new MemoryStream(content);
            await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = MimeUtility.GetMimeMapping(path) });
        }

        public async Task<byte[]> ReadAsync(string path)
        {
            var blobContainerClient = await GetBlobClientAsync(settings.ContainerName);
            var blobClient = blobContainerClient.GetBlobClient(path);

            using var stream = new MemoryStream();
            await blobClient.DownloadToAsync(stream);

            return stream.ToArray();
        }

        public async Task DeleteAsync(string path)
        {
            var blobClient = await GetBlobClientAsync(settings.ContainerName);
            await blobClient.DeleteBlobIfExistsAsync(path);
        }

        private async Task<BlobContainerClient> GetBlobClientAsync(string containerName, bool createIfNotExists = false)
        {
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
            if (createIfNotExists)
            {
                await blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.None);
            }

            return blobContainerClient;
        }
    }
}
