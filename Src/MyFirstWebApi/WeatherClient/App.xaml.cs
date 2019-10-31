using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
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
            host = new HostBuilder()
                    .ConfigureAppConfiguration((context, builder) =>
                    {
                        builder.SetBasePath(context.HostingEnvironment.ContentRootPath);
                        builder.AddJsonFile("appsettings.json", optional: false);
                    })
                    .ConfigureServices((context, services) =>
                    {
                        services.Configure<AppSettings>(context.Configuration.GetSection(nameof(AppSettings)));
                        services.AddSingleton<IWeatherService, WeatherService>();
                        services.AddSingleton<MainWindow>();
                    })
                    .ConfigureLogging(logging =>
                    {
                        logging.AddConsole();
                    })
                    .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await host.StartAsync();

            var mainWindow = host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (host)
            {
                await host.StopAsync(TimeSpan.FromSeconds(5));
            }

            base.OnExit(e);
        }
    }
}
