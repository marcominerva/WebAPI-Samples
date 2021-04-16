using System.Threading.Tasks;

namespace CalendarApi.BusinessLayer.Providers
{
    public interface IStorageProvider
    {
        Task SaveAsync(string path, byte[] content);

        Task<byte[]> ReadAsync(string path);

        Task DeleteAsync(string path);
    }
}