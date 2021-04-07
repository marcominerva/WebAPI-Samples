using System;
using Microsoft.Extensions.DependencyInjection;

namespace CalendarApi.BusinessLayer.Providers
{
    public static class StorageProviderExtensions
    {
        public static IServiceCollection AddFileSystemStorage(this IServiceCollection services, Action<FileSystemStorageSettings> configuration)
        {
            var settings = new FileSystemStorageSettings();
            configuration?.Invoke(settings);
            services.AddSingleton(settings);

            services.AddScoped<IStorageProvider, FileSystemStorageProvider>();
            return services;
        }
    }
}
