using Refit;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherClient.Services
{
    public class JsonContentSerializer : IContentSerializer
    {
        private static readonly MediaTypeHeaderValue jsonMediaType =
            new MediaTypeHeaderValue("application/json") { CharSet = Encoding.UTF8.WebName };

        private readonly JsonSerializerOptions serializerOptions;

        public JsonContentSerializer(JsonSerializerOptions serializerOptions)
        {
            this.serializerOptions = serializerOptions;
        }

        public async Task<T> DeserializeAsync<T>(HttpContent content)
        {
            using var utf8Json = await content.ReadAsStreamAsync().ConfigureAwait(false);
            return await JsonSerializer.DeserializeAsync<T>(utf8Json, serializerOptions).ConfigureAwait(false);
        }

        public async Task<HttpContent> SerializeAsync<T>(T item)
        {
            using var stream = new MemoryStream();

            await JsonSerializer.SerializeAsync(stream, item, serializerOptions).ConfigureAwait(false);
            await stream.FlushAsync().ConfigureAwait(false);

            var content = new StreamContent(stream);
            content.Headers.ContentType = jsonMediaType;

            return content;
        }
    }
}
