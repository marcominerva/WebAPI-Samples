using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CalendarApi.BusinessLayer.Extensions
{
    public static class IFormFileExtensions
    {
        public static async Task<byte[]> GetContentAsByteArrayAsync(this IFormFile file)
        {
            using var readStream = file.OpenReadStream();
            using var outputStream = new MemoryStream();
            await readStream.CopyToAsync(outputStream);

            var content = outputStream.ToArray();
            return content;
        }

        public static async Task<Stream> GetContentAsStreamAsync(this IFormFile file)
        {
            using var readStream = file.OpenReadStream();
            var outputStream = new MemoryStream();
            await readStream.CopyToAsync(outputStream);

            outputStream.Position = 0;
            return outputStream;
        }
    }
}
