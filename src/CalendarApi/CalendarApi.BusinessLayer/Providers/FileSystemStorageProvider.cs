using System.IO;
using System.Threading.Tasks;

namespace CalendarApi.BusinessLayer.Providers
{
    public class FileSystemStorageProvider : IStorageProvider
    {
        private readonly FileSystemStorageSettings settings;

        public FileSystemStorageProvider(FileSystemStorageSettings settings)
        {
            this.settings = settings;
        }

        public Task SaveAsync(string path, byte[] content)
        {
            var fullPath = Path.Combine(settings.StorageFolder, path);

            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            return File.WriteAllBytesAsync(fullPath, content);
        }

        public Task<byte[]> ReadAsync(string path)
        {
            var fullPath = Path.Combine(settings.StorageFolder, path);
            return File.ReadAllBytesAsync(fullPath);
        }

        public Task DeleteAsync(string path)
        {
            var fullPath = Path.Combine(settings.StorageFolder, path);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            return Task.CompletedTask;
        }
    }
}
