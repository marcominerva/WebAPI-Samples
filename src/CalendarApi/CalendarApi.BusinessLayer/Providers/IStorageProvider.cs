using System.Threading.Tasks;

namespace CalendarApi.BusinessLayer.Providers
{
    public interface IStorageProvider
    {
        Task DeleteAsync(string path);
        Task<byte[]> ReadAsync(string path);
        Task SaveAsync(string path, byte[] content);
    }
}