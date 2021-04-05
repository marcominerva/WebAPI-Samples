using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
using System;
using System.Text.Json;
using System.Windows;
using WeatherClient.Models;
using WeatherClient.Services;

namespace WeatherClient
{
    public partial class App : Application
    {
        private readonly IHost host;

        public App()
        {
            host = Host.CreateDefaultBuilder()
                    .ConfigureServices((context, services) =>
                    {
                        ConfigureServices(context.Configuration, services);
                    })
                    .Build();
        }

        private static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            var appSettings = configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();

            // Create a RestClient using Refit and the System.Text.Json serializer.
            services.AddRefitClient<IWeatherServiceApi>(new RefitSettings
            {
                ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions(JsonSerializerDefaults.Web))
            }).ConfigureHttpClient(c => c.BaseAddress = new Uri(appSettings.ServiceUrl));

            services.AddSingleton<IWeatherService, WeatherService>();
            services.AddSingleton<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }
    }
}
