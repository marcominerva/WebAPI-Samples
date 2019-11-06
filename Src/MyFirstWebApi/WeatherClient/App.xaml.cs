using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            host = Host.CreateDefaultBuilder()
                    .ConfigureServices((context, services) =>
                    {
                        services.Configure<AppSettings>(context.Configuration.GetSection(nameof(AppSettings)));
                        services.AddSingleton<IWeatherService, WeatherService>();
                        services.AddSingleton<MainWindow>();
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
