using System;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
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
                    .ConfigureServices(ConfigureServices)
                    .Build();
        }

        private static void ConfigureServices(HostBuilderContext hostingContext, IServiceCollection services)
        {
            var appSettings = hostingContext.Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();

            // Create a RestClient using Refit and the System.Text.Json serializer.
            services.AddRefitClient<IWeatherServiceApi>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri(appSettings.ServiceUrl);
                });

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
